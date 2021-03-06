﻿using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.ORA
{
	public class When_process_ORA_with_immediate_addressing_mode_should
		: When_process_ORA_should<ImmediateAddressingMode>
	{
		public When_process_ORA_with_immediate_addressing_mode_should()
			: base(new ImmediateAddressingMode(OpCode.ORA_Immediate))
		{
		}
	}
}
