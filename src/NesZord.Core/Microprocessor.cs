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
				{ OpCode.BranchIfCarryIsClear, this.BranchIfCarryIsClear },
				{ OpCode.BranchIfCarryIsSet, this.BranchIfCarryIsSet },
				{ OpCode.BranchIfEqual, this.BranchIfEqual },
				{ OpCode.BranchIfNotEqual, this.BranchIfNotEqual },
				{ OpCode.DecrementValueAtX, this.DecrementValueAtX },
				{ OpCode.DecrementValueAtY, this.DecrementValueAtY },
				{ OpCode.IncrementValueAtX, this.IncrementValueAtX },
				{ OpCode.IncrementValueAtY, this.IncrementValueAtY },
				{ OpCode.SetCarryFlag, this.SetCarryFlag },
				{ OpCode.TransferFromAccumulatorToX, this.TransferFromAccumulatorToX },
				{ OpCode.TransferFromAccumulatorToY, this.TransferFromAccumulatorToY },
				{ OpCode.TransferFromXToAccumulator, this.TransferFromXToAccumulator },
				{ OpCode.TransferFromYToAccumulator, this.TransferFromYToAccumulator }
			};

			this.immediateOperations = new Dictionary<OpCode, Action<byte>>
			{
				{ OpCode.ImmediateAddWithCarry, this.AddWithCarry },
				{ OpCode.ImmediateBitwiseAnd, this.BitwiseAndOperation },
				{ OpCode.ImmediateCompareXRegister, this.CompareXRegister },
				{ OpCode.ImmediateCompareYRegister, this.CompareYRegister },
				{ OpCode.ImmediateLoadYRegister, this.LoadYRegister },
				{ OpCode.ImmediateLoadXRegister, this.LoadXRegister },
				{ OpCode.ImmediateLoadAccumulator, this.LoadAccumulator },
				{ OpCode.ImmediateSubtractWithCarry, this.SubtractWithCarry }
			};

			this.addressedOperations = new Dictionary<OpCode, Action<MemoryLocation>>
			{
				{ OpCode.AbsoluteAddWithCarry, this.AddWithCarry },
				{ OpCode.AbsoluteBitwiseAnd, this.BitwiseAndOperation },
				{ OpCode.AbsoluteCompareYRegister, this.CompareYRegister },
				{ OpCode.AbsoluteCompareXRegister, this.CompareXRegister },
				{ OpCode.AbsoluteLoadAccumulator, this.LoadAccumulator },
				{ OpCode.AbsoluteLoadXRegister, this.LoadXRegister },
				{ OpCode.AbsoluteLoadYRegister, this.LoadYRegister },
				{ OpCode.AbsoluteStoreAccumulator, this.StoreAccumulator },
				{ OpCode.AbsoluteSubtractWithCarry, this.SubtractWithCarry },
				{ OpCode.AbsoluteXAddWithCarry, this.AddWithCarry },
				{ OpCode.AbsoluteXBitwiseAnd, this.BitwiseAndOperation },
				{ OpCode.AbsoluteXLoadAccumulator, this.LoadAccumulator },
				{ OpCode.AbsoluteXLoadYRegister, this.LoadYRegister },
				{ OpCode.AbsoluteXStoreAccumulator, this.StoreAccumulator },
				{ OpCode.AbsoluteXSubtractWithCarry, this.SubtractWithCarry },
				{ OpCode.AbsoluteYAddWithCarry, this.AddWithCarry },
				{ OpCode.AbsoluteYBitwiseAnd, this.BitwiseAndOperation },
				{ OpCode.AbsoluteYLoadAccumulator, this.LoadAccumulator },
				{ OpCode.AbsoluteYLoadXRegister, this.LoadXRegister },
				{ OpCode.AbsoluteYStoreAccumulator, this.StoreAccumulator },
				{ OpCode.AbsoluteYSubtractWithCarry, this.SubtractWithCarry },
				{ OpCode.AbsoluteStoreXRegister, this.StoreXRegister },
				{ OpCode.AbsoluteStoreYRegister, this.StoreYRegister },
				{ OpCode.IndexedIndirectAddWithCarry, this.AddWithCarry },
				{ OpCode.IndexedIndirectBitwiseAnd, this.BitwiseAndOperation },
				{ OpCode.IndexedIndirectLoadAccumulator, this.LoadAccumulator },
				{ OpCode.IndexedIndirectStoreAccumulator, this.StoreAccumulator },
				{ OpCode.IndexedIndirectSubtractWithCarry, this.SubtractWithCarry },
				{ OpCode.IndirectIndexedAddWithCarry, this.AddWithCarry },
				{ OpCode.IndirectIndexedBitwiseAnd, this.BitwiseAndOperation },
				{ OpCode.IndirectIndexedLoadAccumulator, this.LoadAccumulator },
				{ OpCode.IndirectIndexedStoreAccumulator, this.StoreAccumulator },
				{ OpCode.IndirectIndexedSubtractWithCarry, this.SubtractWithCarry },
				{ OpCode.ZeroPageAddWithCarry, this.AddWithCarry },
				{ OpCode.ZeroPageBitwiseAnd, this.BitwiseAndOperation },
				{ OpCode.ZeroPageCompareYRegister, this.CompareYRegister },
				{ OpCode.ZeroPageCompareXRegister, this.CompareXRegister },
				{ OpCode.ZeroPageLoadAccumulator, this.LoadAccumulator },
				{ OpCode.ZeroPageLoadXRegister, this.LoadXRegister },
				{ OpCode.ZeroPageLoadYRegister, this.LoadYRegister },
				{ OpCode.ZeroPageStoreAccumulator, this.StoreAccumulator },
				{ OpCode.ZeroPageStoreXRegister, this.StoreXRegister },
				{ OpCode.ZeroPageStoreYRegister, this.StoreYRegister },
				{ OpCode.ZeroPageSubtractWithCarry, this.SubtractWithCarry },
				{ OpCode.ZeroPageXAddWithCarry, this.AddWithCarry },
				{ OpCode.ZeroPageXBitwiseAnd, this.BitwiseAndOperation },
				{ OpCode.ZeroPageXLoadAccumulator, this.LoadAccumulator },
				{ OpCode.ZeroPageXLoadYRegister, this.LoadYRegister },
				{ OpCode.ZeroPageXStoreAccumulator, this.StoreAccumulator },
				{ OpCode.ZeroPageXStoreYRegister, this.StoreYRegister },
				{ OpCode.ZeroPageXSubtractWithCarry, this.SubtractWithCarry },
				{ OpCode.ZeroPageYLoadXRegister, this.LoadXRegister },
				{ OpCode.ZeroPageYStoreXRegister, this.StoreXRegister }
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
			if (opCode == OpCode.Break) { return; }

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