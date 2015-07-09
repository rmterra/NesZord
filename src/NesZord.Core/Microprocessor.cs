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

		public Microprocessor()
		{
			this.memory = new byte[Byte.MaxValue][];
			for (int i = 0; i < Byte.MaxValue; i++) { this.memory[i] = new byte[Byte.MaxValue]; }
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
			LoadProgramToMemory(program);
			RunProgram();
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

		private void RunProgram()
		{
			OpCode receivedOpCode = OpCode.Break;

			while ((receivedOpCode = (OpCode)this.ReadProgramByte()) != OpCode.Break)
			{
				if (receivedOpCode == OpCode.ImmediateLoadAccumulator) { this.Accumulator = this.ReadProgramByte(); }
				else if (receivedOpCode == OpCode.ImmediateLoadXRegister) { this.X = this.ReadProgramByte(); }
				else if (receivedOpCode == OpCode.TransferFromAccumulatorToX) { this.X = this.Accumulator; }
				else if (receivedOpCode == OpCode.IncrementValueAtX) { this.X += 1; }
				else if (receivedOpCode == OpCode.DecrementValueAtX) { this.X -= 1; }
				else if (receivedOpCode == OpCode.AbsoluteStoreAccumulator)
				{
					byte offset = this.ReadProgramByte();
					byte page = this.ReadProgramByte();
					this.memory[page][offset] = this.Accumulator;
				}
				else if (receivedOpCode == OpCode.AbsoluteStoreXRegister)
				{
					byte offset = this.ReadProgramByte();
					byte page = this.ReadProgramByte();
					this.memory[page][offset] = this.X;
				}
				else if (receivedOpCode == OpCode.ImmediateAddWithCarry)
				{
					int result = this.Accumulator + this.ReadProgramByte();
					this.Accumulator = (byte)(result & 0xff);
					this.Carry = (result >> 8) > 0;
				}
				else if (receivedOpCode == OpCode.ImmediateCompareXRegister)
				{
					int result = this.X - this.ReadProgramByte();
					this.Carry = result >= 0;
					this.Zero = result == 0;
				}
				else if (receivedOpCode == OpCode.BranchIfNotEqual)
				{
					if (this.Zero)
					{
						this.ReadProgramByte();
						continue;
					}

					byte memoryPage = (byte)(this.ProgramCounter >> 8);
					byte memoryOffset = (byte)(this.ProgramCounter & 0xff);
					byte branchOffset = this.ValueAt(memoryPage, memoryOffset);

					int offset = 0xff ^ branchOffset;
					this.ProgramCounter -= offset;
				}
				else
				{
					String error = String.Format(CultureInfo.InvariantCulture, "unknown opcode {0}", receivedOpCode);
					throw new InvalidOperationException(error);
				}
			}
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