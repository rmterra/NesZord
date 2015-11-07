using NesZord.Core;
using Ploeh.AutoFixture;

namespace NesZord.Tests
{
	public class MemoryMock : Memory
	{
		private static readonly Fixture fixture = new Fixture();

		public void MockIndexedIndirectMemoryWrite(byte offset, byte xRegisterValue, byte value)
		{
			var location = GetIndexedIndirectLocation(offset, xRegisterValue);
            this.Write(location, value);
		}

		public MemoryLocation GetIndexedIndirectLocation(byte offset, byte xRegisterValue)
		{
			var indirectPage = fixture.Create<byte>();
			var indirectOffset = fixture.Create<byte>();

			var computedOffset = (byte)(xRegisterValue + offset);
			this.Write(Memory.ZERO_PAGE, computedOffset, indirectPage);
			this.Write(Memory.ZERO_PAGE, (byte)(computedOffset + 1), indirectOffset);

			return new MemoryLocation(indirectOffset, indirectPage);
		}

		public void MockIndirectIndexedMemoryWrite(byte offset, byte yRegisterValue, byte value)
		{
			var location = this.GetIndirectIndexedLocation(offset, yRegisterValue);
			this.Write(location, value);
		}

		public MemoryLocation GetIndirectIndexedLocation(byte offset, byte yRegisterValue)
		{
			var indirectPage = fixture.Create<byte>();
			var indirectOffset = fixture.Create<byte>();

			this.Write(Memory.ZERO_PAGE, offset, indirectPage);
			this.Write(Memory.ZERO_PAGE, (byte)(offset + 1), indirectOffset);

			return new MemoryLocation(indirectOffset, (byte)(indirectPage + yRegisterValue));
		}
	}
}