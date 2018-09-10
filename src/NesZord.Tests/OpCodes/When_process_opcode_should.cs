using NesZord.Core;
using NesZord.Tests.Memory;

namespace NesZord.Tests.OpCodes
{
	public abstract class When_process_opcode_should
	{
		public When_process_opcode_should()
		{
			this.Memory = new MemoryMapperMock();
			this.Cpu = new Cpu(this.Memory);
		}

		protected MemoryMapperMock Memory { get; }

		protected Cpu Cpu { get; }

		protected abstract void RunProgram();
	}
}
