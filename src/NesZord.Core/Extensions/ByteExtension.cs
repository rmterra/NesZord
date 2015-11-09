namespace NesZord.Core.Extensions
{
	public static class ByteExtension
	{
		public static bool GetBitAt(this byte value, int position)
		{
			return (value & (1 << position - 1)) != 0;
		}
	}
}
