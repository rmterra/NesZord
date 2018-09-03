using NesZord.Core;
using NesZord.Tests.Memory;
using System;

namespace NesZord.Tests.AddressingMode
{
	public class ImmediateAddressingMode : IAddressingMode
	{
		private readonly OpCode opCode;

		private Cpu cpu;

		public ImmediateAddressingMode(OpCode opCode)
		{
			this.opCode = opCode;
		}

		public byte OperationByte { get; set; }

		public void Initialize(Cpu cpu, MemoryMock memory)
		{
			this.cpu = cpu ?? throw new ArgumentNullException(nameof(cpu));
		}

		public void RunProgram()
			=> this.cpu.RunProgram(new byte[] { (byte)this.opCode, this.OperationByte });
	}
}
