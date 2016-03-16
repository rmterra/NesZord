namespace NesZord.Core
{
	public enum AddressingMode
	{
		Undefined = 0,

		Absolute = 1,

		AbsoluteY = 2,

		AbsoluteX = 3,

		Accumulator = 4,

		Indirect = 5,

		IndexedIndirect = 6,

		IndirectIndexed = 7,

		Immediate = 8,

		Implied = 9,

		Relative = 10,

		ZeroPage = 11,

		ZeroPageX = 12,

		ZeroPageY = 13
	}
}
