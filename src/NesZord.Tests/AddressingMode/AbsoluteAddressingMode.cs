using AutoFixture;
using NesZord.Core;
using NesZord.Tests.Memory;
using System;

namespace NesZord.Tests.AddressingMode
{
	public class AbsoluteAddressingMode : IAddressingMode
	{
		private static Fixture fixture = new Fixture();

		private Cpu cpu;

		private MemoryMock memory;

		private OpCode opCode;

		public AbsoluteAddressingMode(OpCode opCode)
		{
			this.opCode = opCode;
		}

		public byte OperationByte
		{
			get => this.memory.Read(this.RandomOffset, this.RandomPage);
			set => this.memory.Write(this.RandomOffset, this.RandomPage, value);
		}

		public byte RandomOffset { get; private set; }

		public byte RandomPage { get; private set; }

		public void Initialize(Cpu cpu, MemoryMock memory)
		{
			this.cpu = cpu ?? throw new ArgumentNullException(nameof(cpu));
			this.memory = memory ?? throw new ArgumentNullException(nameof(memory));

			this.RandomOffset = fixture.Create<byte>();
			this.RandomPage = fixture.Create<byte>();
		}

		public void RunProgram()
			=> this.cpu.RunProgram(new byte[] { (byte)this.opCode, this.RandomOffset, this.RandomPage });
	}
}
