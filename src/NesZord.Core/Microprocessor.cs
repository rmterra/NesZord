using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using NesZord.Core.Extensions;

namespace NesZord.Core
{
	public class Microprocessor
	{
		public const int NEGATIVE_FLAG_BYTE_POSITION = 7;

		private readonly Dictionary<OpCode, Action> unadressedOperations;

		private readonly Dictionary<OpCode, Action<byte>> immediateOperations;

		private readonly Dictionary<OpCode, Action<MemoryLocation>> addressedOperations;

		private readonly Memory memory;

		public Microprocessor(Memory memory)
		{
			if (memory == null) { throw new ArgumentNullException(nameof(memory)); }

			this.memory = memory;

			this.unadressedOperations = new Dictionary<OpCode, Action>
			{
				{ OpCode.BCC, this.BranchIfCarryIsClear },
				{ OpCode.BCS, this.BranchIfCarryIsSet },
				{ OpCode.BEQ, this.BranchIfEqual },
				{ OpCode.BNE, this.BranchIfNotEqual },
				{ OpCode.DEX, this.DecrementValueAtX },
				{ OpCode.DEY, this.DecrementValueAtY },
				{ OpCode.INX, this.IncrementValueAtX },
				{ OpCode.INY, this.IncrementValueAtY },
				{ OpCode.SEC, this.SetCarryFlag },
				{ OpCode.TAX, this.TransferFromAccumulatorToX },
				{ OpCode.TAY, this.TransferFromAccumulatorToY },
				{ OpCode.TXA, this.TransferFromXToAccumulator },
				{ OpCode.TYA, this.TransferFromYToAccumulator }
			};

			this.immediateOperations = new Dictionary<OpCode, Action<byte>>
			{
				{ OpCode.ImmediateADC, this.AddWithCarry },
				{ OpCode.ImmediateAND, this.BitwiseAndOperation },
				{ OpCode.ImmediateCPX, this.CompareXRegister },
				{ OpCode.ImmediateCPY, this.CompareYRegister },
				{ OpCode.ImmediateLDY, this.LoadYRegister },
				{ OpCode.ImmediateLDX, this.LoadXRegister },
				{ OpCode.ImmediateLDA, this.LoadAccumulator },
				{ OpCode.ImmediateSBC, this.SubtractWithCarry }
			};

			this.addressedOperations = new Dictionary<OpCode, Action<MemoryLocation>>
			{
				{ OpCode.AbsoluteADC, this.AddWithCarry },
				{ OpCode.AbsoluteAND, this.BitwiseAndOperation },
				{ OpCode.AbsoluteCPY, this.CompareYRegister },
				{ OpCode.AbsoluteCPX, this.CompareXRegister },
				{ OpCode.AbsoluteLDA, this.LoadAccumulator },
				{ OpCode.AbsoluteLDX, this.LoadXRegister },
				{ OpCode.AbsoluteLDY, this.LoadYRegister },
				{ OpCode.AbsoluteSTA, this.StoreAccumulator },
				{ OpCode.AbsoluteSBC, this.SubtractWithCarry },
				{ OpCode.AbsoluteXADC, this.AddWithCarry },
				{ OpCode.AbsoluteXAND, this.BitwiseAndOperation },
				{ OpCode.AbsoluteXLDA, this.LoadAccumulator },
				{ OpCode.AbsoluteXLDY, this.LoadYRegister },
				{ OpCode.AbsoluteXSTA, this.StoreAccumulator },
				{ OpCode.AbsoluteXSBC, this.SubtractWithCarry },
				{ OpCode.AbsoluteYADC, this.AddWithCarry },
				{ OpCode.AbsoluteYAND, this.BitwiseAndOperation },
				{ OpCode.AbsoluteYLDA, this.LoadAccumulator },
				{ OpCode.AbsoluteYLDX, this.LoadXRegister },
				{ OpCode.AbsoluteYSTA, this.StoreAccumulator },
				{ OpCode.AbsoluteYSBC, this.SubtractWithCarry },
				{ OpCode.AbsoluteSTX, this.StoreXRegister },
				{ OpCode.AbsoluteSTY, this.StoreYRegister },
				{ OpCode.IndexedIndirectADC, this.AddWithCarry },
				{ OpCode.IndexedAND, this.BitwiseAndOperation },
				{ OpCode.IndexedIndirectLDA, this.LoadAccumulator },
				{ OpCode.IndexedIndirectSTA, this.StoreAccumulator },
				{ OpCode.IndexedIndirectSBC, this.SubtractWithCarry },
				{ OpCode.IndirectIndexedADC, this.AddWithCarry },
				{ OpCode.IndirectIndexedAND, this.BitwiseAndOperation },
				{ OpCode.IndirectIndexedLDA, this.LoadAccumulator },
				{ OpCode.IndirectIndexedSTA, this.StoreAccumulator },
				{ OpCode.IndirectIndexedSBC, this.SubtractWithCarry },
				{ OpCode.ZeroPageADC, this.AddWithCarry },
				{ OpCode.ZeroPageAND, this.BitwiseAndOperation },
				{ OpCode.ZeroPageCPY, this.CompareYRegister },
				{ OpCode.ZeroPageCPX, this.CompareXRegister },
				{ OpCode.ZeroPageLDA, this.LoadAccumulator },
				{ OpCode.ZeroPageLDX, this.LoadXRegister },
				{ OpCode.ZeroPageLDY, this.LoadYRegister },
				{ OpCode.ZeroPageSTA, this.StoreAccumulator },
				{ OpCode.ZeroPageSTX, this.StoreXRegister },
				{ OpCode.ZeroPageSTY, this.StoreYRegister },
				{ OpCode.ZeroPageSBC, this.SubtractWithCarry },
				{ OpCode.ZeroPageXADC, this.AddWithCarry },
				{ OpCode.ZeroPageXAND, this.BitwiseAndOperation },
				{ OpCode.ZeroPageXLDA, this.LoadAccumulator },
				{ OpCode.ZeroPageXLDY, this.LoadYRegister },
				{ OpCode.ZeroPageXSTA, this.StoreAccumulator },
				{ OpCode.ZeroPageXSTY, this.StoreYRegister },
				{ OpCode.ZeroPageXSBC, this.SubtractWithCarry },
				{ OpCode.ZeroPageYLDX, this.LoadXRegister },
				{ OpCode.ZeroPageYSTX, this.StoreXRegister }
			};
		}

		public bool Carry { get; private set; }

		public bool Negative { get; private set; }

		public bool Zero { get; private set; }

		public byte X { get; private set; }

		public byte Y { get; private set; }

		public byte Accumulator { get; private set; }

		public int ProgramCounter { get; private set; }

		public void RunProgram(IEnumerable<byte> program)
		{
			this.RunProgram(program.ToArray());
		}

		public void RunProgram(byte[] program)
		{
			this.LoadProgramToMemory(program);
			this.ProcessProgramByteWhileNotBreak();
		}

		private void LoadProgramToMemory(byte[] program)
		{
			this.ProgramCounter = Memory.PROGRAM_ROM_START;
			this.memory.LoadMemory(program);
		}

		private void ProcessProgramByteWhileNotBreak()
		{
			var opCode = (OpCode)this.ReadProgramByte();
			if (opCode == OpCode.BRK) { return; }

			if (this.unadressedOperations.ContainsKey(opCode))
			{
				this.unadressedOperations[opCode]();
			}
			else if (this.immediateOperations.ContainsKey(opCode))
			{
				this.immediateOperations[opCode](this.ReadProgramByte());
			}
			else if (this.addressedOperations.ContainsKey(opCode))
			{
				var addressingMode = AddressingModeLookup.For(opCode);
				this.addressedOperations[opCode](this.CreateMemoryLocation(addressingMode));
			}
			else
			{
				throw new InvalidOperationException(String.Format(CultureInfo.InvariantCulture, "unknown opcode {0}", opCode));
			}

			this.ProcessProgramByteWhileNotBreak();
		}

		private MemoryLocation CreateMemoryLocation(AddressingMode addressingMode)
		{
			var offset = default(byte);

			if (addressingMode == AddressingMode.ZeroPage) { return new MemoryLocation(this.ReadProgramByte(), Memory.ZERO_PAGE); }
			else if (addressingMode == AddressingMode.ZeroPageX) { return new MemoryLocation((byte)(this.ReadProgramByte() + this.X), Memory.ZERO_PAGE); }
			else if (addressingMode == AddressingMode.ZeroPageY) { return new MemoryLocation((byte)(this.ReadProgramByte() + this.Y), Memory.ZERO_PAGE); }
			else if (addressingMode == AddressingMode.Absolute) { offset = this.ReadProgramByte(); }
			else if (addressingMode == AddressingMode.AbsoluteX) { offset = (byte)(this.ReadProgramByte() + this.X); }
			else if (addressingMode == AddressingMode.AbsoluteY) { offset = (byte)(this.ReadProgramByte() + this.Y); }
			else if (addressingMode == AddressingMode.IndexedIndirect)
			{
				offset = (byte)(this.ReadProgramByte() + this.X);
				var indirectPage = this.memory.Read(offset, Memory.ZERO_PAGE);
				var indirectOffset = this.memory.Read((byte)(offset + 1), Memory.ZERO_PAGE);
				return new MemoryLocation(indirectOffset, indirectPage);
			}
			else if (addressingMode == AddressingMode.IndirectIndexed)
			{
				offset = this.ReadProgramByte();
				var indirectPage = this.memory.Read(offset, Memory.ZERO_PAGE);
				var indirectOffset = this.memory.Read((byte)(offset + 1), Memory.ZERO_PAGE);
				return new MemoryLocation(indirectOffset, (byte)(indirectPage + this.Y));
			}


			return new MemoryLocation(offset, this.ReadProgramByte());
		}

		private void StoreAccumulator(MemoryLocation location)
		{
			this.memory.Write(location, this.Accumulator);
		}

		private void StoreXRegister(MemoryLocation location)
		{
			this.memory.Write(location, this.X);
		}

		private void StoreYRegister(MemoryLocation location)
		{
			this.memory.Write(location, this.Y);
		}

		private void BranchIfCarryIsClear()
		{
			this.BranchIfConditionIsNotSatisfied(() => this.Carry);
		}

		private void BranchIfCarryIsSet()
		{
			this.BranchIfConditionIsNotSatisfied(() => this.Carry == false);
		}

		private void BranchIfEqual()
		{
			this.BranchIfConditionIsNotSatisfied(() => this.Zero == false);
		}

		private void BranchIfNotEqual()
		{
			this.BranchIfConditionIsNotSatisfied(() => this.Zero);
		}

		private void BranchIfConditionIsNotSatisfied(Func<bool> condition)
		{
			if (condition.Invoke()) { this.ReadProgramByte(); return; }

			var memoryPage = (byte)(this.ProgramCounter >> 8);
			var memoryOffset = (byte)(this.ProgramCounter & 0xff);
			var branchOffset = this.memory.Read(memoryOffset, memoryPage);

			var offset = 0xff ^ branchOffset;
			this.ProgramCounter -= offset;
		}

		private void DecrementValueAtX()
		{
			this.X -= 1;
		}

		private void DecrementValueAtY()
		{
			this.Y -= 1;
		}

		private void IncrementValueAtX()
		{
			this.X += 1;
		}

		private void IncrementValueAtY()
		{
			this.Y += 1;
		}

		private void AddWithCarry(MemoryLocation location)
		{
			var byteToAdd = this.memory.Read(location);
			this.AddWithCarry(byteToAdd);
		}

		private void AddWithCarry(byte byteToAdd)
		{
			var result = this.Accumulator + byteToAdd;
			this.Accumulator = (byte)(result & 0xff);
			this.Carry = (result >> 8) > 0;
		}

		private void BitwiseAndOperation(MemoryLocation location)
		{
			this.BitwiseAndOperation(this.memory.Read(location));
		}

		private void BitwiseAndOperation(byte byteToCompare)
		{
			this.Accumulator = (byte)(this.Accumulator & byteToCompare);
			this.Negative = this.Accumulator.GetBitAt(NEGATIVE_FLAG_BYTE_POSITION);
			this.Zero = this.Accumulator == 0;
		}

		private void CompareXRegister(MemoryLocation location)
		{
			this.CompareXRegister(this.memory.Read(location));
		}

		private void CompareXRegister(byte byteToCompare)
		{
			var result = this.X - byteToCompare;
			this.Carry = result >= 0;
			this.Zero = result == 0;
		}

		private void CompareYRegister(MemoryLocation location)
		{
			this.CompareYRegister(this.memory.Read(location));
		}

		private void CompareYRegister(byte byteToCompare)
		{
			var result = this.Y - byteToCompare;
			this.Carry = result >= 0;
			this.Zero = result == 0;
		}

		private void LoadAccumulator(MemoryLocation location)
		{
			this.LoadAccumulator(this.memory.Read(location));
		}

		private void LoadAccumulator(byte value)
		{
			this.Accumulator = value;
		}

		private void LoadXRegister(MemoryLocation location)
		{
			this.LoadXRegister(this.memory.Read(location));
		}

		private void LoadXRegister(byte value)
		{
			this.X = value;
		}

		private void LoadYRegister(MemoryLocation location)
		{
			this.LoadYRegister(this.memory.Read(location));
		}

		private void LoadYRegister(byte value)
		{
			this.Y = value;
		}

		private void SetCarryFlag()
		{
			this.Carry = true;
		}

		private void SubtractWithCarry(MemoryLocation location)
		{
			this.SubtractWithCarry(this.memory.Read(location));
		}

		private void SubtractWithCarry(byte byteToSubtract)
		{
			var result = this.Accumulator - byteToSubtract;
			this.Accumulator = (byte)(result & 0xff);
			this.Carry = (result << 8) > 0;
		}

		private void TransferFromAccumulatorToX()
		{
			this.X = this.Accumulator;
		}

		private void TransferFromAccumulatorToY()
		{
			this.Y = this.Accumulator;
		}

		private void TransferFromXToAccumulator()
		{
			this.Accumulator = this.X;
		}

		private void TransferFromYToAccumulator()
		{
			this.Accumulator = this.Y;
		}

		internal byte ReadProgramByte()
		{
			var page = (byte)(this.ProgramCounter >> 8);
			var offset = (byte)(this.ProgramCounter & 0xff);
			var value = this.memory.Read(offset, page);

			this.ProgramCounter++;

			return value;
		}
	}
}