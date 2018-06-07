using AutoFixture;
using NesZord.Core;

namespace NesZord.Tests.New.AddressingMode
{
	public class AbsoluteAddressingMode : IAddressingMode
	{
		private static Fixture fixture = new Fixture();

		public byte RandomOffset { get; private set; }

		public byte RandomPage { get; private set; }

		public void Initialize()
		{
			this.RandomOffset = fixture.Create<byte>();
			this.RandomPage = fixture.Create<byte>();
		}

		public void SetOperationByte(MemoryMock memory, byte value)
			=> memory.Write(this.RandomOffset, this.RandomPage, value);
	}
}
