using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.ADC
{
	public class When_process_ADC_with_zero_page_x_addressing_mode_should 
		: When_process_ADC_should<ZeroPageXAddressingMode>
	{
		public When_process_ADC_with_zero_page_x_addressing_mode_should() 
			: base(new ZeroPageXAddressingMode(OpCode.ADC_ZeroPageX))
		{
		}
	}
}
