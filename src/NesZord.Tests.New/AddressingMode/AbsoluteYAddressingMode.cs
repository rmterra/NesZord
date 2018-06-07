using AutoFixture;
using NesZord.Core;

namespace NesZord.Tests.New.AddressingMode
{
	public class AbsoluteYAddressingMode : IAddressingMode
	{
		private static Fixture fixture = new Fixture();

		public byte RandomOffset { get; private set; }

		public byte RandomPage { get; private set; }

		public byte YRegisterValue { get; private set; }

		public void Initialize()
		{
			this.RandomOffset = fixture.Create<byte>();
			this.RandomPage = fixture.Create<byte>();
			this.YRegisterValue = fixture.Create<byte>();
		}

		public void SetOperationByte(MemoryMock memory, byte value)
			=> memory.Write(new MemoryLocation(this.RandomOffset, this.RandomPage).Sum(this.YRegisterValue), value);
	}
}
