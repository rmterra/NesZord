using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NesZord.Core.Extensions;

namespace NesZord.Core
{
	public class Register
	{
		public bool IsFirstBitSet
		{
			get { return this.Value.GetBitAt(Microprocessor.FIRST_BIT_INDEX); }
		}

		public bool IsSignBitSet
		{
			get { return this.Value.GetBitAt(Microprocessor.SIGN_BIT_INDEX); }
		}

		public bool IsValueEqualZero
		{
			get { return (this.Value & 0xff) == 0; }
		}

		public byte Value { get; set; }

		public void Decrement()
		{
			this.Value -= 1;
		}

		public void Increment()
		{
			this.Value += 1;
		}

		public void RotateLeft(byte carry)
		{
			this.ShiftLeft();
			this.Or(carry);
		}

		public void RotateRight(byte carry)
		{
			this.ShiftRight();
			this.Or(carry);
		}

		public void ShiftLeft()
		{
			this.Value = (byte)(this.Value << 1);
		}

		public void ShiftRight()
		{
			this.Value = (byte)(this.Value >> 1);
		}

		public byte And(byte valueToCompare)
		{
			this.Value = (byte)(this.Value & valueToCompare);
			return this.Value;
		}

		public byte ExlusiveOr(byte valueToCompare)
		{
			this.Value = (byte)(this.Value ^ valueToCompare);
			return this.Value;
		}

		public byte Or(byte valueToCompare)
		{
			this.Value = (byte)(this.Value | valueToCompare);
			return this.Value;
		}

		public byte ToBcd()
		{
			return this.Value.ConvertToBcd();
		}
	}
}
