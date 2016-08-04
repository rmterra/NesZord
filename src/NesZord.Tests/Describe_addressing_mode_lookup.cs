using NesZord.Core;
using NSpec;

namespace NesZord.Tests
{
	public class Describe_addressing_mode_lookup : nspec
	{
		public void When_lookup()
		{
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ADC_Absolute, AddressingMode.Absolute);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AND_Absolute, AddressingMode.Absolute);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ASL_Absolute, AddressingMode.Absolute);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.BIT_Absolute, AddressingMode.Absolute);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.CMP_Absolute, AddressingMode.Absolute);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.CPY_Absolute, AddressingMode.Absolute);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.CPX_Absolute, AddressingMode.Absolute);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.DEC_Absolute, AddressingMode.Absolute);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.EOR_Absolute, AddressingMode.Absolute);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.INC_Absolute, AddressingMode.Absolute);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.JMP_Absolute, AddressingMode.Absolute);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.JSR_Absolute, AddressingMode.Absolute);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.LDA_Absolute, AddressingMode.Absolute);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.LDX_Absolute, AddressingMode.Absolute);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.LDY_Absolute, AddressingMode.Absolute);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.LSR_Absolute, AddressingMode.Absolute);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ORA_Absolute, AddressingMode.Absolute);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ROL_Absolute, AddressingMode.Absolute);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ROR_Absolute, AddressingMode.Absolute);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.STY_Absolute, AddressingMode.Absolute);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.STA_Absolute, AddressingMode.Absolute);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.STX_Absolute, AddressingMode.Absolute);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.SBC_Absolute, AddressingMode.Absolute);

			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ASL_Accumulator, AddressingMode.Accumulator);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.LSR_Accumulator, AddressingMode.Accumulator);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ROL_Accumulator, AddressingMode.Accumulator);
            this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ROR_Accumulator, AddressingMode.Accumulator);

            this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ADC_AbsoluteX, AddressingMode.AbsoluteX);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AND_AbsoluteX, AddressingMode.AbsoluteX);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ASL_AbsoluteX, AddressingMode.AbsoluteX);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.CMP_AbsoluteX, AddressingMode.AbsoluteX);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.DEC_AbsoluteX, AddressingMode.AbsoluteX);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.EOR_AbsoluteX, AddressingMode.AbsoluteX);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.INC_AbsoluteX, AddressingMode.AbsoluteX);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.LDA_AbsoluteX, AddressingMode.AbsoluteX);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.LDY_AbsoluteX, AddressingMode.AbsoluteX);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.LSR_AbsoluteX, AddressingMode.AbsoluteX);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ORA_AbsoluteX, AddressingMode.AbsoluteX);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ROL_AbsoluteX, AddressingMode.AbsoluteX);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ROR_AbsoluteX, AddressingMode.AbsoluteX);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.STA_AbsoluteX, AddressingMode.AbsoluteX);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.SBC_AbsoluteX, AddressingMode.AbsoluteX);

			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ADC_AbsoluteY, AddressingMode.AbsoluteY);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AND_AbsoluteY, AddressingMode.AbsoluteY);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.CMP_AbsoluteY, AddressingMode.AbsoluteY);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.EOR_AbsoluteY, AddressingMode.AbsoluteY);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.LDA_AbsoluteY, AddressingMode.AbsoluteY);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.LDX_AbsoluteY, AddressingMode.AbsoluteY);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ORA_AbsoluteY, AddressingMode.AbsoluteY);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.STA_AbsoluteY, AddressingMode.AbsoluteY);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.SBC_AbsoluteY, AddressingMode.AbsoluteY);

			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.JMP_Indirect, AddressingMode.Indirect);

			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ADC_IndexedIndirect, AddressingMode.IndexedIndirect);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AND_IndexedIndirect, AddressingMode.IndexedIndirect);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.CMP_IndexedIndirect, AddressingMode.IndexedIndirect);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.EOR_IndexedIndirect, AddressingMode.IndexedIndirect);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.LDA_IndexedIndirect, AddressingMode.IndexedIndirect);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ORA_IndexedIndirect, AddressingMode.IndexedIndirect);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.STA_IndexedIndirect, AddressingMode.IndexedIndirect);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.SBC_IndexedIndirect, AddressingMode.IndexedIndirect);

			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ADC_IndirectIndexed, AddressingMode.IndirectIndexed);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AND_IndirectIndexed, AddressingMode.IndirectIndexed);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.CMP_IndirectIndexed, AddressingMode.IndirectIndexed);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.EOR_IndirectIndexed, AddressingMode.IndirectIndexed);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.LDA_IndirectIndexed, AddressingMode.IndirectIndexed);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ORA_IndirectIndexed, AddressingMode.IndirectIndexed);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.STA_IndirectIndexed, AddressingMode.IndirectIndexed);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.SBC_IndirectIndexed, AddressingMode.IndirectIndexed);

			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ADC_Immediate, AddressingMode.Immediate);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AND_Immediate, AddressingMode.Immediate);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.CMP_Immediate, AddressingMode.Immediate);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.EOR_Immediate, AddressingMode.Immediate);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.LDY_Immediate, AddressingMode.Immediate);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.LDX_Immediate, AddressingMode.Immediate);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.LDA_Immediate, AddressingMode.Immediate);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.CPY_Immediate, AddressingMode.Immediate);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.CPX_Immediate, AddressingMode.Immediate);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ORA_Immediate, AddressingMode.Immediate);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.SBC_Immediate, AddressingMode.Immediate);

			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.BRK_Implied, AddressingMode.Implied);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.DEX_Implied, AddressingMode.Implied);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.INY_Implied, AddressingMode.Implied);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.INX_Implied, AddressingMode.Implied);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.SEC_Implied, AddressingMode.Implied);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.TAX_Implied, AddressingMode.Implied);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.TSX_Implied, AddressingMode.Implied);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.TXA_Implied, AddressingMode.Implied);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.TXS_Implied, AddressingMode.Implied);

			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.BCC_Relative, AddressingMode.Relative);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.BCS_Relative, AddressingMode.Relative);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.BNE_Relative, AddressingMode.Relative);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.BEQ_Relative, AddressingMode.Relative);

			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ADC_ZeroPage, AddressingMode.ZeroPage);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AND_ZeroPage, AddressingMode.ZeroPage);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ASL_ZeroPage, AddressingMode.ZeroPage);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.BIT_ZeroPage, AddressingMode.ZeroPage);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.CMP_ZeroPage, AddressingMode.ZeroPage);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.DEC_ZeroPage, AddressingMode.ZeroPage);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.EOR_ZeroPage, AddressingMode.ZeroPage);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.INC_ZeroPage, AddressingMode.ZeroPage);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.LDA_ZeroPage, AddressingMode.ZeroPage);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.LDX_ZeroPage, AddressingMode.ZeroPage);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.LDY_ZeroPage, AddressingMode.ZeroPage);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.LSR_ZeroPage, AddressingMode.ZeroPage);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.CPY_ZeroPage, AddressingMode.ZeroPage);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.CPX_ZeroPage, AddressingMode.ZeroPage);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ORA_ZeroPage, AddressingMode.ZeroPage);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ROL_ZeroPage, AddressingMode.ZeroPage);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ROR_ZeroPage, AddressingMode.ZeroPage);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.STA_ZeroPage, AddressingMode.ZeroPage);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.STX_ZeroPage, AddressingMode.ZeroPage);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.STY_ZeroPage, AddressingMode.ZeroPage);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.SBC_ZeroPage, AddressingMode.ZeroPage);

			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ADC_ZeroPageX, AddressingMode.ZeroPageX);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.AND_ZeroPageX, AddressingMode.ZeroPageX);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ASL_ZeroPageX, AddressingMode.ZeroPageX);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.CMP_ZeroPageX, AddressingMode.ZeroPageX);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.DEC_ZeroPageX, AddressingMode.ZeroPageX);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.INC_ZeroPageX, AddressingMode.ZeroPageX);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.EOR_ZeroPageX, AddressingMode.ZeroPageX);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.LDA_ZeroPageX, AddressingMode.ZeroPageX);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.LDY_ZeroPageX, AddressingMode.ZeroPageX);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.LSR_ZeroPageX, AddressingMode.ZeroPageX);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ORA_ZeroPageX, AddressingMode.ZeroPageX);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ROL_ZeroPageX, AddressingMode.ZeroPageX);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.ROR_ZeroPageX, AddressingMode.ZeroPageX);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.STA_ZeroPageX, AddressingMode.ZeroPageX);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.STY_ZeroPageX, AddressingMode.ZeroPageX);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.SBC_ZeroPageX, AddressingMode.ZeroPageX);

			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.LDX_ZeroPageY, AddressingMode.ZeroPageY);
			this.GivenTheOpCodeReceivedAddressingModeShouldBe(OpCode.STX_ZeroPageY, AddressingMode.ZeroPageY);
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
