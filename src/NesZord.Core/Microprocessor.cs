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
				{ OpCode.ASL_Accumulator, this.ArithmeticShiftLeftOnAccumulator },
				{ OpCode.BCC_Relative, this.BranchIfCarryIsClear },
				{ OpCode.BCS_Relative, this.BranchIfCarryIsSet },
				{ OpCode.BEQ_Relative, this.BranchIfEqual },
				{ OpCode.BNE_Relative, this.BranchIfNotEqual },
				{ OpCode.DEX_Implied, this.DecrementValueAtX },
				{ OpCode.DEY_Implied, this.DecrementValueAtY },
				{ OpCode.INX_Implied, this.IncrementValueAtX },
				{ OpCode.INY_Implied, this.IncrementValueAtY },
				{ OpCode.NOP_Implied, () => { } }, /*yes, this opcode do nothing =x*/
				{ OpCode.SEC_Implied, this.SetCarryFlag },
				{ OpCode.TAX_Implied, this.TransferFromAccumulatorToX },
				{ OpCode.TAY_Implied, this.TransferFromAccumulatorToY },
				{ OpCode.TXA_Implied, this.TransferFromXToAccumulator },
				{ OpCode.TYA_Implied, this.TransferFromYToAccumulator }
			};

			this.immediateOperations = new Dictionary<OpCode, Action<byte>>
			{
				{ OpCode.ADC_Immediate, this.AddWithCarry },
				{ OpCode.AND_Immediate, this.BitwiseAndOperation },
				{ OpCode.CPX_Immediate, this.CompareXRegister },
				{ OpCode.CPY_Immediate, this.CompareYRegister },
				{ OpCode.LDY_Immediate, this.LoadYRegister },
				{ OpCode.LDX_Immediate, this.LoadXRegister },
				{ OpCode.LDA_Immediate, this.LoadAccumulator },
				{ OpCode.SBC_Immediate, this.SubtractWithCarry }
			};

			this.addressedOperations = new Dictionary<OpCode, Action<MemoryLocation>>
			{
				{ OpCode.ADC_Absolute, this.AddWithCarry },
				{ OpCode.AND_Absolute, this.BitwiseAndOperation },
				{ OpCode.ASL_Absolute, this.ArithmeticShiftLeftOnMemory },
				{ OpCode.CPY_Absolute, this.CompareYRegister },
				{ OpCode.CPX_Absolute, this.CompareXRegister },
				{ OpCode.LDA_Absolute, this.LoadAccumulator },
				{ OpCode.LDX_Absolute, this.LoadXRegister },
				{ OpCode.LDY_Absolute, this.LoadYRegister },
				{ OpCode.STA_Absolute, this.StoreAccumulator },
				{ OpCode.SBC_Absolute, this.SubtractWithCarry },
				{ OpCode.ADC_AbsoluteX, this.AddWithCarry },
				{ OpCode.AND_AbsoluteX, this.BitwiseAndOperation },
				{ OpCode.ASL_AbsoluteX, this.ArithmeticShiftLeftOnMemory },
				{ OpCode.LDA_AbsoluteX, this.LoadAccumulator },
				{ OpCode.LDY_AbsoluteX, this.LoadYRegister },
				{ OpCode.STA_AbsoluteX, this.StoreAccumulator },
				{ OpCode.SBC_AbsoluteX, this.SubtractWithCarry },
				{ OpCode.ADC_AbsoluteY, this.AddWithCarry },
				{ OpCode.AND_AbsoluteY, this.BitwiseAndOperation },
				{ OpCode.LDA_AbsoluteY, this.LoadAccumulator },
				{ OpCode.LDX_AbsoluteY, this.LoadXRegister },
				{ OpCode.STA_AbsoluteY, this.StoreAccumulator },
				{ OpCode.SBC_AbsoluteY, this.SubtractWithCarry },
				{ OpCode.STX_Absolute, this.StoreXRegister },
				{ OpCode.STY_Absolute, this.StoreYRegister },
				{ OpCode.ADC_IndexedIndirect, this.AddWithCarry },
				{ OpCode.AND_IndexedIndirect, this.BitwiseAndOperation },
				{ OpCode.LDA_IndexedIndirect, this.LoadAccumulator },
				{ OpCode.STA_IndexedIndirect, this.StoreAccumulator },
				{ OpCode.SBC_IndexedIndirect, this.SubtractWithCarry },
				{ OpCode.ADC_IndirectIndexed, this.AddWithCarry },
				{ OpCode.AND_IndirectIndexed, this.BitwiseAndOperation },
				{ OpCode.LDA_IndirectIndexed, this.LoadAccumulator },
				{ OpCode.STA_IndirectIndexed, this.StoreAccumulator },
				{ OpCode.SBC_IndirectIndexed, this.SubtractWithCarry },
				{ OpCode.ADC_ZeroPage, this.AddWithCarry },
				{ OpCode.AND_ZeroPage, this.BitwiseAndOperation },
				{ OpCode.ASL_ZeroPage, this.ArithmeticShiftLeftOnMemory },
				{ OpCode.CPY_ZeroPage, this.CompareYRegister },
				{ OpCode.CPX_ZeroPage, this.CompareXRegister },
				{ OpCode.LDA_ZeroPage, this.LoadAccumulator },
				{ OpCode.LDX_ZeroPage, this.LoadXRegister },
				{ OpCode.LDY_ZeroPage, this.LoadYRegister },
				{ OpCode.STA_ZeroPage, this.StoreAccumulator },
				{ OpCode.STX_ZeroPage, this.StoreXRegister },
				{ OpCode.STY_ZeroPage, this.StoreYRegister },
				{ OpCode.SBC_ZeroPage, this.SubtractWithCarry },
				{ OpCode.ADC_ZeroPageX, this.AddWithCarry },
				{ OpCode.AND_ZeroPageX, this.BitwiseAndOperation },
				{ OpCode.ASL_ZeroPageX, this.ArithmeticShiftLeftOnMemory },
				{ OpCode.LDA_ZeroPageX, this.LoadAccumulator },
				{ OpCode.LDY_ZeroPageX, this.LoadYRegister },
				{ OpCode.STA_ZeroPageX, this.StoreAccumulator },
				{ OpCode.STY_ZeroPageX, this.StoreYRegister },
				{ OpCode.SBC_ZeroPageX, this.SubtractWithCarry },
				{ OpCode.LDX_ZeroPageY, this.LoadXRegister },
				{ OpCode.STX_ZeroPageY, this.StoreXRegister }
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
			if (opCode == OpCode.BRK_Implied) { return; }

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

		private void ArithmeticShiftLeftOnAccumulator()
		{
			this.Carry = this.Accumulator.GetBitAt(NEGATIVE_FLAG_BYTE_POSITION);

			this.Accumulator = (byte)(this.Accumulator << 1);

			this.Negative = this.Accumulator.GetBitAt(NEGATIVE_FLAG_BYTE_POSITION);
			this.Zero = (this.Accumulator & 0xff) == 0;
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

		private void ArithmeticShiftLeftOnMemory(MemoryLocation location)
		{
			var memoryValue = this.memory.Read(location);

			this.Carry = memoryValue.GetBitAt(NEGATIVE_FLAG_BYTE_POSITION);

			var shiftedValue = (byte)(memoryValue << 1);

			this.Negative = shiftedValue.GetBitAt(NEGATIVE_FLAG_BYTE_POSITION);
			this.Zero = (shiftedValue & 0xff) == 0;

			this.memory.Write(location, shiftedValue);
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