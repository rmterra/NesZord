using System;

namespace NesZord.Core.Memory
{
	internal class BoundedMemory : IBoundedMemory
	{
		private const int LENGTH = 0x800;

		private byte[] data;

		public BoundedMemory(int firstAddress, int lastAddress)
		{
			this.data = new byte[LENGTH];

			this.FirstAddress = firstAddress;
			this.LastAddress = lastAddress;
		}

		public int FirstAddress { get; }

		public int LastAddress { get; }

		public void Write(MemoryLocation location, byte value)
		{
			if (location == null) { throw new ArgumentNullException(nameof(location)); }
			this.ThrowIfOutOfRange(location);

			this.data[location.FullLocation] = value;
		}

		public byte Read(MemoryLocation location)
		{
			if (location == null) { throw new ArgumentNullException(nameof(location)); }
			this.ThrowIfOutOfRange(location);

			return this.data[location.FullLocation];
		}

		private void ThrowIfOutOfRange(MemoryLocation location)
		{
			if (location.FullLocation < this.FirstAddress) { throw new ArgumentOutOfRangeException(nameof(location)); }
			if (location.FullLocation > this.LastAddress) { throw new ArgumentOutOfRangeException(nameof(location)); }
		}
	}
}
