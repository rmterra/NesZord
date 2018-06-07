using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.ADC
{
	public class When_process_ADC_with_zero_page_x_addressing_mode_should 
		: When_process_ADC_should<ZeroPageXAddressingMode>
	{
		public When_process_ADC_with_zero_page_x_addressing_mode_should() 
			: base(new ZeroPageXAddressingMode())
		{
		}

		protected override void RunProgram()
			=> this.Processor.RunProgram(new byte[]
			{
				(byte)OpCode.LDA_Immediate, this.AccumulatorValue,
				(byte)OpCode.LDX_Immediate, this.AddressingMode.XRegisterValue,
				(byte)OpCode.ADC_ZeroPageX, this.AddressingMode.RandomOffset
			});
	}
}
