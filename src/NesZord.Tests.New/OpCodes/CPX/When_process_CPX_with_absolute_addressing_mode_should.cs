﻿using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.CPX
{
	public class When_process_CPX_with_absolute_addressing_mode_should
		: When_process_CPX_should<AbsoluteAddressingMode>
	{
		public When_process_CPX_with_absolute_addressing_mode_should()
			: base(new AbsoluteAddressingMode(OpCode.CPX_Absolute))
		{
		}
	}
}
