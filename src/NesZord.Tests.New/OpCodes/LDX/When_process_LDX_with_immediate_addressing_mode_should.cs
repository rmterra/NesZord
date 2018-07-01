﻿using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.LDX
{
	public class When_process_LDX_with_immediate_addressing_mode_should
		: When_process_LDX_should<ImmediateAddressingMode>
	{
		public When_process_LDX_with_immediate_addressing_mode_should()
			: base(new ImmediateAddressingMode(OpCode.LDX_Immediate))
		{
		}
	}
}
