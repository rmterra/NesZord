using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using NesZord.Core.Extensions;
using NesZord.Core.Memory;

namespace NesZord.Core
{
	public class Cpu
	{
		public const int BCD_MAX_VALUE = 99;

		public const int BREAK_BIT_INDEX = 4;

		public const int CARRY_BIT_INDEX = 0;

		public const int DECIMAL_BIT_INDEX = 3;

		public const int FIRST_BIT_INDEX = 0;

		public const int INTERRUPT_BIT_INDEX = 2;

		public const int MAX_NEGATIVE_VALUE = -128;

		public const int MAX_POSITIVE_VALUE = 127;

		public const int OVERFLOW_BIT_INDEX = 6;

		public const int SIGN_BIT_INDEX = 7;

		public const int ZERO_BIT_INDEX = 1;

		private readonly Dictionary<OpCode, Action> unadressedOperations;

		private readonly Dictionary<OpCode, Action<byte>> immediateOperations;

		private readonly Dictionary<OpCode, Action<MemoryAddress>> addressedOperations;

		private readonly Emulator emulator;

		public Cpu(Emulator emulator)
		{
			this.emulator = emulator ?? throw new ArgumentNullException(nameof(emulator));

			this.StackPointer = new Stack(this.emulator);
			this.Accumulator = new Register();
			this.X = new Register();
			this.Y = new Register();

			this.unadressedOperations = new Dictionary<OpCode, Action>
			{
				{ OpCode.ASL_Accumulator, this.ArithmeticShiftLeftOnAccumulator },
				{ OpCode.LSR_Accumulator, this.LogicalShiftRightOnAccumulator },
				{ OpCode.ROL_Accumulator, this.RotateLeftOnAccumulator },
				{ OpCode.ROR_Accumulator, this.RotateRightOnAccumulator },
				{ OpCode.BCC_Relative, this.BranchIfCarryIsClear },
				{ OpCode.BCS_Relative, this.BranchIfCarryIsSet },
				{ OpCode.BEQ_Relative, this.BranchIfEqual },
				{ OpCode.BMI_Relative, this.BranchIfNegativeIsSet },
				{ OpCode.BNE_Relative, this.BranchIfNotEqual },
				{ OpCode.BPL_Relative, this.BranchIfNegativeIsClear },
				{ OpCode.BVC_Relative, this.BranchIfOverflowIsClear },
				{ OpCode.BVS_Relative, this.BranchIfOverflowIsSet },
				{ OpCode.CLC_Implied, this.ClearCarryFlag },
				{ OpCode.CLD_Implied, this.ClearDecimalFlag },
				{ OpCode.CLI_Implied, this.ClearInterruptFlag },
				{ OpCode.CLV_Implied, this.ClearOverflowFlag },
				{ OpCode.DEX_Implied, this.DecrementValueAtX },
				{ OpCode.DEY_Implied, this.DecrementValueAtY },
				{ OpCode.INX_Implied, this.IncrementValueAtX },
				{ OpCode.INY_Implied, this.IncrementValueAtY },
				{ OpCode.NOP_Implied, () => { } }, /*yes, this opcode do nothing =x*/
				{ OpCode.PHA_Implied, this.PushAccumulatorToStack },
				{ OpCode.PHP_Implied, this.PushCpuStatusToStack },
				{ OpCode.PLA_Implied, this.PullFromStackToAccumulator },
				{ OpCode.PLP_Implied, this.PullFromStackToStatusFlags },
				{ OpCode.RTI_Implied, this.ReturnFromInterrupt },
				{ OpCode.RTS_Implied, this.ReturnFromSubRoutine },
				{ OpCode.SEC_Implied, this.SetCarryFlag },
				{ OpCode.SED_Implied, this.SetDecimalFlag },
				{ OpCode.SEI_Implied, this.SetInterruptFlag },
				{ OpCode.TAX_Implied, this.TransferFromAccumulatorToX },
				{ OpCode.TAY_Implied, this.TransferFromAccumulatorToY },
				{ OpCode.TSX_Implied, this.TransferFromStackPointerToX },
				{ OpCode.TXA_Implied, this.TransferFromXToAccumulator },
				{ OpCode.TXS_Implied, this.TransferFromXToStackPointer },
				{ OpCode.TYA_Implied, this.TransferFromYToAccumulator }
			};

			this.immediateOperations = new Dictionary<OpCode, Action<byte>>
			{
				{ OpCode.ADC_Immediate, this.AddWithCarry },
				{ OpCode.AND_Immediate, this.BitwiseAndOperation },
				{ OpCode.CMP_Immediate, this.CompareAccumulator },
				{ OpCode.CPX_Immediate, this.CompareXRegister },
				{ OpCode.CPY_Immediate, this.CompareYRegister },
				{ OpCode.EOR_Immediate, this.BitwiseExclusiveOrOperation },
				{ OpCode.LDY_Immediate, this.LoadYRegister },
				{ OpCode.LDX_Immediate, this.LoadXRegister },
				{ OpCode.LDA_Immediate, this.LoadAccumulator },
				{ OpCode.ORA_Immediate, this.BitwiseOrOperation },
				{ OpCode.SBC_Immediate, this.SubtractWithCarry }
			};

			this.addressedOperations = new Dictionary<OpCode, Action<MemoryAddress>>
			{
				{ OpCode.ADC_Absolute, this.AddWithCarry },
				{ OpCode.AND_Absolute, this.BitwiseAndOperation },
				{ OpCode.ASL_Absolute, this.ArithmeticShiftLeftOnMemory },
				{ OpCode.BIT_Absolute, this.TestBitsInAccumulator },
				{ OpCode.CMP_Absolute, this.CompareAccumulator },
				{ OpCode.CPY_Absolute, this.CompareYRegister },
				{ OpCode.CPX_Absolute, this.CompareXRegister },
				{ OpCode.DEC_Absolute, this.DecrementValueAtMemory },
				{ OpCode.EOR_Absolute, this.BitwiseExclusiveOrOperation },
				{ OpCode.INC_Absolute, this.IncrementValueAtMemory },
				{ OpCode.JMP_Absolute, this.Jump },
				{ OpCode.JSR_Absolute, this.JumpToSubRoutine },
				{ OpCode.LDA_Absolute, this.LoadAccumulator },
				{ OpCode.LDX_Absolute, this.LoadXRegister },
				{ OpCode.LDY_Absolute, this.LoadYRegister },
				{ OpCode.LSR_Absolute, this.LogicalShiftRightOnMemory },
				{ OpCode.ORA_Absolute, this.BitwiseOrOperation },
				{ OpCode.ROL_Absolute, this.RotateLeftOnMemory },
				{ OpCode.ROR_Absolute, this.RotateRightOnMemory },
				{ OpCode.STA_Absolute, this.StoreAccumulator },
				{ OpCode.SBC_Absolute, this.SubtractWithCarry },
				{ OpCode.ADC_AbsoluteX, this.AddWithCarry },
				{ OpCode.AND_AbsoluteX, this.BitwiseAndOperation },
				{ OpCode.ASL_AbsoluteX, this.ArithmeticShiftLeftOnMemory },
				{ OpCode.CMP_AbsoluteX, this.CompareAccumulator },
				{ OpCode.DEC_AbsoluteX, this.DecrementValueAtMemory },
				{ OpCode.EOR_AbsoluteX, this.BitwiseExclusiveOrOperation },
				{ OpCode.INC_AbsoluteX, this.IncrementValueAtMemory },
				{ OpCode.LDA_AbsoluteX, this.LoadAccumulator },
				{ OpCode.LDY_AbsoluteX, this.LoadYRegister },
				{ OpCode.LSR_AbsoluteX, this.LogicalShiftRightOnMemory },
				{ OpCode.ORA_AbsoluteX, this.BitwiseOrOperation },
				{ OpCode.ROL_AbsoluteX, this.RotateLeftOnMemory },
				{ OpCode.ROR_AbsoluteX, this.RotateRightOnMemory },
				{ OpCode.STA_AbsoluteX, this.StoreAccumulator },
				{ OpCode.SBC_AbsoluteX, this.SubtractWithCarry },
				{ OpCode.ADC_AbsoluteY, this.AddWithCarry },
				{ OpCode.AND_AbsoluteY, this.BitwiseAndOperation },
				{ OpCode.CMP_AbsoluteY, this.CompareAccumulator },
				{ OpCode.EOR_AbsoluteY, this.BitwiseExclusiveOrOperation },
				{ OpCode.LDA_AbsoluteY, this.LoadAccumulator },
				{ OpCode.LDX_AbsoluteY, this.LoadXRegister },
				{ OpCode.ORA_AbsoluteY, this.BitwiseOrOperation },
				{ OpCode.STA_AbsoluteY, this.StoreAccumulator },
				{ OpCode.SBC_AbsoluteY, this.SubtractWithCarry },
				{ OpCode.STX_Absolute, this.StoreXRegister },
				{ OpCode.STY_Absolute, this.StoreYRegister },
				{ OpCode.JMP_Indirect, this.Jump },
				{ OpCode.ADC_IndexedIndirect, this.AddWithCarry },
				{ OpCode.AND_IndexedIndirect, this.BitwiseAndOperation },
				{ OpCode.CMP_IndexedIndirect, this.CompareAccumulator },
				{ OpCode.EOR_IndexedIndirect, this.BitwiseExclusiveOrOperation },
				{ OpCode.LDA_IndexedIndirect, this.LoadAccumulator },
				{ OpCode.ORA_IndexedIndirect, this.BitwiseOrOperation },
				{ OpCode.STA_IndexedIndirect, this.StoreAccumulator },
				{ OpCode.SBC_IndexedIndirect, this.SubtractWithCarry },
				{ OpCode.ADC_IndirectIndexed, this.AddWithCarry },
				{ OpCode.AND_IndirectIndexed, this.BitwiseAndOperation },
				{ OpCode.CMP_IndirectIndexed, this.CompareAccumulator },
				{ OpCode.EOR_IndirectIndexed, this.BitwiseExclusiveOrOperation },
				{ OpCode.LDA_IndirectIndexed, this.LoadAccumulator },
				{ OpCode.ORA_IndirectIndexed, this.BitwiseOrOperation },
				{ OpCode.STA_IndirectIndexed, this.StoreAccumulator },
				{ OpCode.SBC_IndirectIndexed, this.SubtractWithCarry },
				{ OpCode.ADC_ZeroPage, this.AddWithCarry },
				{ OpCode.AND_ZeroPage, this.BitwiseAndOperation },
				{ OpCode.ASL_ZeroPage, this.ArithmeticShiftLeftOnMemory },
				{ OpCode.BIT_ZeroPage, this.TestBitsInAccumulator },
				{ OpCode.CMP_ZeroPage, this.CompareAccumulator },
				{ OpCode.CPY_ZeroPage, this.CompareYRegister },
				{ OpCode.CPX_ZeroPage, this.CompareXRegister },
				{ OpCode.DEC_ZeroPage, this.DecrementValueAtMemory },
				{ OpCode.EOR_ZeroPage, this.BitwiseExclusiveOrOperation },
				{ OpCode.INC_ZeroPage, this.IncrementValueAtMemory },
				{ OpCode.LDA_ZeroPage, this.LoadAccumulator },
				{ OpCode.LDX_ZeroPage, this.LoadXRegister },
				{ OpCode.LDY_ZeroPage, this.LoadYRegister },
				{ OpCode.LSR_ZeroPage, this.LogicalShiftRightOnMemory },
				{ OpCode.ROL_ZeroPage, this.RotateLeftOnMemory },
				{ OpCode.ROR_ZeroPage, this.RotateRightOnMemory },
				{ OpCode.ORA_ZeroPage, this.BitwiseOrOperation },
				{ OpCode.STA_ZeroPage, this.StoreAccumulator },
				{ OpCode.STX_ZeroPage, this.StoreXRegister },
				{ OpCode.STY_ZeroPage, this.StoreYRegister },
				{ OpCode.SBC_ZeroPage, this.SubtractWithCarry },
				{ OpCode.ADC_ZeroPageX, this.AddWithCarry },
				{ OpCode.AND_ZeroPageX, this.BitwiseAndOperation },
				{ OpCode.ASL_ZeroPageX, this.ArithmeticShiftLeftOnMemory },
				{ OpCode.CMP_ZeroPageX, this.CompareAccumulator },
				{ OpCode.DEC_ZeroPageX, this.DecrementValueAtMemory },
				{ OpCode.EOR_ZeroPageX, this.BitwiseExclusiveOrOperation },
				{ OpCode.INC_ZeroPageX, this.IncrementValueAtMemory },
				{ OpCode.LDA_ZeroPageX, this.LoadAccumulator },
				{ OpCode.LDY_ZeroPageX, this.LoadYRegister },
				{ OpCode.LSR_ZeroPageX, this.LogicalShiftRightOnMemory },
				{ OpCode.ORA_ZeroPageX, this.BitwiseOrOperation },
				{ OpCode.ROL_ZeroPageX, this.RotateLeftOnMemory },
				{ OpCode.ROR_ZeroPageX, this.RotateRightOnMemory },
				{ OpCode.STA_ZeroPageX, this.StoreAccumulator },
				{ OpCode.STY_ZeroPageX, this.StoreYRegister },
				{ OpCode.SBC_ZeroPageX, this.SubtractWithCarry },
				{ OpCode.LDX_ZeroPageY, this.LoadXRegister },
				{ OpCode.STX_ZeroPageY, this.StoreXRegister }
			};
		}

		public bool Break { get; private set; }

		public bool Carry { get; private set; }

		public bool Decimal { get; private set; }

		public bool Interrupt { get; private set; }

		public bool Negative { get; private set; }

		public bool Overflow { get; private set; }

		public bool Zero { get; private set; }

		public Stack StackPointer { get; private set; }

		public int ProgramCounter { get; private set; }

		public Register Accumulator { get; private set; }

		public Register X { get; private set; }

		public Register Y { get; private set; }

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
			this.ProgramCounter = Emulator.PROGRAM_ROM_START.FullAddress;
			this.emulator.LoadProgram(program);
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
				this.addressedOperations[opCode](this.CreateMemoryAddress(addressingMode));
			}
			else
			{
				throw new InvalidOperationException(String.Format(CultureInfo.InvariantCulture, "unknown opcode {0}", opCode));
			}

			this.ProcessProgramByteWhileNotBreak();
		}

		private MemoryAddress CreateMemoryAddress(AddressingMode addressingMode)
		{
			var offset = this.ReadProgramByte();

			if (addressingMode == AddressingMode.ZeroPage) { return new MemoryAddress(Emulator.ZERO_PAGE, offset); }
			else if (addressingMode == AddressingMode.ZeroPageX) { return new MemoryAddress(Emulator.ZERO_PAGE, (byte)(offset + this.X.Value)); }
			else if (addressingMode == AddressingMode.ZeroPageY) { return new MemoryAddress(Emulator.ZERO_PAGE, (byte)(offset + this.Y.Value)); }
			else if (addressingMode == AddressingMode.AbsoluteX)
			{
				var address = new MemoryAddress(this.ReadProgramByte(), offset);
				return address.Sum(this.X.Value);
			}
			else if (addressingMode == AddressingMode.AbsoluteY)
			{
				var address = new MemoryAddress(this.ReadProgramByte(), offset);
				return address.Sum(this.Y.Value);
			}
			else if (addressingMode == AddressingMode.Indirect)
			{
				var indirectOffset = this.emulator.Read(Emulator.ZERO_PAGE, offset);
				var indirectPage = this.emulator.Read(Emulator.ZERO_PAGE, (byte)(offset + 1));
				return new MemoryAddress(indirectPage, indirectOffset);
			}
			else if (addressingMode == AddressingMode.IndexedIndirect)
			{
				offset += this.X.Value;
				var indirectOffset = this.emulator.Read(Emulator.ZERO_PAGE, offset);
				var indirectPage = this.emulator.Read(Emulator.ZERO_PAGE, (byte)(offset + 1));
				return new MemoryAddress(indirectPage, indirectOffset);
			}
			else if (addressingMode == AddressingMode.IndirectIndexed)
			{
				var indirectOffset = this.emulator.Read(Emulator.ZERO_PAGE, offset);
				var indirectPage = this.emulator.Read(Emulator.ZERO_PAGE, (byte)(offset + 1));
				var address = new MemoryAddress(indirectPage, indirectOffset);
				return address.Sum(this.Y.Value);
			}

			return new MemoryAddress(this.ReadProgramByte(), offset);
		}

		private void StoreAccumulator(MemoryAddress address)
		{
			this.emulator.Write(address, this.Accumulator.Value);
		}

		private void StoreXRegister(MemoryAddress address)
		{
			this.emulator.Write(address, this.X.Value);
		}

		private void StoreYRegister(MemoryAddress address)
		{
			this.emulator.Write(address, this.Y.Value);
		}

		private void ArithmeticShiftLeftOnAccumulator()
		{
			this.Carry = this.Accumulator.IsSignBitSet;

			this.Accumulator.ShiftLeft();

			this.Negative = this.Accumulator.IsSignBitSet;
			this.Zero = this.Accumulator.IsValueEqualZero;
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

		private void BranchIfNegativeIsClear()
		{
			this.BranchIfConditionIsNotSatisfied(() => this.Negative);
		}

		private void BranchIfNegativeIsSet()
		{
			this.BranchIfConditionIsNotSatisfied(() => this.Negative == false);
		}

		private void BranchIfOverflowIsClear()
		{
			this.BranchIfConditionIsNotSatisfied(() => this.Overflow);
		}

		private void BranchIfOverflowIsSet()
		{
			this.BranchIfConditionIsNotSatisfied(() => this.Overflow == false);
		}

		private void BranchIfConditionIsNotSatisfied(Func<bool> condition)
		{
			var offset = this.ReadProgramByte();

			if (condition.Invoke()) { return; }

			this.ProgramCounter += offset;
		}

		private void DecrementValueAtMemory(MemoryAddress address)
		{
			var memoryValue = (byte)(this.emulator.Read(address) - 1);
			this.Zero = memoryValue == 0;
			this.Negative = memoryValue.GetBitAt(SIGN_BIT_INDEX);

			this.emulator.Write(address, memoryValue);
		}

		private void DecrementValueAtX()
		{
			this.X.Decrement();
			this.Zero = this.X.IsValueEqualZero;
			this.Negative = this.X.IsSignBitSet;
		}

		private void DecrementValueAtY()
		{
			this.Y.Decrement();
			this.Zero = this.Y.IsValueEqualZero;
			this.Negative = this.Y.IsSignBitSet;
		}

		private void IncrementValueAtMemory(MemoryAddress address)
		{
			var memoryValue = (byte)(this.emulator.Read(address) + 1);
			this.Zero = memoryValue == 0;
			this.Negative = memoryValue.GetBitAt(SIGN_BIT_INDEX);

			this.emulator.Write(address, memoryValue);
		}

		private void IncrementValueAtX()
		{
			this.X.Increment();
			this.Zero = this.X.IsValueEqualZero;
			this.Negative = this.X.IsSignBitSet;
		}

		private void IncrementValueAtY()
		{
			this.Y.Increment();
			this.Zero = this.Y.IsValueEqualZero;
			this.Negative = this.Y.IsSignBitSet;
		}

		private void LogicalShiftRightOnAccumulator()
		{
			this.Negative = false;
			this.Carry = this.Accumulator.IsFirstBitSet;

			this.Accumulator.ShiftRight();
			this.Zero = this.Accumulator.IsValueEqualZero;
		}

		private void AddWithCarry(MemoryAddress address)
		{
			var byteToAdd = this.emulator.Read(address);
			this.AddWithCarry(byteToAdd);
		}

		private void AddWithCarry(byte byteToAdd)
		{
			var result = this.Decimal
				? this.Accumulator.ToBcd() + byteToAdd.ConvertToBcd() + Convert.ToInt32(this.Carry)
				: this.Accumulator.Value + byteToAdd + Convert.ToInt32(this.Carry);

			this.Negative = this.Accumulator.IsSignBitSet;
			this.Zero = result == 0;

			this.Carry = result > (this.Decimal ? BCD_MAX_VALUE : Byte.MaxValue);
			this.Overflow = this.Carry;

			this.Accumulator.Value = (byte)(result & 0xff);
		}

		private void ArithmeticShiftLeftOnMemory(MemoryAddress address)
		{
			var memoryValue = this.emulator.Read(address);

			this.Carry = memoryValue.GetBitAt(SIGN_BIT_INDEX);

			var shiftedValue = (byte)(memoryValue << 1);

			this.Negative = shiftedValue.GetBitAt(SIGN_BIT_INDEX);
			this.Zero = (shiftedValue & 0xff) == 0;

			this.emulator.Write(address, shiftedValue);
		}

		private void BitwiseAndOperation(MemoryAddress address)
		{
			this.BitwiseAndOperation(this.emulator.Read(address));
		}

		private void BitwiseAndOperation(byte byteToCompare)
		{
			this.Accumulator.And(byteToCompare);
			this.Negative = this.Accumulator.IsSignBitSet;
			this.Zero = this.Accumulator.IsValueEqualZero;
		}

		private void BitwiseExclusiveOrOperation(MemoryAddress address)
		{
			this.BitwiseExclusiveOrOperation(this.emulator.Read(address));
		}

		private void BitwiseExclusiveOrOperation(byte byteToCompare)
		{
			this.Accumulator.ExlusiveOr(byteToCompare);
			this.Negative = this.Accumulator.IsSignBitSet;
			this.Zero = this.Accumulator.IsValueEqualZero;
		}

		private void BitwiseOrOperation(MemoryAddress address)
		{
			this.BitwiseOrOperation(this.emulator.Read(address));
		}

		private void BitwiseOrOperation(byte byteToCompare)
		{
			this.Accumulator.Or(byteToCompare);
			this.Negative = this.Accumulator.IsSignBitSet;
			this.Zero = this.Accumulator.IsValueEqualZero;
		}

		private void ClearCarryFlag()
		{
			this.Carry = false;
		}

		private void ClearDecimalFlag()
		{
			this.Decimal = false;
		}

		private void ClearInterruptFlag()
		{
			this.Interrupt = false;
		}

		private void ClearOverflowFlag()
		{
			this.Overflow = false;
		}

		private void CompareAccumulator(MemoryAddress address)
		{
			this.CompareAccumulator(this.emulator.Read(address));
		}

		private void CompareAccumulator(byte byteToCompare)
		{
			var result = (byte)(this.Accumulator.Value - byteToCompare);
			this.Negative = result.GetBitAt(SIGN_BIT_INDEX);
			this.Carry = this.Accumulator.Value >= byteToCompare;
			this.Zero = result == 0;
		}

		private void CompareXRegister(MemoryAddress address)
		{
			this.CompareXRegister(this.emulator.Read(address));
		}

		private void CompareXRegister(byte byteToCompare)
		{
			var result = (byte)(this.X.Value - byteToCompare);
			this.Negative = result.GetBitAt(SIGN_BIT_INDEX);
			this.Carry = this.X.Value >= byteToCompare;
			this.Zero = result == 0;
		}

		private void CompareYRegister(MemoryAddress address)
		{
			this.CompareYRegister(this.emulator.Read(address));
		}

		private void CompareYRegister(byte byteToCompare)
		{
			var result = (byte)(this.Y.Value - byteToCompare);
			this.Negative = result.GetBitAt(SIGN_BIT_INDEX);
			this.Carry = this.Y.Value >= byteToCompare;
			this.Zero = result == 0;
		}

		private void Jump(MemoryAddress address)
		{
			this.ProgramCounter = address.FullAddress;
		}

		private void JumpToSubRoutine(MemoryAddress address)
		{
			var page = (byte)(this.ProgramCounter >> 8);
			this.StackPointer.Push(page);

			var offset = (byte)(this.ProgramCounter & 0xff);
			this.StackPointer.Push(offset);

			this.ProgramCounter = address.FullAddress;
		}

		private void LoadAccumulator(MemoryAddress address)
		{
			this.LoadAccumulator(this.emulator.Read(address));
		}

		private void LoadAccumulator(byte value)
		{
			this.Accumulator.Value = value;
			this.UpdateFlagsWith(this.Accumulator.Value);
		}

		private void LoadXRegister(MemoryAddress address)
		{
			this.LoadXRegister(this.emulator.Read(address));
		}

		private void LoadXRegister(byte value)
		{
			this.X.Value = value;
			this.UpdateFlagsWith(this.X.Value);
		}

		private void LoadYRegister(MemoryAddress address)
		{
			this.LoadYRegister(this.emulator.Read(address));
		}

		private void LoadYRegister(byte value)
		{
			this.Y.Value = value;
			this.UpdateFlagsWith(this.Y.Value);
		}

		private void LogicalShiftRightOnMemory(MemoryAddress address)
		{
			var memoryValue = this.emulator.Read(address);

			this.Negative = false;
			this.Carry = memoryValue.GetBitAt(FIRST_BIT_INDEX);

			var shiftedValue = (byte)(memoryValue >> 1);
			this.Zero = (shiftedValue & 0xff) == 0;

			this.emulator.Write(address, shiftedValue);
		}

		private void PullFromStackToAccumulator()
		{
			this.Accumulator.Value = this.StackPointer.Pop();
			this.Zero = this.Accumulator.IsValueEqualZero;
			this.Negative = this.Accumulator.IsSignBitSet;
		}

		private void PullFromStackToStatusFlags()
		{
			var status = this.StackPointer.Pop();

			this.Carry = status.GetBitAt(CARRY_BIT_INDEX);
			this.Zero = status.GetBitAt(ZERO_BIT_INDEX);
			this.Interrupt = status.GetBitAt(INTERRUPT_BIT_INDEX);
			this.Decimal = status.GetBitAt(DECIMAL_BIT_INDEX);
			this.Break = status.GetBitAt(BREAK_BIT_INDEX);
			this.Overflow = status.GetBitAt(OVERFLOW_BIT_INDEX);
			this.Negative = status.GetBitAt(SIGN_BIT_INDEX);
		}

		private void PushAccumulatorToStack()
		{
			this.StackPointer.Push(this.Accumulator.Value);
		}

		private void PushCpuStatusToStack()
		{
			var status = default(byte);

			status = status.SetBitAt(CARRY_BIT_INDEX, this.Carry);
			status = status.SetBitAt(ZERO_BIT_INDEX, this.Zero);
			status = status.SetBitAt(INTERRUPT_BIT_INDEX, this.Interrupt);
			status = status.SetBitAt(DECIMAL_BIT_INDEX, this.Decimal);
			status = status.SetBitAt(BREAK_BIT_INDEX, this.Break);
			status = status.SetBitAt(OVERFLOW_BIT_INDEX, this.Overflow);
			status = status.SetBitAt(SIGN_BIT_INDEX, this.Negative);

			this.StackPointer.Push(status);
		}

		private void ReturnFromInterrupt()
		{
			this.PullFromStackToStatusFlags();

			var offset = this.StackPointer.Pop();
			var page = this.StackPointer.Pop();
			this.ProgramCounter = new MemoryAddress(page, offset).FullAddress;
		}

		private void ReturnFromSubRoutine()
		{
			var offset = this.StackPointer.Pop();
			var page = this.StackPointer.Pop();
			var address = new MemoryAddress(page, offset);

			this.ProgramCounter = address.FullAddress;
		}

		private void RotateLeftOnAccumulator()
		{
			var newCarryValue = this.Accumulator.IsSignBitSet;

			this.Accumulator.RotateLeft(Convert.ToByte(this.Carry));

			this.Carry = newCarryValue;

			this.Zero = this.Accumulator.IsValueEqualZero;
			this.Negative = this.Accumulator.IsSignBitSet;
		}

		private void RotateLeftOnMemory(MemoryAddress address)
		{
			var memoryByte = this.emulator.Read(address);

			var newCarryValue = memoryByte.GetBitAt(SIGN_BIT_INDEX);

			memoryByte = (byte)(memoryByte << 1);
			memoryByte = (byte)(memoryByte | Convert.ToByte(this.Carry));

			this.Carry = newCarryValue;

			this.Zero = memoryByte == 0;
			this.Negative = memoryByte.GetBitAt(SIGN_BIT_INDEX);

			this.emulator.Write(address, memoryByte);
		}

		private void RotateRightOnAccumulator()
		{
			var newCarryValue = this.Accumulator.IsFirstBitSet;

			this.Accumulator.RotateRight((byte)(this.Carry ? 0x80 : 0x00));

			this.Carry = newCarryValue;

			this.Zero = this.Accumulator.IsValueEqualZero;
			this.Negative = this.Accumulator.IsSignBitSet;
		}

		private void RotateRightOnMemory(MemoryAddress address)
		{
			var memoryByte = this.emulator.Read(address);
			var newCarryValue = memoryByte.GetBitAt(FIRST_BIT_INDEX);

			memoryByte = (byte)(memoryByte >> 1);
			memoryByte = (byte)(memoryByte | (this.Carry ? 0x80 : 0x00));

			this.Carry = newCarryValue;

			this.Zero = memoryByte == 0;
			this.Negative = memoryByte.GetBitAt(SIGN_BIT_INDEX);

			this.emulator.Write(address, memoryByte);
		}

		private void SetCarryFlag()
		{
			this.Carry = true;
		}

		private void SetDecimalFlag()
		{
			this.Decimal = true;
		}

		private void SetInterruptFlag()
		{
			this.Interrupt = true;
		}

		private void SubtractWithCarry(MemoryAddress address)
		{
			this.SubtractWithCarry(this.emulator.Read(address));
		}

		private void SubtractWithCarry(byte byteToSubtract)
		{
			var result = this.Decimal
				? this.Accumulator.ToBcd() - byteToSubtract.ConvertToBcd() - Convert.ToInt32(!this.Carry)
				: this.Accumulator.Value - byteToSubtract - Convert.ToInt32(!this.Carry);

			this.Overflow = this.Decimal
				? result < 0
				: result > MAX_POSITIVE_VALUE || result < MAX_NEGATIVE_VALUE;

			this.Carry = result >= 0;
			this.Negative = ((byte)result).GetBitAt(SIGN_BIT_INDEX);
			this.Zero = result == 0;

			this.Accumulator.Value = (byte)(result & 0xff);
		}

		private void TestBitsInAccumulator(MemoryAddress address)
		{
			var memoryValue = this.emulator.Read(address);
			var testResult = (byte)(this.Accumulator.Value & memoryValue);

			this.Negative = testResult.GetBitAt(SIGN_BIT_INDEX);
			this.Overflow = testResult.GetBitAt(OVERFLOW_BIT_INDEX);
			this.Zero = testResult == 0;
		}

		private void TransferFromAccumulatorToX()
		{
			this.X.Value = this.Accumulator.Value;
		}

		private void TransferFromAccumulatorToY()
		{
			this.Y.Value = this.Accumulator.Value;
		}

		private void TransferFromStackPointerToX()
		{
			this.X.Value = this.StackPointer.CurrentOffset;
			this.Negative = this.X.IsSignBitSet;
			this.Zero = this.X.IsValueEqualZero;
		}

		private void TransferFromXToAccumulator()
		{
			this.Accumulator.Value = this.X.Value;
		}

		private void TransferFromXToStackPointer()
		{
			this.StackPointer.CurrentOffset = this.X.Value;
		}

		private void TransferFromYToAccumulator()
		{
			this.Accumulator.Value = this.Y.Value;
		}

		private byte ReadProgramByte()
		{
			var page = (byte)(this.ProgramCounter >> 8);
			var offset = (byte)(this.ProgramCounter & 0xff);
			var value = this.emulator.Read(page, offset);

			this.ProgramCounter++;

			return value;
		}

		private void UpdateFlagsWith(byte value)
		{
			this.Zero = value == 0x00;
			this.Negative = (value & 0x80) == 0x80;
		}
	}
}