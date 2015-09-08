using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace NesZord.Core
{
	public class Microprocessor
	{
		private const int PROGRAM_ROM_START = 0x0600;

		private readonly byte[][] memory;

		private readonly Dictionary<OpCode, Action> operations;

		public Microprocessor()
		{
			this.memory = new byte[Byte.MaxValue][];
			for (int i = 0; i < Byte.MaxValue; i++) { this.memory[i] = new byte[Byte.MaxValue]; }

			this.operations = new Dictionary<OpCode, Action>
			{
				{ OpCode.SetCarryFlag, this.SetCarryFlag },
				{ OpCode.ImmediateAddWithCarry, this.AddWithCarry },
				{ OpCode.TransferFromXToAccumulator, this.TransferFromXToAccumulator },
				{ OpCode.AbsoluteStoreYRegister, this.StoreYRegister },
				{ OpCode.AbsoluteStoreAccumulator, this.AbsoluteStoreAccumulator },
				{ OpCode.AbsoluteStoreXRegister, this.StoreXRegister },
				{ OpCode.BranchIfCarryIsClear, this.BranchIfCarryIsClear },
				{ OpCode.AbsoluteXStoreAccumulator, this.AbsoluteXStoreAccumulator },
				{ OpCode.AbsoluteYStoreAccumulator, this.AbsoluteYStoreAccumulator },
				{ OpCode.ImmediateLoadYRegister, this.LoadYRegister },
				{ OpCode.ImmediateLoadXRegister, this.LoadXRegister },
				{ OpCode.ImmediateLoadAccumulator, this.LoadAccumulator },
				{ OpCode.TransferFromAccumulatorToX, this.TransferFromAccumulatorToX },
				{ OpCode.BranchIfCarryIsSet, this.BranchIfCarryIsSet },
				{ OpCode.DecrementValueAtX, this.DecrementValueAtX },
				{ OpCode.BranchIfNotEqual, this.BranchIfNotEqual },
				{ OpCode.BranchIfEqual, this.BranchIfEqual },
				{ OpCode.ImmediateCompareYRegister, this.CompareYRegister },
				{ OpCode.ImmediateCompareXRegister, this.CompareXRegister },
				{ OpCode.IncrementValueAtY, this.IncrementValueAtY },
				{ OpCode.IncrementValueAtX, this.IncrementValueAtX },
				{ OpCode.ImmediateSubtractWithCarry, this.ImmediateSubtractWithCarry }
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
			this.ProgramCounter = PROGRAM_ROM_START;

			byte page = PROGRAM_ROM_START >> 8;
			byte offset = PROGRAM_ROM_START & 0xff;

			for (int i = 0; i < program.Length; i++)
			{
				memory[page][offset] = program[i];
				offset++;
			}
		}

		private void ProcessProgramByteWhileNotBreak()
		{
			var receivedOpCode = (OpCode)this.ReadProgramByte();
			if (receivedOpCode == OpCode.Break) { return; }

			if (this.operations.ContainsKey(receivedOpCode) == false)
			{
				throw new InvalidOperationException(String.Format(CultureInfo.InvariantCulture, "unknown opcode {0}", receivedOpCode));
			}

			this.operations[receivedOpCode]();
			this.ProcessProgramByteWhileNotBreak();
		}

		private void SetCarryFlag()
		{
			this.Carry = true;
		}

		private void AddWithCarry()
		{
			var result = this.Accumulator + this.ReadProgramByte();
			this.Accumulator = (byte)(result & 0xff);
			this.Carry = (result >> 8) > 0;
		}

		private void TransferFromXToAccumulator()
		{
			this.Accumulator = this.X;
		}

		private void StoreYRegister()
		{
			var offset = this.ReadProgramByte();
			var page = this.ReadProgramByte();
			this.memory[page][offset] = this.Y;
		}

		private void AbsoluteYStoreAccumulator()
		{
			var offset = (byte)(this.ReadProgramByte() + this.Y);
			var page = this.ReadProgramByte();
			this.StoreAccumulator(page, offset);
		}

		private void AbsoluteXStoreAccumulator()
		{
			var offset = (byte)(this.ReadProgramByte() + this.X);
			var page = this.ReadProgramByte();
			this.StoreAccumulator(page, offset);
		}

		private void AbsoluteStoreAccumulator()
		{
			var offset = this.ReadProgramByte();
			var page = this.ReadProgramByte();
			this.StoreAccumulator(page, offset);
		}

		private void StoreAccumulator(byte page, byte offset)
		{
			this.memory[page][offset] = this.Accumulator;
		}

		private void StoreXRegister()
		{
			byte offset = this.ReadProgramByte();
			byte page = this.ReadProgramByte();
			this.memory[page][offset] = this.X;
		}

		private void BranchIfCarryIsClear()
		{
			this.BranchIfConditionIsNotSatisfied(() => this.Carry);
		}

		private void LoadYRegister()
		{
			this.Y = this.ReadProgramByte();
		}

		private void LoadXRegister()
		{
			this.X = this.ReadProgramByte();
		}

		private void LoadAccumulator()
		{
			this.Accumulator = this.ReadProgramByte();
		}

		private void TransferFromAccumulatorToX()
		{
			this.X = this.Accumulator;
		}

		private void BranchIfCarryIsSet()
		{
			this.BranchIfConditionIsNotSatisfied(() => this.Carry == false);
		}

		private void DecrementValueAtX()
		{
			this.X -= 1;
		}

		private void BranchIfNotEqual()
		{
			this.BranchIfConditionIsNotSatisfied(() => this.Zero);
		}

		private void BranchIfEqual()
		{
			this.BranchIfConditionIsNotSatisfied(() => this.Zero == false);
		}

		private void BranchIfConditionIsNotSatisfied(Func<bool> condition)
		{
			if (condition.Invoke()) { this.ReadProgramByte(); return; }

			var memoryPage = (byte)(this.ProgramCounter >> 8);
			var memoryOffset = (byte)(this.ProgramCounter & 0xff);
			var branchOffset = this.ValueAt(memoryPage, memoryOffset);

			var offset = 0xff ^ branchOffset;
			this.ProgramCounter -= offset;
		}

		private void CompareYRegister()
		{
			int result = this.Y - this.ReadProgramByte();
			this.Carry = result >= 0;
			this.Zero = result == 0;
		}

		private void CompareXRegister()
		{
			int result = this.X - this.ReadProgramByte();
			this.Carry = result >= 0;
			this.Zero = result == 0;
		}

		private void IncrementValueAtY()
		{
			this.Y += 1;
		}

		private void IncrementValueAtX()
		{
			this.X += 1;
		}

		private void ImmediateSubtractWithCarry()
		{
			var result = this.Accumulator - this.ReadProgramByte();
			this.Accumulator = (byte)(result & 0xff);
			this.Carry = (result << 8) > 0;
		}

		private byte ReadProgramByte()
		{
			var page = (byte)(this.ProgramCounter >> 8);
			var offset = (byte)(this.ProgramCounter & 0xff);
			var value = this.ValueAt(page, offset);

			this.ProgramCounter++;

			return value;
		}

		public byte ValueAt(int page, int offset)
		{
			return this.memory[page][offset];
		}
	}
}