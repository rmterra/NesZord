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
			this.Write(computedOffset, Memory.ZERO_PAGE, indirectPage);
			this.Write((byte)(computedOffset + 1), Memory.ZERO_PAGE, indirectOffset);

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

			this.Write(offset, Memory.ZERO_PAGE, indirectPage);
			this.Write((byte)(offset + 1), Memory.ZERO_PAGE, indirectOffset);

			return new MemoryLocation(indirectOffset, (byte)(indirectPage + yRegisterValue));
		}

		public void MockIndirectMemoryWrite(byte offset, byte value)
		{
			var location = GetIndirectLocation(offset);
			this.Write(location, value);
		}

		public MemoryLocation GetIndirectLocation(byte offset)
		{
			var indirectPage = fixture.Create<byte>();
			var indirectOffset = fixture.Create<byte>();

			this.Write(offset, Memory.ZERO_PAGE, indirectPage);
			this.Write((byte)(offset + 1), Memory.ZERO_PAGE, indirectOffset);

			return new MemoryLocation(indirectOffset, indirectPage);
		}
	}
}