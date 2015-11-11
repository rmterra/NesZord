﻿namespace NesZord.Core
{
	public enum OpCode
	{
		BRK_Implied = 0x00,

		AND_IndexedIndirect = 0x21,

		AND_ZeroPage = 0x25,

		AND_Immediate = 0x29,

		AND_Absolute = 0x2d,

		AND_IndirectIndexed = 0x31,

		AND_ZeroPageX = 0x35,

		SEC_Implied = 0x38,

		AND_AbsoluteY = 0x39,

		AND_AbsoluteX = 0x3d,

		ADC_IndexedIndirect = 0x61,

		ADC_ZeroPage = 0x65,

		ADC_Immediate = 0x69,

		ADC_Absolute = 0x6d,

		ADC_IndirectIndexed = 0x71,

		ADC_ZeroPageX = 0x75,

		ADC_AbsoluteY = 0x79,

		ADC_AbsoluteX = 0x7d,

		STA_IndexedIndirect = 0x81,

		STY_ZeroPage = 0x84,

		STA_ZeroPage = 0x85,

		STX_ZeroPage = 0x86,

		DEY_Implied = 0x88,

		TXA_Implied = 0x8a,

		STY_Absolute = 0x8c,

		STA_Absolute = 0x8d,

		STX_Absolute = 0x8e,

		BCC_Relative = 0x90,

		STA_IndirectIndexed = 0x91,

		STA_ZeroPageX = 0x95,

		STA_AbsoluteX = 0x9d,

		STY_ZeroPageX = 0x94,

		STX_ZeroPageY = 0x96,

		TYA_Implied = 0x98,

		STA_AbsoluteY = 0x99,

		LDY_Immediate = 0xa0,

		LDX_Immediate = 0xa2,

		LDY_ZeroPage = 0xa4,

		LDX_ZeroPage = 0xa6,

		TAY_Implied = 0xa8,

		LDA_IndexedIndirect = 0xa1,

		LDA_ZeroPage = 0xa5,

		LDA_Immediate = 0xa9,

		TAX_Implied = 0xaa,

		LDY_Absolute = 0xac,

		LDA_Absolute = 0xad,

		LDX_Absolute = 0xae,

		BCS_Relative = 0xb0,

		LDA_IndirectIndexed = 0xb1,

		LDY_ZeroPageX = 0xb4,

		LDA_ZeroPageX = 0xb5,

		LDX_ZeroPageY = 0xb6,

		LDA_AbsoluteY = 0xb9,

		LDY_AbsoluteX = 0xbc,

		LDA_AbsoluteX = 0xbd,

		LDX_AbsoluteY = 0xbe,

		CPY_Immediate = 0xc0,

		CPY_ZeroPage = 0xc4,

		INY_Implied = 0xc8,

		DEX_Implied = 0xca,

		CPY_Absolute = 0xcc,

		BNE_Relative = 0xd0,

		CPX_Immediate = 0xe0,

		SBC_IndexedIndirect = 0xe1,

		CPX_ZeroPage = 0xe4,

		SBC_ZeroPage = 0xe5,

		INX_Implied = 0xe8,

		SBC_Immediate = 0xe9,

		CPX_Absolute = 0xec,

		SBC_Absolute = 0xed,

		BEQ_Relative = 0xf0,

		SBC_IndirectIndexed = 0xf1,

		SBC_ZeroPageX = 0xf5,

		SBC_AbsoluteY = 0xf9,

		SBC_AbsoluteX = 0xfd
	}
}