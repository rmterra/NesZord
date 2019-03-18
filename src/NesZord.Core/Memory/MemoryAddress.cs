using NesZord.Core.Extensions;
using System;

namespace NesZord.Core.Memory
{
	public class MemoryAddress : IComparable<int>, IComparable<MemoryAddress>
	{
		public MemoryAddress(byte page, byte offset)
		{
			this.Page = page;
			this.Offset = offset;
		}

		public byte Offset { get; private set; }

		public byte Page { get; private set; }

		public int FullAddress
		{
			get { return (this.Page << 8) + this.Offset; }
		}

		public bool In(MemoryAddress address1, MemoryAddress address2)
		{
			if (address1 == null) { throw new ArgumentNullException(nameof(address1)); }
			if (address2 == null) { throw new ArgumentNullException(nameof(address2)); }

			return this >= address1 && this <= address2;
		}

		public MemoryAddress NextAddress()
			=> this.Sum(0x0001);

		public MemoryAddress Sum(byte value)
			=> FromInt32(this.FullAddress + value);

		public MemoryAddress And(MemoryAddress other)
		{
			if (other == null) { throw new ArgumentNullException(nameof(other)); }

			return FromInt32(this.FullAddress & other.FullAddress);
		}

		public MemoryAddress Or(MemoryAddress other)
		{
			if (other == null) { throw new ArgumentNullException(nameof(other)); }

			return FromInt32(this.FullAddress | other.FullAddress);
		}

		private static MemoryAddress FromInt32(int value)
			=> new MemoryAddress(value.GetPage(), value.GetOffset());

		public int CompareTo(MemoryAddress other)
		{
			if (other == null) { throw new ArgumentNullException(nameof(other)); }

			return this.CompareTo(other.FullAddress);
		}

		public int CompareTo(int other)
		{
			if (this.FullAddress == other) { return 0; }
			else if (this.FullAddress > other) { return 1; }
			else { return -1; }
		}

		public static bool operator <(MemoryAddress address1, MemoryAddress address2)
			=> address1.CompareTo(address2) < 0;

		public static bool operator <=(MemoryAddress address1, MemoryAddress address2)
			=> address1.CompareTo(address2) <= 0;

		public static bool operator >(MemoryAddress address1, MemoryAddress address2)
			=> address1.CompareTo(address2) > 0;

		public static bool operator >=(MemoryAddress address1, MemoryAddress address2)
			=> address1.CompareTo(address2) >= 0;
	}
}