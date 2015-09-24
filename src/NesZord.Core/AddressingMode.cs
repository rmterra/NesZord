using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NesZord.Core
{
	public enum AddressingMode
	{
		Undefined = 0,

		AbsoluteY = 1,

		AbsoluteX = 2,

		Implied = 3,
		Immediate = 4,
		Absolute = 5,
		Relative = 6,
	}
}
