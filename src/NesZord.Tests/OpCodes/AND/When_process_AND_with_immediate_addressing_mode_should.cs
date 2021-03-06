﻿using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.AND
{
	public class When_process_AND_with_immediate_addressing_mode_should
		: When_process_AND_should<ImmediateAddressingMode>
	{
		public When_process_AND_with_immediate_addressing_mode_should()
			: base(new ImmediateAddressingMode(OpCode.AND_Immediate))
		{
		}
	}
}
