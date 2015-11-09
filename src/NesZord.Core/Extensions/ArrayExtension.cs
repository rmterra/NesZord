using System;

namespace NesZord.Core.Extensions
{
	public static class ArrayExtension
	{
		public static T[] Concat<T>(this T[] x, T[] y)
		{
			if (x == null) { throw new ArgumentNullException("x"); }
			if (y == null) { throw new ArgumentNullException("y"); }

			Array.Resize<T>(ref x, x.Length + y.Length);
			Array.Copy(y, 0, x, x.Length, y.Length);

			return x;
		}
	}
}