﻿using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.STY
{
	public class When_process_STY_with_zero_page_addressing_mode_should
		: When_process_STY_should<ZeroPageAddressingMode>
	{
		public When_process_STY_with_zero_page_addressing_mode_should()
			: base(new ZeroPageAddressingMode(OpCode.STY_ZeroPage))
		{
		}
	}
}
