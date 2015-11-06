using NesZord.Core;
using NSpec;

namespace NesZord.Tests
{
	public class Describe_addressing_mode_lookup : nspec
	{
		public void When_lookup()
		{
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AbsoluteAddWithCarry, AddressingMode.Absolute);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AbsoluteCompareYRegister, AddressingMode.Absolute);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AbsoluteCompareXRegister, AddressingMode.Absolute);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AbsoluteStoreYRegister, AddressingMode.Absolute);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AbsoluteStoreAccumulator, AddressingMode.Absolute);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AbsoluteStoreXRegister, AddressingMode.Absolute);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AbsoluteSubtractWithCarry, AddressingMode.Absolute);

			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AbsoluteXAddWithCarry, AddressingMode.AbsoluteX);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AbsoluteXStoreAccumulator, AddressingMode.AbsoluteX);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AbsoluteXSubtractWithCarry, AddressingMode.AbsoluteX);

			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AbsoluteYAddWithCarry, AddressingMode.AbsoluteY);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AbsoluteYStoreAccumulator, AddressingMode.AbsoluteY);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AbsoluteYSubtractWithCarry, AddressingMode.AbsoluteY);

			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.IndexedIndirectAddWithCarry, AddressingMode.IndexedIndirect);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.IndexedIndirectSubtractWithCarry, AddressingMode.IndexedIndirect);

			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.IndirectIndexedAddWithCarry, AddressingMode.IndirectIndexed);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.IndirectIndexedSubtractWithCarry, AddressingMode.IndirectIndexed);

			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ImmediateAddWithCarry, AddressingMode.Immediate);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ImmediateLoadYRegister, AddressingMode.Immediate);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ImmediateLoadXRegister, AddressingMode.Immediate);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ImmediateLoadAccumulator, AddressingMode.Immediate);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ImmediateCompareYRegister, AddressingMode.Immediate);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ImmediateCompareXRegister, AddressingMode.Immediate);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ImmediateSubtractWithCarry, AddressingMode.Immediate);

			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.Break, AddressingMode.Implied);
            this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.SetCarryFlag, AddressingMode.Implied);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.TransferFromXToAccumulator, AddressingMode.Implied);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.TransferFromAccumulatorToX, AddressingMode.Implied);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.DecrementValueAtX, AddressingMode.Implied);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.IncrementValueAtY, AddressingMode.Implied);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.IncrementValueAtX, AddressingMode.Implied);

			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.BranchIfCarryIsClear, AddressingMode.Relative);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.BranchIfCarryIsSet, AddressingMode.Relative);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.BranchIfNotEqual, AddressingMode.Relative);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.BranchIfEqual, AddressingMode.Relative);

			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ZeroPageAddWithCarry, AddressingMode.ZeroPage);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ZeroPageCompareYRegister, AddressingMode.ZeroPage);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ZeroPageCompareXRegister, AddressingMode.ZeroPage);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ZeroPageSubtractWithCarry, AddressingMode.ZeroPage);

			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ZeroPageXAddWithCarry, AddressingMode.ZeroPageX);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ZeroPageXSubtractWithCarry, AddressingMode.ZeroPageX);
		}

		public void GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode opCode, AddressingMode expected)
		{
			var received = default(AddressingMode);

			context[string.Format("given that opcode is {0}", opCode)] = () =>
			{
				act = () => { received = AddressingModeLookup.For(opCode); };

				it[string.Format("should found addressing mode be equal {0}", expected) ] = () => { received.should_be(expected); };
			};
		}
	}
}
