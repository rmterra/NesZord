namespace NesZord.Core.Extensions
{
	public static class Int32Extension
	{
		public static byte GetPage(this int value)
		{
			return (byte)(value >> 8);
		}

		public static byte GetOffset(this int value)
		{
			return (byte)(value & 0xff);
		}
	}
}