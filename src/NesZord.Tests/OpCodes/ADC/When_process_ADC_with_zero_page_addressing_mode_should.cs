﻿using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.ADC
{
	public class When_process_ADC_with_zero_page_addressing_mode_should
		: When_process_ADC_should<ZeroPageAddressingMode>
	{
		public When_process_ADC_with_zero_page_addressing_mode_should()
			: base(new ZeroPageAddressingMode(OpCode.ADC_ZeroPage))
		{
		}
	}
}
