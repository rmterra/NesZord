using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.ADC
{
	public class When_process_ADC_with_absolute_x_addressing_mode_should
		: When_process_ADC_should<AbsoluteXAddressingMode>
	{
		public When_process_ADC_with_absolute_x_addressing_mode_should()
			: base(new AbsoluteXAddressingMode(OpCode.ADC_AbsoluteX))
		{
		}
	}
}
