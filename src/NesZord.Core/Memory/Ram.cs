using System;

namespace NesZord.Core.Memory
{
	internal class Ram : IBoundedMemory
	{
		private static readonly MemoryAddress INTERNAL_RAM_FIRST_ADDRESS = new MemoryAddress(0x00, 0x00);

		private static readonly MemoryAddress INTERNAL_RAM_LASTADDRESS = new MemoryAddress(0x07, 0xff);

		private static readonly MemoryAddress MIRROR1_FIRST_ADDRESS = new MemoryAddress(0x08, 0x00);

		private static readonly MemoryAddress MIRROR1_LAST_ADDRESS = new MemoryAddress(0x0f, 0xff);

		private static readonly MemoryAddress MIRROR2_FIRST_ADDRESS = new MemoryAddress(0x10, 0x00);

		private static readonly MemoryAddress MIRROR2_LAST_ADDRESS = new MemoryAddress(0x17, 0xff);

		private static readonly MemoryAddress MIRROR3_FIRST_ADDRESS = new MemoryAddress(0x18, 0x00);

		private static readonly MemoryAddress MIRROR3_LAST_ADDRESS = new MemoryAddress(0x1f, 0xff);

		private readonly BoundedMemory internalRam;

		private readonly BoundedMemory mirror1;

		private readonly BoundedMemory mirror2;

		private readonly BoundedMemory mirror3;

		public Ram()
		{
			this.internalRam = new BoundedMemory(INTERNAL_RAM_FIRST_ADDRESS, INTERNAL_RAM_LASTADDRESS);
			this.mirror1 = new BoundedMemory(MIRROR1_FIRST_ADDRESS, MIRROR1_LAST_ADDRESS);
			this.mirror2 = new BoundedMemory(MIRROR2_FIRST_ADDRESS, MIRROR2_LAST_ADDRESS);
			this.mirror3 = new BoundedMemory(MIRROR3_FIRST_ADDRESS, MIRROR3_LAST_ADDRESS);
		}

		public MemoryAddress FirstAddress { get { return this.internalRam.FirstAddress; } }

		public MemoryAddress LastAddress { get { return this.mirror3.LastAddress; } }

		public void Write(MemoryAddress address, byte value)
		{
			if (address == null) { throw new ArgumentNullException(nameof(address)); }
			this.ThrowIfOutOfRange(address);

			var normalizedAddress = this.Normalize(address);

			this.internalRam.Write(normalizedAddress, value);

			this.WriteOnMirror(this.mirror1, normalizedAddress, value);
			this.WriteOnMirror(this.mirror2, normalizedAddress, value);
			this.WriteOnMirror(this.mirror3, normalizedAddress, value);
		}

		public byte Read(MemoryAddress address)
		{
			if (address == null) { throw new ArgumentNullException(nameof(address)); }
			this.ThrowIfOutOfRange(address);

			var normalizedAddress = this.Normalize(address);
			return this.internalRam.Read(normalizedAddress);
		}

		private void ThrowIfOutOfRange(MemoryAddress address)
		{
			if (address < this.FirstAddress) { throw new ArgumentOutOfRangeException(nameof(address)); }
			if (address > this.LastAddress) { throw new ArgumentOutOfRangeException(nameof(address)); }
		}

		private MemoryAddress Normalize(MemoryAddress address)
			=> address.And(this.internalRam.LastAddress);

		private void WriteOnMirror(BoundedMemory mirror, MemoryAddress address, byte value)
		{
			var mirrorAddress = address.Or(mirror.FirstAddress);

			mirror.Write(mirrorAddress, value);
		}
	}
}
