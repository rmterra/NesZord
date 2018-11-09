using AutoFixture;
using NesZord.Core.Memory;

namespace NesZord.Tests.Memory
{
	public class MemoryMapperMock : MemoryMapper
	{
		private static readonly Fixture fixture = new Fixture();

		public void MockIndexedIndirectMemoryWrite(byte offset, byte xRegisterValue, byte value)
		{
			var address = GetIndexedIndirectAddress(offset, xRegisterValue);
            this.Write(address, value);
		}

		public MemoryAddress GetIndexedIndirectAddress(byte offset, byte xRegisterValue)
		{
			var indirectOffset = fixture.Create<byte>();
			var indirectPage = fixture.Create<byte>();

			var computedOffset = (byte)(xRegisterValue + offset);
			this.WriteZeroPage(computedOffset, indirectOffset);
			this.WriteZeroPage((byte)(computedOffset + 1), indirectPage);

			return new MemoryAddress(indirectOffset, indirectPage);
		}

		public void MockIndirectIndexedMemoryWrite(byte offset, byte yRegisterValue, byte value)
		{
			var address = this.GetIndirectIndexedAddress(offset, yRegisterValue);
			this.Write(address, value);
		}

		public MemoryAddress GetIndirectIndexedAddress(byte offset, byte yRegisterValue)
		{
			var indirectOffset = fixture.Create<byte>();
			var indirectPage = fixture.Create<byte>();

			this.WriteZeroPage(offset, indirectOffset);
			this.WriteZeroPage((byte)(offset + 1), indirectPage);

			return new MemoryAddress(indirectOffset, indirectPage).Sum(yRegisterValue);
		}

		public void MockIndirectMemoryWrite(byte offset, byte value)
		{
			var address = GetIndirectAddress(offset);
			this.Write(address, value);
		}

		public MemoryAddress GetIndirectAddress(byte offset)
		{
			var indirectOffset = fixture.Create<byte>();
			var indirectPage = fixture.Create<byte>();

			this.Write(offset, ZERO_PAGE, indirectOffset);
			this.Write((byte)(offset + 1), ZERO_PAGE, indirectPage);

			return new MemoryAddress(indirectOffset, indirectPage);
		}
	}
}