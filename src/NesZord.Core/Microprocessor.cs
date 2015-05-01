using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NesZord.Core
{
	public class Microprocessor
	{
		public byte Accumulator { get; private set; }

		public int ProgramCounter { get; private set; }

		private byte[] program;

		public void Start(IEnumerable<byte> program)
		{
			this.Start(program.ToArray());
		}

		public void Start(byte[] program)
		{
			OpCode receivedOpCode = (OpCode)program[0];

			if (receivedOpCode == OpCode.ImmediateLoadAccumulator)
			{
				this.ProgramCounter = 2;
				this.Accumulator = program[1];
			}

			if (program.Length <= this.ProgramCounter) { return; }

			receivedOpCode = (OpCode)program[this.ProgramCounter];
			this.ProgramCounter += 3;
		}

		public object ValueAt(int p1, int p2)
		{
			return this.Accumulator;
		}
	}
}