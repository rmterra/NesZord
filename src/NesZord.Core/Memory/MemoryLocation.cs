using NesZord.Core.Extensions;
using System;

namespace NesZord.Core.Memory
{
	public class MemoryLocation : IComparable<int>, IComparable<MemoryLocation>
	{
		public MemoryLocation(byte offset, byte page)
		{
			this.Offset = offset;
			this.Page = page;
		}

		public byte Offset { get; private set; }

		public byte Page { get; private set; }

		public int FullLocation
		{
			get { return (this.Page << 8) + this.Offset; }
		}

		public MemoryLocation Sum(byte value)
			=> FromInt32(this.FullLocation + value);

		public MemoryLocation And(MemoryLocation other)
			=> FromInt32(this.FullLocation & other.FullLocation);

		public MemoryLocation Or(MemoryLocation other)
			=> FromInt32(this.FullLocation | other.FullLocation);

		private static MemoryLocation FromInt32(int value)
			=> new MemoryLocation(value.GetOffset(), value.GetPage());

		public int CompareTo(MemoryLocation other)
		{
			if (other == null) { throw new ArgumentNullException(nameof(other)); }

			return this.CompareTo(other.FullLocation);
		}

		public int CompareTo(int other)
		{
			if (this.FullLocation == other) { return 0; }
			else if (this.FullLocation > other) { return 1; }
			else { return -1; }
		}

		public static bool operator <(MemoryLocation location1, MemoryLocation location2)
			=> location1.CompareTo(location2) < 0;

		public static bool operator >(MemoryLocation location1, MemoryLocation location2)
			=> location1.CompareTo(location2) > 0;
	}
}