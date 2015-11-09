using NesZord.Core;
using NSpec;

namespace NesZord.Tests
{
	public class Describe_addressing_mode_lookup : nspec
	{
		public void When_lookup()
		{
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AbsoluteADC, AddressingMode.Absolute);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AbsoluteAND, AddressingMode.Absolute);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AbsoluteCPY, AddressingMode.Absolute);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AbsoluteCPX, AddressingMode.Absolute);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AbsoluteLDA, AddressingMode.Absolute);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AbsoluteLDX, AddressingMode.Absolute);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AbsoluteLDY, AddressingMode.Absolute);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AbsoluteSTY, AddressingMode.Absolute);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AbsoluteSTA, AddressingMode.Absolute);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AbsoluteSTX, AddressingMode.Absolute);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AbsoluteSBC, AddressingMode.Absolute);

			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AbsoluteXADC, AddressingMode.AbsoluteX);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AbsoluteXAND, AddressingMode.AbsoluteX);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AbsoluteXLDA, AddressingMode.AbsoluteX);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AbsoluteXLDY, AddressingMode.AbsoluteX);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AbsoluteXSTA, AddressingMode.AbsoluteX);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AbsoluteXSBC, AddressingMode.AbsoluteX);

			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AbsoluteYADC, AddressingMode.AbsoluteY);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AbsoluteYAND, AddressingMode.AbsoluteY);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AbsoluteYLDA, AddressingMode.AbsoluteY);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AbsoluteYLDX, AddressingMode.AbsoluteY);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AbsoluteYSTA, AddressingMode.AbsoluteY);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AbsoluteYSBC, AddressingMode.AbsoluteY);

			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.IndexedIndirectADC, AddressingMode.IndexedIndirect);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.IndexedAND, AddressingMode.IndexedIndirect);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.IndexedIndirectLDA, AddressingMode.IndexedIndirect);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.IndexedIndirectSTA, AddressingMode.IndexedIndirect);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.IndexedIndirectSBC, AddressingMode.IndexedIndirect);

			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.IndirectIndexedADC, AddressingMode.IndirectIndexed);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.IndirectIndexedAND, AddressingMode.IndirectIndexed);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.IndirectIndexedLDA, AddressingMode.IndirectIndexed);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.IndirectIndexedSTA, AddressingMode.IndirectIndexed);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.IndirectIndexedSBC, AddressingMode.IndirectIndexed);

			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ImmediateADC, AddressingMode.Immediate);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ImmediateAND, AddressingMode.Immediate);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ImmediateLDY, AddressingMode.Immediate);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ImmediateLDX, AddressingMode.Immediate);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ImmediateLDA, AddressingMode.Immediate);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ImmediateCPY, AddressingMode.Immediate);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ImmediateCPX, AddressingMode.Immediate);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ImmediateSBC, AddressingMode.Immediate);

			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.BRK, AddressingMode.Implied);
            this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.SEC, AddressingMode.Implied);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.TXA, AddressingMode.Implied);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.TAX, AddressingMode.Implied);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.DEX, AddressingMode.Implied);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.INY, AddressingMode.Implied);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.INX, AddressingMode.Implied);

			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.BCC, AddressingMode.Relative);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.BCS, AddressingMode.Relative);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.BNE, AddressingMode.Relative);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.BEQ, AddressingMode.Relative);

			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ZeroPageADC, AddressingMode.ZeroPage);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ZeroPageAND, AddressingMode.ZeroPage);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ZeroPageLDA, AddressingMode.ZeroPage);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ZeroPageLDX, AddressingMode.ZeroPage);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ZeroPageLDY, AddressingMode.ZeroPage);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ZeroPageCPY, AddressingMode.ZeroPage);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ZeroPageCPX, AddressingMode.ZeroPage);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ZeroPageSTA, AddressingMode.ZeroPage);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ZeroPageSTX, AddressingMode.ZeroPage);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ZeroPageSTY, AddressingMode.ZeroPage);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ZeroPageSBC, AddressingMode.ZeroPage);

			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ZeroPageXADC, AddressingMode.ZeroPageX);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ZeroPageXAND, AddressingMode.ZeroPageX);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ZeroPageXLDA, AddressingMode.ZeroPageX);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ZeroPageXLDY, AddressingMode.ZeroPageX);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ZeroPageXSTA, AddressingMode.ZeroPageX);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ZeroPageXSTY, AddressingMode.ZeroPageX);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ZeroPageXSBC, AddressingMode.ZeroPageX);

			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ZeroPageYLDX, AddressingMode.ZeroPageY);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ZeroPageYSTX, AddressingMode.ZeroPageY);
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
