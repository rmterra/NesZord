namespace NesZord.Core
{
	public static class AddressingModeLookup
	{
		public static AddressingMode For(OpCode opCode)
		{
			switch (opCode)
			{
				case OpCode.AbsoluteAddWithCarry:
				case OpCode.AbsoluteStoreYRegister:
				case OpCode.AbsoluteStoreAccumulator:
				case OpCode.AbsoluteStoreXRegister:
					return AddressingMode.Absolute;

				case OpCode.AbsoluteXAddWithCarry:
				case OpCode.AbsoluteXStoreAccumulator:
					return AddressingMode.AbsoluteX;

				case OpCode.AbsoluteYAddWithCarry:
				case OpCode.AbsoluteYStoreAccumulator:
					return AddressingMode.AbsoluteY;

				case OpCode.IndexedIndirectAddWithCarry:
					return AddressingMode.IndexedIndirect;

				case OpCode.IndirectIndexedAddWithCarry:
					return AddressingMode.IndirectIndexed;

				case OpCode.ImmediateAddWithCarry:
				case OpCode.ImmediateLoadYRegister:
				case OpCode.ImmediateLoadXRegister:
				case OpCode.ImmediateLoadAccumulator:
				case OpCode.ImmediateCompareYRegister:
				case OpCode.ImmediateCompareXRegister:
				case OpCode.ImmediateSubtractWithCarry:
					return AddressingMode.Immediate;

				case OpCode.Break:
				case OpCode.SetCarryFlag:
				case OpCode.TransferFromXToAccumulator:
				case OpCode.TransferFromAccumulatorToX:
				case OpCode.DecrementValueAtX:
				case OpCode.IncrementValueAtY:
				case OpCode.IncrementValueAtX:
					return AddressingMode.Implied;

				case OpCode.BranchIfCarryIsClear:
				case OpCode.BranchIfCarryIsSet:
				case OpCode.BranchIfNotEqual:
				case OpCode.BranchIfEqual:
					return AddressingMode.Relative;

				case OpCode.ZeroPageAddWithCarry:
					return AddressingMode.ZeroPage;

				case OpCode.ZeroPageXAddWithCarry:
					return AddressingMode.ZeroPageX;

				default:
					return AddressingMode.Undefined;
			}
		}
	}
}