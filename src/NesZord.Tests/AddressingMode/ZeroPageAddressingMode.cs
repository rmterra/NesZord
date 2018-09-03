using AutoFixture;
using NesZord.Core;
using NesZord.Tests.Memory;
using System;

namespace NesZord.Tests.AddressingMode
{
	public class ZeroPageAddressingMode : IAddressingMode
	{
		private static Fixture fixture = new Fixture();

		private readonly OpCode opCode;

		private Cpu cpu;

		private MemoryMock memory;

		public ZeroPageAddressingMode(OpCode opCode)
		{
			this.opCode = opCode;
		}

		public byte OperationByte
		{
			get => this.memory.Read(this.RandomOffset, Core.Memory.ZERO_PAGE);
			set => this.memory.WriteZeroPage(this.RandomOffset, value);
		}

		public byte RandomOffset { get; private set; }

		public void Initialize(Cpu cpu, MemoryMock memory)
		{
			this.cpu = cpu ?? throw new ArgumentNullException(nameof(cpu));
			this.memory = memory ?? throw new ArgumentNullException(nameof(memory));

			this.RandomOffset = fixture.Create<byte>();
		}

		public void RunProgram()
			=> this.cpu.RunProgram(new byte[] { (byte)this.opCode, this.RandomOffset });
	}
}
