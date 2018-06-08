using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.ASL
{
	public class When_process_ASL_with_absolute_x_addressing_mode_should
		: When_process_ASL_should<AbsoluteXAddressingMode>
	{
		public When_process_ASL_with_absolute_x_addressing_mode_should()
			: base(new AbsoluteXAddressingMode())
		{
		}

		protected override void RunProgram()
			=> this.Processor.RunProgram(new byte[]
			{
				(byte)OpCode.LDX_Immediate, this.AddressingMode.XRegisterValue,
				(byte)OpCode.ASL_AbsoluteX, this.AddressingMode.RandomOffset, this.AddressingMode.RandomPage
			});
	}
}
