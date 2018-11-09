using System;

namespace NesZord.Core.Memory
{
	internal class Ram : IBoundedMemory
	{
		private static readonly MemoryLocation INTERNAL_RAM_FIRST_ADDRESS = new MemoryLocation(0x00, 0x00);

		private static readonly MemoryLocation INTERNAL_RAM_LASTADDRESS = new MemoryLocation(0xff, 0x07);

		private static readonly MemoryLocation MIRROR1_FIRST_ADDRESS = new MemoryLocation(0x00, 0x08);

		private static readonly MemoryLocation MIRROR1_LAST_ADDRESS = new MemoryLocation(0xff, 0x0f);

		private static readonly MemoryLocation MIRROR2_FIRST_ADDRESS = new MemoryLocation(0x00, 0x10);

		private static readonly MemoryLocation MIRROR2_LAST_ADDRESS = new MemoryLocation(0xff, 0x17);

		private static readonly MemoryLocation MIRROR3_FIRST_ADDRESS = new MemoryLocation(0x00, 0x18);

		private static readonly MemoryLocation MIRROR3_LAST_ADDRESS = new MemoryLocation(0xff, 0x1f);

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

		public MemoryLocation FirstAddress { get { return this.internalRam.FirstAddress; } }

		public MemoryLocation LastAddress { get { return this.mirror3.LastAddress; } }

		public void Write(MemoryLocation location, byte value)
		{
			if (location == null) { throw new ArgumentNullException(nameof(location)); }
			this.ThrowIfOutOfRange(location);

			var normalizedLocation = this.Normalize(location);

			this.internalRam.Write(normalizedLocation, value);

			this.WriteOnMirror(this.mirror1, normalizedLocation, value);
			this.WriteOnMirror(this.mirror2, normalizedLocation, value);
			this.WriteOnMirror(this.mirror3, normalizedLocation, value);
		}

		public byte Read(MemoryLocation location)
		{
			if (location == null) { throw new ArgumentNullException(nameof(location)); }
			this.ThrowIfOutOfRange(location);

			var normalizedLocation = this.Normalize(location);
			return this.internalRam.Read(normalizedLocation);
		}

		private void ThrowIfOutOfRange(MemoryLocation location)
		{
			if (location < this.FirstAddress) { throw new ArgumentOutOfRangeException(nameof(location)); }
			if (location > this.LastAddress) { throw new ArgumentOutOfRangeException(nameof(location)); }
		}

		private MemoryLocation Normalize(MemoryLocation location)
			=> location.And(this.internalRam.LastAddress);

		private void WriteOnMirror(BoundedMemory mirror, MemoryLocation location, byte value)
		{
			var mirrorLocation = location.Or(mirror.FirstAddress);

			mirror.Write(mirrorLocation, value);
		}
	}
}
