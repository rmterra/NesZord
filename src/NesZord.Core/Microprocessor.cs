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
			if (memory == null) { throw new ArgumentNullException("memory"); }
			
			this.memory = memory;

			this.addressedOperations = new Dictionary<OpCode, Action<MemoryLocation>>
			{
				{ OpCode.AbsoluteStoreAccumulator, this.StoreAccumulator },
				{ OpCode.AbsoluteXStoreAccumulator, this.StoreAccumulator },
				{ OpCode.AbsoluteYStoreAccumulator, this.StoreAccumulator },
				{ OpCode.AbsoluteStoreXRegister, this.StoreXRegister },
				{ OpCode.AbsoluteStoreYRegister, this.StoreYRegister }
			};

			this.unaddressedOperations = new Dictionary<OpCode, Action>
			{
				{ OpCode.BranchIfCarryIsClear, this.BranchIfCarryIsClear },
				{ OpCode.BranchIfCarryIsSet, this.BranchIfCarryIsSet },
				{ OpCode.BranchIfEqual, this.BranchIfEqual },
				{ OpCode.BranchIfNotEqual, this.BranchIfNotEqual },
				{ OpCode.DecrementValueAtX, this.DecrementValueAtX },
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
				{ OpCode.TransferFromXToAccumulator, this.TransferFromXToAccumulator }
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
			var result = this.Accumulator + this.ReadProgramByte();
			this.Accumulator = (byte)(result & 0xff);
			this.Carry = (result >> 8) > 0;
		}

		private void ImmediateCompareXRegister()
		{
			int result = this.X - this.ReadProgramByte();
			this.Carry = result >= 0;
			this.Zero = result == 0;
		}

		private void ImmediateCompareYRegister()
		{
			int result = this.Y - this.ReadProgramByte();
			this.Carry = result >= 0;
			this.Zero = result == 0;
		}

		private void ImmediateLoadAccumulator()
		{
			this.Accumulator = this.ReadProgramByte();
		}

		private void ImmediateLoadXRegister()
		{
			this.X = this.ReadProgramByte();
		}

		private void ImmediateLoadYRegister()
		{
			this.Y = this.ReadProgramByte();
		}

		private void ImmediateSubtractWithCarry()
		{
			var result = this.Accumulator - this.ReadProgramByte();
			this.Accumulator = (byte)(result & 0xff);
			this.Carry = (result << 8) > 0;
		}

		private void SetCarryFlag()
		{
			this.Carry = true;
		}

		private void TransferFromAccumulatorToX()
		{
			this.X = this.Accumulator;
		}

		private void TransferFromXToAccumulator()
		{
			this.Accumulator = this.X;
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