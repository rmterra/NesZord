﻿using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.CPY
{
	public class When_process_CPY_with_immediate_addressing_mode_should 
		: When_process_CPY_should<ImmediateAddressingMode>
	{
		public When_process_CPY_with_immediate_addressing_mode_should() 
			: base(new ImmediateAddressingMode(OpCode.CPY_Immediate))
		{
		}
	}
}
