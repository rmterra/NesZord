using AutoFixture;
using NesZord.Core;
using System;

namespace NesZord.Tests.New.AddressingMode
{
	public class AbsoluteAddressingMode : IAddressingMode
	{
		private static Fixture fixture = new Fixture();

		private MemoryMock memory;

		public byte OperationByte
		{
			get => this.memory.Read(this.RandomOffset, this.RandomPage);
			set => this.memory.Write(this.RandomOffset, this.RandomPage, value);
		}

		public byte RandomOffset { get; private set; }

		public byte RandomPage { get; private set; }

		public void Initialize(Microprocessor processor, MemoryMock memory)
		{
			this.memory = memory ?? throw new ArgumentNullException(nameof(memory));

			this.RandomOffset = fixture.Create<byte>();
			this.RandomPage = fixture.Create<byte>();
		}
	}
}
