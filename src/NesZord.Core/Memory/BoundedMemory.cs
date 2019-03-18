using System;
using System.Collections.Generic;

namespace NesZord.Core.Memory
{
	internal class BoundedMemory : IBoundedMemory
	{
		private Dictionary<int, byte> data;

		public BoundedMemory(MemoryAddress firstAddress, MemoryAddress lastAddress)
		{
			this.FirstAddress = firstAddress ?? throw new ArgumentNullException(nameof(firstAddress));
			this.LastAddress = lastAddress ?? throw new ArgumentNullException(nameof(lastAddress));

			this.data = new Dictionary<int, byte>();
		}

		public BoundedMemory(byte[] buffer)
		{
			if (buffer == null) { throw new ArgumentNullException(nameof(buffer)); }

			this.data = new Dictionary<int, byte>();

			this.FirstAddress = new MemoryAddress(0x00, 0x00);

			var currentAddress = this.FirstAddress;

			foreach (var value in buffer)
			{
				this.data[currentAddress.FullAddress] = value;
				currentAddress = currentAddress.NextAddress();
			}

			this.LastAddress = currentAddress;
		}

		public MemoryAddress FirstAddress { get; }

		public MemoryAddress LastAddress { get; }

		public void Write(MemoryAddress address, byte value)
		{
			if (address == null) { throw new ArgumentNullException(nameof(address)); }
			if (address.In(this.FirstAddress, this.LastAddress) == false)
			{
				throw new ArgumentOutOfRangeException(nameof(address));
			}

			this.data[address.FullAddress] = value;
		}

		public byte Read(MemoryAddress address)
		{
			if (address == null) { throw new ArgumentNullException(nameof(address)); }
			if (address.In(this.FirstAddress, this.LastAddress) == false)
			{
				throw new ArgumentOutOfRangeException(nameof(address));
			}

			return this.data.ContainsKey(address.FullAddress)
				? this.data[address.FullAddress]
				: default(byte);
		}
	}
}
