namespace NesZord.Core
{
	public enum OpCode
	{
		Break = 0x00,

		SetCarryFlag = 0x38,

		IndexedIndirectAddWithCarry = 0x61,

		ZeroPageAddWithCarry = 0x65,

		ImmediateAddWithCarry = 0x69,

		AbsoluteAddWithCarry = 0x6d,

		IndirectIndexedAddWithCarry = 0x71,

		ZeroPageXAddWithCarry = 0x75,

		AbsoluteYAddWithCarry = 0x79,

		AbsoluteXAddWithCarry = 0x7d,

		TransferFromXToAccumulator = 0x8a,

		AbsoluteStoreYRegister = 0x8c,

		AbsoluteStoreAccumulator = 0x8d,

		AbsoluteStoreXRegister = 0x8e,

		BranchIfCarryIsClear = 0x90,

		AbsoluteXStoreAccumulator = 0x9d,

		TransferFromYToAccumulator = 0x98,

		AbsoluteYStoreAccumulator = 0x99,

		ImmediateLoadYRegister = 0xa0,

		ImmediateLoadXRegister = 0xa2,

		TransferFromAccumulatorToY = 0xa8,

		ImmediateLoadAccumulator = 0xa9,

		TransferFromAccumulatorToX = 0xaa,

		BranchIfCarryIsSet = 0xb0,

		ImmediateCompareYRegister = 0xc0,

		ZeroPageCompareYRegister = 0xc4,

		IncrementValueAtY = 0xc8,

		DecrementValueAtX = 0xca,

		AbsoluteCompareYRegister = 0xcc,

		BranchIfNotEqual = 0xd0,

		ImmediateCompareXRegister = 0xe0,

		IndexedIndirectSubtractWithCarry = 0xe1,

		ZeroPageCompareXRegister = 0xe4,

		ZeroPageSubtractWithCarry = 0xe5,

		IncrementValueAtX = 0xe8,

		ImmediateSubtractWithCarry = 0xe9,

		AbsoluteCompareXRegister = 0xec,

		AbsoluteSubtractWithCarry = 0xed,

		BranchIfEqual = 0xf0,

		IndirectIndexedSubtractWithCarry = 0xf1,

		ZeroPageXSubtractWithCarry = 0xf5,

		AbsoluteYSubtractWithCarry = 0xf9,

		AbsoluteXSubtractWithCarry = 0xfd
	}
}