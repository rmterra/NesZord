﻿using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.STY
{
	public class When_process_STY_with_zero_page_y_addressing_mode_should
		: When_process_STY_should<ZeroPageXAddressingMode>
	{
		public When_process_STY_with_zero_page_y_addressing_mode_should()
			: base(new ZeroPageXAddressingMode(OpCode.STY_ZeroPageX))
		{
		}
	}
}
