﻿using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.ROL
{
	public class When_process_ROL_with_zero_page_addressing_mode_should
		: When_process_ROL_should<ZeroPageAddressingMode>
	{
		public When_process_ROL_with_zero_page_addressing_mode_should()
			: base(new ZeroPageAddressingMode(OpCode.ROL_ZeroPage))
		{
		}
	}
}
