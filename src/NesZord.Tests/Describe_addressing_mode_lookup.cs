using NesZord.Core;
using NSpec;

namespace NesZord.Tests
{
	public class Describe_addressing_mode_lookup : nspec
	{
		public void When_lookup()
		{
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AbsoluteStoreYRegister, AddressingMode.Absolute);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AbsoluteStoreAccumulator, AddressingMode.Absolute);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AbsoluteStoreXRegister, AddressingMode.Absolute);

			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AbsoluteXStoreAccumulator, AddressingMode.AbsoluteX);

			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AbsoluteYStoreAccumulator, AddressingMode.AbsoluteY);

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
