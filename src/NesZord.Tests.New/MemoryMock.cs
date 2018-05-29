using NesZord.Core;
using AutoFixture;

namespace NesZord.Tests.New
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
			var indirectOffset = fixture.Create<byte>();
			var indirectPage = fixture.Create<byte>();

			var computedOffset = (byte)(xRegisterValue + offset);
			this.WriteZeroPage(computedOffset, indirectOffset);
			this.WriteZeroPage((byte)(computedOffset + 1), indirectPage);

			return new MemoryLocation(indirectOffset, indirectPage);
		}

		public void MockIndirectIndexedMemoryWrite(byte offset, byte yRegisterValue, byte value)
		{
			var location = this.GetIndirectIndexedLocation(offset, yRegisterValue);
			this.Write(location, value);
		}

		public MemoryLocation GetIndirectIndexedLocation(byte offset, byte yRegisterValue)
		{
			var indirectOffset = fixture.Create<byte>();
			var indirectPage = fixture.Create<byte>();

			this.WriteZeroPage(offset, indirectOffset);
			this.WriteZeroPage((byte)(offset + 1), indirectPage);

			return new MemoryLocation(indirectOffset, indirectPage).Sum(yRegisterValue);
		}

		public void MockIndirectMemoryWrite(byte offset, byte value)
		{
			var location = GetIndirectLocation(offset);
			this.Write(location, value);
		}

		public MemoryLocation GetIndirectLocation(byte offset)
		{
			var indirectOffset = fixture.Create<byte>();
			var indirectPage = fixture.Create<byte>();

			this.Write(offset, Memory.ZERO_PAGE, indirectOffset);
			this.Write((byte)(offset + 1), Memory.ZERO_PAGE, indirectPage);

			return new MemoryLocation(indirectOffset, indirectPage);
		}
	}
}