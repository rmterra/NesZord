using NesZord.Core;
using NesZord.Tests.New.AddressingMode;
using System;

namespace NesZord.Tests.New.OpCodes
{
	public abstract class When_process_opcode_should
	{
		public When_process_opcode_should()
		{
			this.Memory = new MemoryMock();
			this.Processor = new Microprocessor(this.Memory);
		}

		protected MemoryMock Memory { get; }

		protected Microprocessor Processor { get; }

		protected abstract void RunProgram();
	}
}
