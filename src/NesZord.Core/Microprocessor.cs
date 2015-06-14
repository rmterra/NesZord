using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace NesZord.Core
{
	public class Microprocessor
	{
		private readonly byte[][] memory;

		public Microprocessor()
		{
			this.memory = new byte[Byte.MaxValue][];
			for (int i = 0; i < Byte.MaxValue; i++) { this.memory[i] = new byte[Byte.MaxValue]; }
		}

		public bool Carry { get; private set; }

		public byte X { get; private set; }

		public byte Accumulator { get; private set; }

		public int ProgramCounter { get; private set; }

		public void Start(IEnumerable<byte> program)
		{
			this.Start(program.ToArray());
		}

		public void Start(byte[] program)
		{
			while (program.Length > this.ProgramCounter)
			{
				OpCode receivedOpCode = (OpCode)program[this.ProgramCounter];

				if (receivedOpCode == OpCode.ImmediateLoadAccumulator)
				{
					this.ProgramCounter = 2;
					this.Accumulator = program[1];
				}
				else if (receivedOpCode == OpCode.ImmediateLoadXRegister)
				{
					this.ProgramCounter = 2;
					this.X = program[1];
				}
				else if (receivedOpCode == OpCode.AbsoluteStoreAccumulator)
				{
					byte page = program[this.ProgramCounter + 2];
					byte offset = program[this.ProgramCounter + 1];
					this.memory[page][offset] = this.Accumulator;
					this.ProgramCounter += 3;
				}
				else if (receivedOpCode == OpCode.AbsoluteStoreXRegister)
				{
					byte page = program[this.ProgramCounter + 2];
					byte offset = program[this.ProgramCounter + 1];
					this.memory[page][offset] = this.X;
					this.ProgramCounter += 3;
				}
				else if (receivedOpCode == OpCode.TransferFromAccumulatorToX)
				{
					this.ProgramCounter += 1;
					this.X = this.Accumulator;
				}
				else if (receivedOpCode == OpCode.IncrementValueAtX)
				{
					this.ProgramCounter += 1;
					this.X += 1;
				}
				else if (receivedOpCode == OpCode.DecrementValueAtX)
				{
					this.ProgramCounter += 1;
					this.X -= 1;
				}
				else if (receivedOpCode == OpCode.ImmediateAddWithCarry)
				{
					int result = this.Accumulator + program[this.ProgramCounter + 1];
					this.Accumulator = (byte)(result & 0xff);
					this.Carry = (result >> 8) > 0;
					this.ProgramCounter += 2;
				}
				else if (receivedOpCode == OpCode.Break)
				{
					this.ProgramCounter += 1;
					break;
				}
				else
				{
					String error = String.Format(CultureInfo.InvariantCulture, "unknown opcode {0}", receivedOpCode);
					throw new InvalidOperationException(error);
				}
			}
		}

		public object ValueAt(int page, int offset)
		{
			return this.memory[page][offset];
		}
	}
}