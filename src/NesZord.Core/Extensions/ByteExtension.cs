namespace NesZord.Core.Extensions
{
	public static class ByteExtension
	{
		public static bool GetBitAt(this byte value, int index)
		{
			return (value & (1 << index)) != 0;
		}
	}
}
