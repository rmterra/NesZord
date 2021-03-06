﻿using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.ROL
{
	public class When_process_ROL_with_immediate_addressing_mode_should
		: When_process_ROL_should<AccumulatorAddressingMode>
	{
		public When_process_ROL_with_immediate_addressing_mode_should()
			: base(new AccumulatorAddressingMode(OpCode.ROL_Accumulator))
		{
		}
	}
}
