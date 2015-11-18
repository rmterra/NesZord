using System;

namespace NesZord.Core.Extensions
{
	public static class ByteExtension
	{
		public const int HALF_BYTE = 4;

		public const int BCD_MAXIMUM_VALUE = 9;

		public static bool GetBitAt(this byte value, int index)
		{
			return (value & (1 << index)) != 0;
		}

		public static byte ConvertToBcd(this byte value)
		{
			var tenColumn = value >> HALF_BYTE;
			if (tenColumn > BCD_MAXIMUM_VALUE) { throw new InvalidOperationException("Ten column overflow BCD maximum value"); }

			var unitColumn = (tenColumn << HALF_BYTE) ^ value;
			if (unitColumn > BCD_MAXIMUM_VALUE) { throw new InvalidOperationException("Unit column overflow BCD maximum value"); }

			tenColumn *= 10;
			return (byte)(tenColumn + unitColumn);
		}
	}
}
