using NesZord.Core;
using Ploeh.AutoFixture;

namespace NesZord.Tests
{
	public class MemoryMock : Memory
	{
		private static readonly Fixture fixture = new Fixture();

		public void MockIndexedIndirectMemoryWrite(byte offset, byte xRegisterValue, byte value)
		{
			var indirectPage = fixture.Create<byte>();
			var indirectOffset = fixture.Create<byte>();

			var computedOffset = (byte)(xRegisterValue + offset);
			this.Write(Memory.ZERO_PAGE, computedOffset, indirectPage);
			this.Write(Memory.ZERO_PAGE, (byte)(computedOffset + 1), indirectOffset);
			this.Write(indirectPage, indirectOffset, value);
		}

		public void MockIndirectIndexedMemoryWrite(byte offset, byte yRegisterValue, byte value)
		{
			var indirectPage = fixture.Create<byte>();
			var indirectOffset = fixture.Create<byte>();

			this.Write(Memory.ZERO_PAGE, offset, indirectPage);
			this.Write(Memory.ZERO_PAGE, (byte)(offset + 1), indirectOffset);
			this.Write((byte)(indirectPage + yRegisterValue), indirectOffset, value);
		}
	}
}