namespace NesZord.Core
{
	public enum OpCode
	{
		Break = 0x00,

		SetCarryFlag = 0x38,

		ImmediateAddWithCarry = 0x69,

		TransferFromXToAccumulator = 0x8a,

		AbsoluteStoreYRegister = 0x8c,

		AbsoluteStoreAccumulator = 0x8d,

		AbsoluteStoreXRegister = 0x8e,

		BranchIfCarryIsClear = 0x90,

		ImmediateLoadYRegister = 0xa0,

		ImmediateLoadXRegister = 0xa2,

		ImmediateLoadAccumulator = 0xa9,

		TransferFromAccumulatorToX = 0xaa,

		BranchIfCarryIsSet = 0xb0,

		DecrementValueAtX = 0xca,

		BranchIfNotEqual = 0xd0,

		BranchIfEqual = 0xf0,

		ImmediateCompareYRegister = 0xc0,

		ImmediateCompareXRegister = 0xe0,

		IncrementValueAtY = 0xc8,

		IncrementValueAtX = 0xe8
	}
}