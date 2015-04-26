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

		public void Start(byte[] program)
		{
			this.ProgramCounter = 2;
			this.Accumulator = program[1];
		}
	}
}