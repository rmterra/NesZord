using AutoFixture;

namespace NesZord.Tests.New.AddressingMode
{
	public class IndirectIndexedAddressingMode : IAddressingMode
	{
		private static Fixture fixture = new Fixture();

		public byte RandomOffset { get; private set; }

		public byte YRegisterValue { get; private set; }

		public void Initialize()
		{
			this.RandomOffset = fixture.Create<byte>();
			this.YRegisterValue = fixture.Create<byte>();
		}

		public void SetOperationByte(MemoryMock memory, byte value)
			=> memory.MockIndirectIndexedMemoryWrite(this.RandomOffset, this.YRegisterValue, value);
	}
}
