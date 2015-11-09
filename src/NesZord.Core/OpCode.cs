namespace NesZord.Core
{
	public enum OpCode
	{
		Break = 0x00,

		IndexedIndirectBitwiseAnd = 0x21,

		ZeroPageBitwiseAnd = 0x25,

		ImmediateBitwiseAnd = 0x29,

		AbsoluteBitwiseAnd = 0x2d,

		IndirectIndexedBitwiseAnd = 0x31,

		ZeroPageXBitwiseAnd = 0x35,

		SetCarryFlag = 0x38,

		AbsoluteYBitwiseAnd = 0x39,

		AbsoluteXBitwiseAnd = 0x3d,

		IndexedIndirectAddWithCarry = 0x61,

		ZeroPageAddWithCarry = 0x65,

		ImmediateAddWithCarry = 0x69,

		AbsoluteAddWithCarry = 0x6d,

		IndirectIndexedAddWithCarry = 0x71,

		ZeroPageXAddWithCarry = 0x75,

		AbsoluteYAddWithCarry = 0x79,

		AbsoluteXAddWithCarry = 0x7d,

		IndexedIndirectStoreAccumulator = 0x81,

		ZeroPageStoreYRegister = 0x84,

		ZeroPageStoreAccumulator = 0x85,

		ZeroPageStoreXRegister = 0x86,

		DecrementValueAtY = 0x88,

		TransferFromXToAccumulator = 0x8a,

		AbsoluteStoreYRegister = 0x8c,

		AbsoluteStoreAccumulator = 0x8d,

		AbsoluteStoreXRegister = 0x8e,

		BranchIfCarryIsClear = 0x90,

		IndirectIndexedStoreAccumulator = 0x91,

		ZeroPageXStoreAccumulator = 0x95,

		AbsoluteXStoreAccumulator = 0x9d,

		ZeroPageXStoreYRegister = 0x94,

		ZeroPageYStoreXRegister = 0x96,

		TransferFromYToAccumulator = 0x98,

		AbsoluteYStoreAccumulator = 0x99,

		ImmediateLoadYRegister = 0xa0,

		ImmediateLoadXRegister = 0xa2,

		ZeroPageLoadYRegister = 0xa4,

		ZeroPageLoadXRegister = 0xa6,

		TransferFromAccumulatorToY = 0xa8,

		IndexedIndirectLoadAccumulator = 0xa1,

		ZeroPageLoadAccumulator = 0xa5,

		ImmediateLoadAccumulator = 0xa9,

		TransferFromAccumulatorToX = 0xaa,

		AbsoluteLoadYRegister = 0xac,

		AbsoluteLoadAccumulator = 0xad,

		AbsoluteLoadXRegister = 0xae,

		BranchIfCarryIsSet = 0xb0,

		IndirectIndexedLoadAccumulator = 0xb1,

		ZeroPageXLoadYRegister = 0xb4,

		ZeroPageXLoadAccumulator = 0xb5,

		ZeroPageYLoadXRegister = 0xb6,

		AbsoluteYLoadAccumulator = 0xb9,

		AbsoluteXLoadYRegister = 0xbc,

		AbsoluteXLoadAccumulator = 0xbd,

		AbsoluteYLoadXRegister = 0xbe,

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