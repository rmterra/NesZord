using AutoFixture;

namespace NesZord.Tests.New.AddressingMode
{
	public class IndexedIndirectAddressingMode : IAddressingMode
	{
		private static Fixture fixture = new Fixture();

		public byte RandomOffset { get; private set; }

		public byte XRegisterValue { get; private set; }

		public void Initialize()
		{
			this.RandomOffset = fixture.Create<byte>();
			this.XRegisterValue = fixture.Create<byte>();
		}

		public void SetOperationByte(MemoryMock memory, byte value)
			=> memory.MockIndexedIndirectMemoryWrite(this.RandomOffset, this.XRegisterValue, value);
	}
}
