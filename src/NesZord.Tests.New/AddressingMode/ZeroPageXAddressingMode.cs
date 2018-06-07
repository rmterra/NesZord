using AutoFixture;

namespace NesZord.Tests.New.AddressingMode
{
	public class ZeroPageXAddressingMode : IAddressingMode
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
			=> memory.WriteZeroPage((byte)(this.XRegisterValue + this.RandomOffset), value);
	}
}
