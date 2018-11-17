using NesZord.Core;
using NesZord.Tests.Memory;

namespace NesZord.Tests.OpCodes
{
	public abstract class When_process_opcode_should
	{
		public When_process_opcode_should()
		{
			this.Emulator = new EmulatorMock();
			this.Cpu = new Cpu(this.Emulator);
		}

		protected EmulatorMock Emulator { get; }

		protected Cpu Cpu { get; }

		protected abstract void RunProgram();
	}
}
