using NesZord.Core;
using NesZord.Tests.Memory;

namespace NesZord.Tests.OpCodes
{
	public abstract class When_process_opcode_should
	{
		public When_process_opcode_should()
		{
			this.Memory = new MemoryMock();
			this.Cpu = new Cpu(this.Memory);
		}

		protected MemoryMock Memory { get; }

		protected Cpu Cpu { get; }

		protected abstract void RunProgram();
	}
}
