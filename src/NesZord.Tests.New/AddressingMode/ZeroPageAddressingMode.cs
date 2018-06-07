using AutoFixture;

namespace NesZord.Tests.New.AddressingMode
{
	public class ZeroPageAddressingMode : IAddressingMode
	{
		private static Fixture fixture = new Fixture();

		public byte RandomOffset { get; private set; }

		public void Initialize()
			=> this.RandomOffset = fixture.Create<byte>();

		public void SetOperationByte(MemoryMock memory, byte value)
			=> memory.WriteZeroPage(this.RandomOffset, value);
	}
}
