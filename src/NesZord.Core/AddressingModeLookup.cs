namespace NesZord.Core
{
	public static class AddressingModeLookup
	{
		public static AddressingMode For(OpCode opCode)
		{
			switch (opCode)
			{
				case OpCode.AbsoluteADC:
				case OpCode.AbsoluteAND:
				case OpCode.AbsoluteCPY:
				case OpCode.AbsoluteCPX:
				case OpCode.AbsoluteLDA:
				case OpCode.AbsoluteLDX:
				case OpCode.AbsoluteLDY:
				case OpCode.AbsoluteSTY:
				case OpCode.AbsoluteSTA:
				case OpCode.AbsoluteSTX:
				case OpCode.AbsoluteSBC:
					return AddressingMode.Absolute;

				case OpCode.AbsoluteXADC:
				case OpCode.AbsoluteXAND:
				case OpCode.AbsoluteXLDA:
				case OpCode.AbsoluteXLDY:
				case OpCode.AbsoluteXSTA:
				case OpCode.AbsoluteXSBC:
					return AddressingMode.AbsoluteX;

				case OpCode.AbsoluteYADC:
				case OpCode.AbsoluteYAND:
				case OpCode.AbsoluteYLDA:
				case OpCode.AbsoluteYLDX:
				case OpCode.AbsoluteYSTA:
				case OpCode.AbsoluteYSBC:
					return AddressingMode.AbsoluteY;

				case OpCode.IndexedIndirectADC:
				case OpCode.IndexedAND:
				case OpCode.IndexedIndirectLDA:
				case OpCode.IndexedIndirectSTA:
				case OpCode.IndexedIndirectSBC:
					return AddressingMode.IndexedIndirect;

				case OpCode.IndirectIndexedADC:
				case OpCode.IndirectIndexedAND:
				case OpCode.IndirectIndexedLDA:
				case OpCode.IndirectIndexedSTA:
				case OpCode.IndirectIndexedSBC:
                    return AddressingMode.IndirectIndexed;

				case OpCode.ImmediateADC:
				case OpCode.ImmediateAND:
				case OpCode.ImmediateLDY:
				case OpCode.ImmediateLDX:
				case OpCode.ImmediateLDA:
				case OpCode.ImmediateCPY:
				case OpCode.ImmediateCPX:
				case OpCode.ImmediateSBC:
					return AddressingMode.Immediate;

				case OpCode.BRK:
				case OpCode.SEC:
				case OpCode.TXA:
				case OpCode.TAX:
				case OpCode.DEX:
				case OpCode.INY:
				case OpCode.INX:
					return AddressingMode.Implied;

				case OpCode.BCC:
				case OpCode.BCS:
				case OpCode.BNE:
				case OpCode.BEQ:
					return AddressingMode.Relative;

				case OpCode.ZeroPageADC:
				case OpCode.ZeroPageAND:
				case OpCode.ZeroPageCPY:
				case OpCode.ZeroPageCPX:
				case OpCode.ZeroPageLDA:
				case OpCode.ZeroPageLDX:
				case OpCode.ZeroPageLDY:
				case OpCode.ZeroPageSTA:
				case OpCode.ZeroPageSTX:
				case OpCode.ZeroPageSTY:
				case OpCode.ZeroPageSBC:
					return AddressingMode.ZeroPage;

				case OpCode.ZeroPageXADC:
				case OpCode.ZeroPageXAND:
				case OpCode.ZeroPageXLDA:
				case OpCode.ZeroPageXLDY:
				case OpCode.ZeroPageXSTA:
				case OpCode.ZeroPageXSTY:
				case OpCode.ZeroPageXSBC:
					return AddressingMode.ZeroPageX;

				case OpCode.ZeroPageYLDX:
				case OpCode.ZeroPageYSTX:
					return AddressingMode.ZeroPageY;

				default:
					return AddressingMode.Undefined;
			}
		}
	}
}