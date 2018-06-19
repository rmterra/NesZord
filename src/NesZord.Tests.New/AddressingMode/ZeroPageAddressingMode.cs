using AutoFixture;
using NesZord.Core;
using System;

namespace NesZord.Tests.New.AddressingMode
{
	public class ZeroPageAddressingMode : IAddressingMode
	{
		private static Fixture fixture = new Fixture();

		private readonly OpCode opCode;

		private Microprocessor processor;

		private MemoryMock memory;

		public ZeroPageAddressingMode(OpCode opCode)
		{
			this.opCode = opCode;
		}

		public byte OperationByte
		{
			get => this.memory.Read(this.RandomOffset, Memory.ZERO_PAGE);
			set => this.memory.WriteZeroPage(this.RandomOffset, value);
		}

		public byte RandomOffset { get; private set; }

		public void Initialize(Microprocessor processor, MemoryMock memory)
		{
			this.processor = processor ?? throw new ArgumentNullException(nameof(processor));
			this.memory = memory ?? throw new ArgumentNullException(nameof(memory));

			this.RandomOffset = fixture.Create<byte>();
		}

		public void RunProgram()
			=> this.processor.RunProgram(new byte[] { (byte)this.opCode, this.RandomOffset });
	}
}
