using System;

namespace NesZord.Core.Extensions
{
	public static class ByteExtension
	{
		public static bool GetBitAt(this byte value, int index)
		{
			return (value & (1 << index)) != 0;
		}

		public static byte ConvertToBcd(this byte value)
		{
			var tenColumn = value >> 4;
			if (tenColumn > 9) { throw new InvalidOperationException("Ten column overflow BCD maximum value"); }

			var unitColumn = (tenColumn << 4) ^ value;
			if (unitColumn > 9) { throw new InvalidOperationException("Unit column overflow BCD maximum value"); }

			return (byte)((tenColumn * 10) + unitColumn);
        }
    }
}
