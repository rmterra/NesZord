using AutoFixture;
using NesZord.Core;
using System;

namespace NesZord.Tests.New.AddressingMode
{
	public class ZeroPageXAddressingMode : IAddressingMode
	{
		private static Fixture fixture = new Fixture();

		private MemoryMock memory;

		public byte OperationByte
		{
			get => this.memory.Read((byte)(this.XRegisterValue + this.RandomOffset), Memory.ZERO_PAGE);
			set => this.memory.WriteZeroPage((byte)(this.XRegisterValue + this.RandomOffset), value);
		}

		public byte RandomOffset { get; private set; }

		public byte XRegisterValue { get; private set; }

		public void Initialize(Microprocessor processor, MemoryMock memory)
		{
			this.memory = memory ?? throw new ArgumentNullException(nameof(memory));

			this.RandomOffset = fixture.Create<byte>();
			this.XRegisterValue = fixture.Create<byte>();
		}
	}
}
