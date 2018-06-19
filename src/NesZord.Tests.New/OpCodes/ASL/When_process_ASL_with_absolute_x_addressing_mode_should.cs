﻿using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.ASL
{
	public class When_process_ASL_with_absolute_x_addressing_mode_should
		: When_process_ASL_should<AbsoluteXAddressingMode>
	{
		public When_process_ASL_with_absolute_x_addressing_mode_should()
			: base(new AbsoluteXAddressingMode(OpCode.ASL_AbsoluteX))
		{
		}
	}
}
