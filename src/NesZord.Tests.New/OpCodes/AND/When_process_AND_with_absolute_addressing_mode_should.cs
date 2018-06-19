﻿using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.AND
{
	public class When_process_AND_with_absolute_addressing_mode_should
		: When_process_AND_should<AbsoluteAddressingMode>
	{
		public When_process_AND_with_absolute_addressing_mode_should()
			: base(new AbsoluteAddressingMode(OpCode.AND_Absolute))
		{
		}
	}
}
