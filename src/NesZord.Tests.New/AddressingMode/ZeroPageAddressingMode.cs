using AutoFixture;
using NesZord.Core;
using System;

namespace NesZord.Tests.New.AddressingMode
{
	public class ZeroPageAddressingMode : IAddressingMode
	{
		private static Fixture fixture = new Fixture();

		private MemoryMock memory;

		public byte OperationByte
		{
			get => this.memory.Read(this.RandomOffset, Memory.ZERO_PAGE);
			set => this.memory.WriteZeroPage(this.RandomOffset, value);
		}

		public byte RandomOffset { get; private set; }

		public void Initialize(Microprocessor processor, MemoryMock memory)
		{
			this.memory = memory ?? throw new ArgumentNullException(nameof(memory));

			this.RandomOffset = fixture.Create<byte>();
		}
	}
}
