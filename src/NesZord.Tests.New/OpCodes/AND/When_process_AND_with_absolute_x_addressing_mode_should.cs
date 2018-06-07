using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.AND
{
	public class When_process_AND_with_absolute_x_addressing_mode_should
		: When_process_AND_should<AbsoluteXAddressingMode>
	{
		public When_process_AND_with_absolute_x_addressing_mode_should()
			: base(new AbsoluteXAddressingMode())
		{
		}

		protected override void RunProgram()
			=> this.Processor.RunProgram(new byte[]
			{
				(byte) OpCode.LDA_Immediate, this.AccumulatorValue,
				(byte) OpCode.LDX_Immediate, this.AddressingMode.XRegisterValue,
				(byte) OpCode.AND_AbsoluteX, this.AddressingMode.RandomOffset, this.AddressingMode.RandomPage
			});
	}
}
