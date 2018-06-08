using AutoFixture;
using NesZord.Core;
using System;

namespace NesZord.Tests.New.AddressingMode
{
	public class AbsoluteXAddressingMode : IAddressingMode
	{
		private static Fixture fixture = new Fixture();

		private MemoryLocation memoryLocation;

		private MemoryMock memory;

		public byte OperationByte
		{
			get => this.memory.Read(this.memoryLocation);
			set => this.memory.Write(this.memoryLocation, value);
		}

		public byte RandomOffset { get; private set; }

		public byte RandomPage { get; private set; }

		public byte XRegisterValue { get; private set; }

		public void Initialize(Microprocessor processor, MemoryMock memory)
		{
			this.memory = memory ?? throw new ArgumentNullException(nameof(memory));

			this.RandomOffset = fixture.Create<byte>();
			this.RandomPage = fixture.Create<byte>();
			this.XRegisterValue = fixture.Create<byte>();

			this.memoryLocation = new MemoryLocation(this.RandomOffset, this.RandomPage).Sum(this.XRegisterValue);
		}
	}
}
