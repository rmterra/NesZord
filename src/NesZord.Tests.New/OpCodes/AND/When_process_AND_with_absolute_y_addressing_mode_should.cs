﻿using AutoFixture;
using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.AND
{
	public class When_process_AND_with_absolute_y_addressing_mode_should
		: When_process_AND_should<AbsoluteYAddressingMode>
	{
		public When_process_AND_with_absolute_y_addressing_mode_should()
			: base(new AbsoluteYAddressingMode(OpCode.AND_AbsoluteY))
		{
		}
	}
}
