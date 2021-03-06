﻿using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.ROR
{
	public class When_process_ROR_with_zero_page_addressing_mode_should
		: When_process_ROR_should<ZeroPageAddressingMode>
	{
		public When_process_ROR_with_zero_page_addressing_mode_should()
			: base(new ZeroPageAddressingMode(OpCode.ROR_ZeroPage))
		{
		}
	}
}
