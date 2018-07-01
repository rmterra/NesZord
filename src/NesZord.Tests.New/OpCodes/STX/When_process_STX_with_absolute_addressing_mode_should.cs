﻿using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.STX
{
	public class When_process_STX_with_absolute_addressing_mode_should
		: When_process_STX_should<AbsoluteAddressingMode>
	{
		public When_process_STX_with_absolute_addressing_mode_should()
			: base(new AbsoluteAddressingMode(OpCode.STX_Absolute))
		{
		}
	}
}
