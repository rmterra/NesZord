﻿using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.ORA
{
	public class When_process_ORA_with_absolute_addressing_mode_should
		: When_process_ORA_should<AbsoluteAddressingMode>
	{
		public When_process_ORA_with_absolute_addressing_mode_should()
			: base(new AbsoluteAddressingMode(OpCode.ORA_Absolute))
		{
		}
	}
}
