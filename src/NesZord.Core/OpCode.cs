namespace NesZord.Core
{
	public enum OpCode
	{
		BRK = 0x00,

		IndexedAND = 0x21,

		ZeroPageAND = 0x25,

		ImmediateAND = 0x29,

		AbsoluteAND = 0x2d,

		IndirectIndexedAND = 0x31,

		ZeroPageXAND = 0x35,

		SEC = 0x38,

		AbsoluteYAND = 0x39,

		AbsoluteXAND = 0x3d,

		IndexedIndirectADC = 0x61,

		ZeroPageADC = 0x65,

		ImmediateADC = 0x69,

		AbsoluteADC = 0x6d,

		IndirectIndexedADC = 0x71,

		ZeroPageXADC = 0x75,

		AbsoluteYADC = 0x79,

		AbsoluteXADC = 0x7d,

		IndexedIndirectSTA = 0x81,

		ZeroPageSTY = 0x84,

		ZeroPageSTA = 0x85,

		ZeroPageSTX = 0x86,

		DEY = 0x88,

		TXA = 0x8a,

		AbsoluteSTY = 0x8c,

		AbsoluteSTA = 0x8d,

		AbsoluteSTX = 0x8e,

		BCC = 0x90,

		IndirectIndexedSTA = 0x91,

		ZeroPageXSTA = 0x95,

		AbsoluteXSTA = 0x9d,

		ZeroPageXSTY = 0x94,

		ZeroPageYSTX = 0x96,

		TYA = 0x98,

		AbsoluteYSTA = 0x99,

		ImmediateLDY = 0xa0,

		ImmediateLDX = 0xa2,

		ZeroPageLDY = 0xa4,

		ZeroPageLDX = 0xa6,

		TAY = 0xa8,

		IndexedIndirectLDA = 0xa1,

		ZeroPageLDA = 0xa5,

		ImmediateLDA = 0xa9,

		TAX = 0xaa,

		AbsoluteLDY = 0xac,

		AbsoluteLDA = 0xad,

		AbsoluteLDX = 0xae,

		BCS = 0xb0,

		IndirectIndexedLDA = 0xb1,

		ZeroPageXLDY = 0xb4,

		ZeroPageXLDA = 0xb5,

		ZeroPageYLDX = 0xb6,

		AbsoluteYLDA = 0xb9,

		AbsoluteXLDY = 0xbc,

		AbsoluteXLDA = 0xbd,

		AbsoluteYLDX = 0xbe,

		ImmediateCPY = 0xc0,

		ZeroPageCPY = 0xc4,

		INY = 0xc8,

		DEX = 0xca,

		AbsoluteCPY = 0xcc,

		BNE = 0xd0,

		ImmediateCPX = 0xe0,

		IndexedIndirectSBC = 0xe1,

		ZeroPageCPX = 0xe4,

		ZeroPageSBC = 0xe5,

		INX = 0xe8,

		ImmediateSBC = 0xe9,

		AbsoluteCPX = 0xec,

		AbsoluteSBC = 0xed,

		BEQ = 0xf0,

		IndirectIndexedSBC = 0xf1,

		ZeroPageXSBC = 0xf5,

		AbsoluteYSBC = 0xf9,

		AbsoluteXSBC = 0xfd
	}
}