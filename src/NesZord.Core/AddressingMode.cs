namespace NesZord.Core
{
	public enum AddressingMode
	{
		Undefined = 0,

		Absolute = 1,

		AbsoluteY = 2,

		AbsoluteX = 3,

		Accumulator = 4,

		IndexedIndirect = 5,

		IndirectIndexed = 6,

		Immediate = 7,

		Implied = 8,

		Relative = 9,

		ZeroPage = 10,

		ZeroPageX = 11,

		ZeroPageY = 12
	}
}
