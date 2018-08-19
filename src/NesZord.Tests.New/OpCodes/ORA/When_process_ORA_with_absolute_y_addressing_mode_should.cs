﻿using AutoFixture;
using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.ORA
{
	public class When_process_ORA_with_absolute_y_addressing_mode_should
		: When_process_ORA_should<AbsoluteYAddressingMode>
	{
		public When_process_ORA_with_absolute_y_addressing_mode_should()
			: base(new AbsoluteYAddressingMode(OpCode.ORA_AbsoluteY))
		{
		}
	}
}
