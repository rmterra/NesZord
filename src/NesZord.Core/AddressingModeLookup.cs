namespace NesZord.Core
{
	public static class AddressingModeLookup
	{
		public static AddressingMode For(OpCode opCode)
		{
			switch (opCode)
			{
				case OpCode.ADC_Absolute:
				case OpCode.AND_Absolute:
				case OpCode.ASL_Absolute:
				case OpCode.CMP_Absolute:
				case OpCode.BIT_Absolute:
				case OpCode.CPY_Absolute:
				case OpCode.CPX_Absolute:
				case OpCode.DEC_Absolute:
				case OpCode.EOR_Absolute:
				case OpCode.INC_Absolute:
				case OpCode.LDA_Absolute:
				case OpCode.LDX_Absolute:
				case OpCode.LDY_Absolute:
				case OpCode.LSR_Absolute:
				case OpCode.ORA_Absolute:
				case OpCode.STY_Absolute:
				case OpCode.STA_Absolute:
				case OpCode.STX_Absolute:
				case OpCode.SBC_Absolute:
					return AddressingMode.Absolute;

				case OpCode.ADC_AbsoluteX:
				case OpCode.AND_AbsoluteX:
				case OpCode.ASL_AbsoluteX:
				case OpCode.CMP_AbsoluteX:
				case OpCode.DEC_AbsoluteX:
				case OpCode.EOR_AbsoluteX:
				case OpCode.INC_AbsoluteX:
				case OpCode.LDA_AbsoluteX:
				case OpCode.LDY_AbsoluteX:
				case OpCode.LSR_AbsoluteX:
				case OpCode.ORA_AbsoluteX:
				case OpCode.STA_AbsoluteX:
				case OpCode.SBC_AbsoluteX:
					return AddressingMode.AbsoluteX;

				case OpCode.ADC_AbsoluteY:
				case OpCode.AND_AbsoluteY:
				case OpCode.CMP_AbsoluteY:
				case OpCode.EOR_AbsoluteY:
				case OpCode.LDA_AbsoluteY:
				case OpCode.LDX_AbsoluteY:
                case OpCode.ORA_AbsoluteY:
                case OpCode.STA_AbsoluteY:
				case OpCode.SBC_AbsoluteY:
					return AddressingMode.AbsoluteY;

				case OpCode.ASL_Accumulator:
				case OpCode.LSR_Accumulator:
					return AddressingMode.Accumulator;

				case OpCode.ADC_IndexedIndirect:
				case OpCode.AND_IndexedIndirect:
				case OpCode.CMP_IndexedIndirect:
				case OpCode.EOR_IndexedIndirect:
				case OpCode.LDA_IndexedIndirect:
				case OpCode.ORA_IndexedIndirect:
				case OpCode.STA_IndexedIndirect:
				case OpCode.SBC_IndexedIndirect:
					return AddressingMode.IndexedIndirect;

				case OpCode.ADC_IndirectIndexed:
				case OpCode.AND_IndirectIndexed:
				case OpCode.CMP_IndirectIndexed:
				case OpCode.EOR_IndirectIndexed:
				case OpCode.LDA_IndirectIndexed:
				case OpCode.ORA_IndirectIndexed:
				case OpCode.STA_IndirectIndexed:
				case OpCode.SBC_IndirectIndexed:
                    return AddressingMode.IndirectIndexed;

				case OpCode.ADC_Immediate:
				case OpCode.AND_Immediate:
				case OpCode.CMP_Immediate:
				case OpCode.EOR_Immediate:
				case OpCode.LDY_Immediate:
				case OpCode.LDX_Immediate:
				case OpCode.LDA_Immediate:
				case OpCode.CPY_Immediate:
				case OpCode.CPX_Immediate:
				case OpCode.ORA_Immediate:
				case OpCode.SBC_Immediate:
					return AddressingMode.Immediate;

				case OpCode.BRK_Implied:
				case OpCode.SEC_Implied:
				case OpCode.TXA_Implied:
				case OpCode.TAX_Implied:
				case OpCode.DEX_Implied:
				case OpCode.INY_Implied:
				case OpCode.INX_Implied:
					return AddressingMode.Implied;

				case OpCode.BCC_Relative:
				case OpCode.BCS_Relative:
				case OpCode.BNE_Relative:
				case OpCode.BEQ_Relative:
					return AddressingMode.Relative;

				case OpCode.ADC_ZeroPage:
				case OpCode.AND_ZeroPage:
				case OpCode.ASL_ZeroPage:
				case OpCode.CMP_ZeroPage:
				case OpCode.BIT_ZeroPage:
				case OpCode.CPY_ZeroPage:
				case OpCode.CPX_ZeroPage:
				case OpCode.DEC_ZeroPage:
				case OpCode.EOR_ZeroPage:
				case OpCode.INC_ZeroPage:
				case OpCode.LDA_ZeroPage:
				case OpCode.LDX_ZeroPage:
				case OpCode.LDY_ZeroPage:
				case OpCode.LSR_ZeroPage:
				case OpCode.ORA_ZeroPage:
				case OpCode.STA_ZeroPage:
				case OpCode.STX_ZeroPage:
				case OpCode.STY_ZeroPage:
				case OpCode.SBC_ZeroPage:
					return AddressingMode.ZeroPage;

				case OpCode.ADC_ZeroPageX:
				case OpCode.AND_ZeroPageX:
				case OpCode.ASL_ZeroPageX:
				case OpCode.CMP_ZeroPageX:
				case OpCode.DEC_ZeroPageX:
				case OpCode.EOR_ZeroPageX:
				case OpCode.INC_ZeroPageX:
				case OpCode.LDA_ZeroPageX:
				case OpCode.LDY_ZeroPageX:
				case OpCode.LSR_ZeroPageX:
				case OpCode.ORA_ZeroPageX:
				case OpCode.STA_ZeroPageX:
				case OpCode.STY_ZeroPageX:
				case OpCode.SBC_ZeroPageX:
					return AddressingMode.ZeroPageX;

				case OpCode.LDX_ZeroPageY:
				case OpCode.STX_ZeroPageY:
					return AddressingMode.ZeroPageY;

				default:
					return AddressingMode.Undefined;
			}
		}
	}
}