using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.ADC
{
	public class When_process_ADC_with_absolute_y_addressing_mode_should 
		: When_process_ADC_should<AbsoluteYAddressingMode>
	{
		public When_process_ADC_with_absolute_y_addressing_mode_should() 
			: base(new AbsoluteYAddressingMode())
		{
		}

		protected override void RunProgram()
			=> this.Processor.RunProgram(new byte[]
			{
				(byte)OpCode.LDA_Immediate, this.AccumulatorValue,
				(byte)OpCode.LDY_Immediate, this.AddressingMode.YRegisterValue,
				(byte)OpCode.ADC_AbsoluteY, this.AddressingMode.RandomOffset, this.AddressingMode.RandomPage
			});
	}
}
