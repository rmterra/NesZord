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
