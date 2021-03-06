﻿using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.CPY
{
	public class When_process_CPY_with_absolute_addressing_mode_should
		: When_process_CPY_should<AbsoluteAddressingMode>
	{
		public When_process_CPY_with_absolute_addressing_mode_should()
			: base(new AbsoluteAddressingMode(OpCode.CPY_Absolute))
		{
		}
	}
}
