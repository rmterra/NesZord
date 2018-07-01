﻿using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.ROL
{
	public class When_process_ROL_with_absolute_addressing_mode_should
		: When_process_ROL_should<AbsoluteAddressingMode>
	{
		public When_process_ROL_with_absolute_addressing_mode_should()
			: base(new AbsoluteAddressingMode(OpCode.ROL_Absolute))
		{
		}
	}
}
