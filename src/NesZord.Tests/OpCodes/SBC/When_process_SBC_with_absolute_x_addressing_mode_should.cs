﻿using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.SBC
{
	public class When_process_SBC_with_absolute_x_addressing_mode_should
		: When_process_SBC_should<AbsoluteXAddressingMode>
	{
		public When_process_SBC_with_absolute_x_addressing_mode_should()
			: base(new AbsoluteXAddressingMode(OpCode.SBC_AbsoluteX))
		{
		}
	}
}
