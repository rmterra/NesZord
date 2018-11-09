using System;

namespace NesZord.Core.Memory
{
	internal class BoundedMemory : IBoundedMemory
	{
		private const int LENGTH = 0x0800;

		private byte[] data;

		public BoundedMemory(MemoryLocation firstAddress, MemoryLocation lastAddress)
		{
			this.FirstAddress = firstAddress ?? throw new ArgumentNullException(nameof(firstAddress));
			this.LastAddress = lastAddress ?? throw new ArgumentNullException(nameof(lastAddress));

			this.data = new byte[LENGTH];
		}

		public MemoryLocation FirstAddress { get; }

		public MemoryLocation LastAddress { get; }

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
			if (location < this.FirstAddress) { throw new ArgumentOutOfRangeException(nameof(location)); }
			if (location > this.LastAddress) { throw new ArgumentOutOfRangeException(nameof(location)); }
		}
	}
}
