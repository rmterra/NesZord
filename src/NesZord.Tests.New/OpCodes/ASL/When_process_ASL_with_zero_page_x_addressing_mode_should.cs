﻿using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.ASL
{
	public class When_process_ASL_with_zero_page_x_addressing_mode_should 
		: When_process_ASL_should<ZeroPageXAddressingMode>
	{
		public When_process_ASL_with_zero_page_x_addressing_mode_should() 
			: base(new ZeroPageXAddressingMode())
		{
		}

		protected override void RunProgram()
			=> this.Processor.RunProgram(new byte[]
			{
				(byte) OpCode.LDX_Immediate, this.AddressingMode.XRegisterValue,
				(byte) OpCode.ASL_ZeroPageX, this.AddressingMode.RandomOffset
			});
	}
}