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
				{ OpCode.ImmediateAddWithCarry, this.AddWithCarry },
				{ OpCode.AbsoluteStoreAccumulator, this.StoreAccumulator },
				{ OpCode.AbsoluteStoreXRegister, this.StoreXRegister },
				{ OpCode.ImmediateLoadXRegister, this.LoadXRegister },
				{ OpCode.ImmediateLoadAccumulator, this.LoadAccumulator },
				{ OpCode.TransferFromAccumulatorToX, this.TransferFromAccumulatorToX },
				{ OpCode.DecrementValueAtX, this.DecrementValueAtX },
				{ OpCode.BranchIfNotEqual, this.BranchIfNotEqual },
				{ OpCode.ImmediateCompareXRegister, this.CompareXRegister },
				{ OpCode.IncrementValueAtX, this.IncrementValueAtX }
			};
		}

		public bool Carry { get; private set; }

		public bool Zero { get; private set; }

		public byte X { get; private set; }

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
			OpCode receivedOpCode = (OpCode)this.ReadProgramByte();
			if (receivedOpCode == OpCode.Break) { return; }

			if (this.operations.ContainsKey(receivedOpCode) == false)
			{
				String error = String.Format(CultureInfo.InvariantCulture, "unknown opcode {0}", receivedOpCode);
				throw new InvalidOperationException(error);
			}

			this.operations[receivedOpCode]();
			this.ProcessProgramByteWhileNotBreak();
		}

		private void AddWithCarry()
		{
			int result = this.Accumulator + this.ReadProgramByte();
			this.Accumulator = (byte)(result & 0xff);
			this.Carry = (result >> 8) > 0;
		}

		private void StoreAccumulator()
		{
			byte offset = this.ReadProgramByte();
			byte page = this.ReadProgramByte();
			this.memory[page][offset] = this.Accumulator;
		}

		private void StoreXRegister()
		{
			byte offset = this.ReadProgramByte();
			byte page = this.ReadProgramByte();
			this.memory[page][offset] = this.X;
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

		private void DecrementValueAtX()
		{
			this.X -= 1;
		}

		private void BranchIfNotEqual()
		{
			if (this.Zero)
			{
				this.ReadProgramByte();
				return;
			}

			byte memoryPage = (byte)(this.ProgramCounter >> 8);
			byte memoryOffset = (byte)(this.ProgramCounter & 0xff);
			byte branchOffset = this.ValueAt(memoryPage, memoryOffset);

			int offset = 0xff ^ branchOffset;
			this.ProgramCounter -= offset;
		}

		private void CompareXRegister()
		{
			int result = this.X - this.ReadProgramByte();
			this.Carry = result >= 0;
			this.Zero = result == 0;
		}

		private void IncrementValueAtX()
		{
			this.X += 1;
		}

		private byte ReadProgramByte()
		{
			byte page = (byte)(this.ProgramCounter >> 8);
			byte offset = (byte)(this.ProgramCounter & 0xff);
			byte value = this.ValueAt(page, offset);

			this.ProgramCounter++;

			return value;
		}

		public byte ValueAt(int page, int offset)
		{
			return this.memory[page][offset];
		}
	}
}