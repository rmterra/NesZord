using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NesZord.Core
{
	public enum AddressingMode
	{
		Undefined = 0,

		Absolute = 1,

		AbsoluteY = 2,

		AbsoluteX = 3,

		IndexedIndirect = 4,

		IndirectIndexed = 5,

		Immediate = 6,

		Implied = 7,

		Relative = 8,

		ZeroPage = 9,

		ZeroPageX = 10,

		ZeroPageY = 11
	}
}
