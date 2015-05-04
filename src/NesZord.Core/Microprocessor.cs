using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NesZord.Core
{
	public class Microprocessor
	{
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
				else if (receivedOpCode == OpCode.AbsoluteStoreAccumulator)
				{
					this.ProgramCounter += 3;
				}
				else
				{
					this.ProgramCounter += 1;
					this.X = this.Accumulator;
				}
			}
		}

		public object ValueAt(int p1, int p2)
		{
			return this.Accumulator;
		}
	}
}