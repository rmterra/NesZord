using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace NesZord.Core
{
	public class Microprocessor
	{
		private readonly Dictionary<OpCode, Action<MemoryLocation>> addressedOperations;

		private readonly Dictionary<OpCode, Action> unaddressedOperations;

		private readonly Memory memory;

		public Microprocessor(Memory memory)
		{
			if (memory == null) { throw new ArgumentNullException(nameof(memory)); }
			
			this.memory = memory;

			this.addressedOperations = new Dictionary<OpCode, Action<MemoryLocation>>
			{
				{ OpCode.AbsoluteAddWithCarry, this.AddWithCarry },
				{ OpCode.AbsoluteCompareYRegister, this.CompareYRegister },
				{ OpCode.AbsoluteCompareXRegister, this.CompareXRegister },
				{ OpCode.AbsoluteLoadAccumulator, this.LoadAccumulator },
				{ OpCode.AbsoluteLoadXRegister, this.LoadXRegister },
				{ OpCode.AbsoluteLoadYRegister, this.LoadYRegister },
				{ OpCode.AbsoluteStoreAccumulator, this.StoreAccumulator },
				{ OpCode.AbsoluteSubtractWithCarry, this.SubtractWithCarry },
				{ OpCode.AbsoluteXAddWithCarry, this.AddWithCarry },
				{ OpCode.AbsoluteXLoadAccumulator, this.LoadAccumulator },
				{ OpCode.AbsoluteXLoadYRegister, this.LoadYRegister },
				{ OpCode.AbsoluteXStoreAccumulator, this.StoreAccumulator },
				{ OpCode.AbsoluteXSubtractWithCarry, this.SubtractWithCarry },
				{ OpCode.AbsoluteYAddWithCarry, this.AddWithCarry },
				{ OpCode.AbsoluteYLoadAccumulator, this.LoadAccumulator },
				{ OpCode.AbsoluteYLoadXRegister, this.LoadXRegister },
				{ OpCode.AbsoluteYStoreAccumulator, this.StoreAccumulator },
				{ OpCode.AbsoluteYSubtractWithCarry, this.SubtractWithCarry },
				{ OpCode.AbsoluteStoreXRegister, this.StoreXRegister },
				{ OpCode.AbsoluteStoreYRegister, this.StoreYRegister },
				{ OpCode.IndexedIndirectAddWithCarry, this.AddWithCarry },
				{ OpCode.IndexedIndirectLoadAccumulator, this.LoadAccumulator },
				{ OpCode.IndexedIndirectStoreAccumulator, this.StoreAccumulator },
				{ OpCode.IndexedIndirectSubtractWithCarry, this.SubtractWithCarry },
				{ OpCode.IndirectIndexedAddWithCarry, this.AddWithCarry },
				{ OpCode.IndirectIndexedLoadAccumulator, this.LoadAccumulator },
				{ OpCode.IndirectIndexedStoreAccumulator, this.StoreAccumulator },
				{ OpCode.IndirectIndexedSubtractWithCarry, this.SubtractWithCarry },
                { OpCode.ZeroPageAddWithCarry, this.AddWithCarry },
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
				{ OpCode.ZeroPageXLoadAccumulator, this.LoadAccumulator },
				{ OpCode.ZeroPageXLoadYRegister, this.LoadYRegister },
				{ OpCode.ZeroPageXStoreAccumulator, this.StoreAccumulator },
				{ OpCode.ZeroPageXStoreYRegister, this.StoreYRegister },
				{ OpCode.ZeroPageXSubtractWithCarry, this.SubtractWithCarry },
				{ OpCode.ZeroPageYLoadXRegister, this.LoadXRegister },
                { OpCode.ZeroPageYStoreXRegister, this.StoreXRegister }
			};

			this.unaddressedOperations = new Dictionary<OpCode, Action>
			{
				{ OpCode.BranchIfCarryIsClear, this.BranchIfCarryIsClear },
				{ OpCode.BranchIfCarryIsSet, this.BranchIfCarryIsSet },
				{ OpCode.BranchIfEqual, this.BranchIfEqual },
				{ OpCode.BranchIfNotEqual, this.BranchIfNotEqual },
				{ OpCode.DecrementValueAtX, this.DecrementValueAtX },
				{ OpCode.DecrementValueAtY, this.DecrementValueAtY },
				{ OpCode.IncrementValueAtX, this.IncrementValueAtX },
				{ OpCode.IncrementValueAtY, this.IncrementValueAtY },
				{ OpCode.ImmediateAddWithCarry, this.ImmediateAddWithCarry },
				{ OpCode.ImmediateCompareXRegister, this.ImmediateCompareXRegister },
				{ OpCode.ImmediateCompareYRegister, this.ImmediateCompareYRegister },
				{ OpCode.ImmediateLoadYRegister, this.ImmediateLoadYRegister },
				{ OpCode.ImmediateLoadXRegister, this.ImmediateLoadXRegister },
				{ OpCode.ImmediateLoadAccumulator, this.ImmediateLoadAccumulator },
				{ OpCode.ImmediateSubtractWithCarry, this.ImmediateSubtractWithCarry },
				{ OpCode.SetCarryFlag, this.SetCarryFlag },
				{ OpCode.TransferFromAccumulatorToX, this.TransferFromAccumulatorToX },
				{ OpCode.TransferFromAccumulatorToY, this.TransferFromAccumulatorToY },
				{ OpCode.TransferFromXToAccumulator, this.TransferFromXToAccumulator },
				{ OpCode.TransferFromYToAccumulator, this.TransferFromYToAccumulator }
			};
		}

		public bool Carry { get; private set; }

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

			this.HandleUnknowOpCode(opCode);

			var addressingMode = AddressingModeLookup.For(opCode);
			
			if (this.IsUnaddressedOperations(opCode)) { this.unaddressedOperations[opCode](); }
			else { this.addressedOperations[opCode](this.CreateMemoryLocation(addressingMode)); }

			this.ProcessProgramByteWhileNotBreak();
		}

		private void HandleUnknowOpCode(OpCode opCode)
		{
			if (this.addressedOperations.ContainsKey(opCode) || this.unaddressedOperations.ContainsKey(opCode)) { return; }
			throw new InvalidOperationException(String.Format(CultureInfo.InvariantCulture, "unknown opcode {0}", opCode));
		}

		private bool IsUnaddressedOperations(OpCode opCode)
		{
			return this.unaddressedOperations.ContainsKey(opCode);
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
				var indirectPage = this.memory.Read(Memory.ZERO_PAGE, offset);
				var indirectOffset = this.memory.Read(Memory.ZERO_PAGE, (byte)(offset + 1));
				return new MemoryLocation(indirectOffset, indirectPage);
			}
			else if (addressingMode == AddressingMode.IndirectIndexed)
			{
				offset = this.ReadProgramByte();
				var indirectPage = this.memory.Read(Memory.ZERO_PAGE, offset);
				var indirectOffset = this.memory.Read(Memory.ZERO_PAGE, (byte)(offset + 1));
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
			var branchOffset = this.memory.Read(memoryPage, memoryOffset);

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

		private void ImmediateAddWithCarry()
		{
			this.AddWithCarry(this.ReadProgramByte());
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

		private void ImmediateCompareXRegister()
		{
			this.CompareXRegister(this.ReadProgramByte());
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

		private void ImmediateCompareYRegister()
		{
			this.CompareYRegister(this.ReadProgramByte());
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

		private void ImmediateLoadAccumulator()
		{
			this.LoadAccumulator(this.ReadProgramByte());
		}

		private void LoadAccumulator(MemoryLocation location)
		{
			this.LoadAccumulator(this.memory.Read(location));
		}

		private void LoadAccumulator(byte value)
		{
			this.Accumulator = value;
		}

		private void ImmediateLoadXRegister()
		{
			this.LoadXRegister(this.ReadProgramByte());
		}

		private void LoadXRegister(MemoryLocation location)
		{
			this.LoadXRegister(this.memory.Read(location));
		}

		private void LoadXRegister(byte value)
		{
			this.X = value;
		}

		private void ImmediateLoadYRegister()
		{
			this.LoadYRegister(this.ReadProgramByte());
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

		private void ImmediateSubtractWithCarry()
		{
			this.SubtractWithCarry(this.ReadProgramByte());
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
			var value = this.memory.Read(page, offset);

			this.ProgramCounter++;

			return value;
		}
	}
}