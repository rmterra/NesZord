using NesZord.Core.Memory;
using System;

namespace NesZord.Core.Cartridge
{
	public class Mapper003 : IMapper
	{
		private static readonly MemoryAddress ADDRESS_MIN_RANGE = new MemoryAddress(0x80, 0x00);

		private static readonly MemoryAddress ADDRESS_MAX_RANGE = new MemoryAddress(0xff, 0xff);

		public int GraphicBank { get; private set; }

		public int ProgramBank { get; private set; }

		public void Write(MemoryAddress address, byte value)
		{
			if (address == null) { throw new ArgumentNullException(nameof(address)); }
			if (address.In(ADDRESS_MIN_RANGE, ADDRESS_MAX_RANGE) == false) { return; }

			this.GraphicBank = value & 0b0000_0011;
		}
	}
}
