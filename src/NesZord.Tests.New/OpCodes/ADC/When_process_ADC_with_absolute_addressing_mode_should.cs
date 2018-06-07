using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.ADC
{
	public class When_process_ADC_with_absolute_addressing_mode_should 
		: When_process_ADC_should<AbsoluteAddressingMode>
	{
		public When_process_ADC_with_absolute_addressing_mode_should()
			: base(new AbsoluteAddressingMode())
		{
		}

		protected override void RunProgram()
			=> this.Processor.RunProgram(new byte[]
			{
				(byte)OpCode.LDA_Immediate, this.AccumulatorValue,
				(byte)OpCode.ADC_Absolute, this.AddressingMode.RandomOffset, this.AddressingMode.RandomPage
			});	
	}
}
