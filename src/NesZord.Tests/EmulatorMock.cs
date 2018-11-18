using NesZord.Core;
using NesZord.Core.Memory;
using System;

namespace NesZord.Tests
{
	public class EmulatorMock : Emulator
	{
		private static readonly Random random = new Random();

		public MemoryAddress GetIndexedIndirectAddress(byte offset, byte xRegisterValue)
		{
			var indirectPage = this.GetRandomPage();
			var indirectOffset = this.GetRandomOffset();

			var computedOffset = (byte)(xRegisterValue + offset);
			this.WriteZeroPage(computedOffset, indirectOffset);
			this.WriteZeroPage((byte)(computedOffset + 1), indirectPage);

			return new MemoryAddress(indirectPage, indirectOffset);
		}

		public MemoryAddress GetIndirectIndexedAddress(byte offset, byte yRegisterValue)
		{
			var indirectPage = this.GetRandomPage();
			var indirectOffset = this.GetRandomOffset();

			this.WriteZeroPage(offset, indirectOffset);
			this.WriteZeroPage((byte)(offset + 1), indirectPage);

			return new MemoryAddress(indirectPage, indirectOffset).Sum(yRegisterValue);
		}

		public MemoryAddress GetIndirectAddress(byte offset)
		{
			var indirectPage = this.GetRandomPage();
			var indirectOffset = this.GetRandomOffset();

			this.WriteZeroPage(offset, indirectOffset);
			this.WriteZeroPage((byte)(offset + 1), indirectPage);

			return new MemoryAddress(indirectPage, indirectOffset);
		}

		public MemoryAddress GetRandomAddress()
			=> new MemoryAddress(this.GetRandomPage(), this.GetRandomOffset());

		public byte GetRandomPage()
			=> (byte)random.Next(0x00, 0x1f);

		public byte GetRandomOffset()
			=> (byte)random.Next(0x00, 0xff);

		public void MockIndexedIndirectMemoryWrite(byte offset, byte xRegisterValue, byte value)
		{
			var address = GetIndexedIndirectAddress(offset, xRegisterValue);
            this.Write(address, value);
		}

		public void MockIndirectIndexedMemoryWrite(byte offset, byte yRegisterValue, byte value)
		{
			var address = this.GetIndirectIndexedAddress(offset, yRegisterValue);
			this.Write(address, value);
		}

		public void MockIndirectMemoryWrite(byte offset, byte value)
		{
			var address = GetIndirectAddress(offset);
			this.Write(address, value);
		}
	}
}