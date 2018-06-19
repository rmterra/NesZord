using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.ADC
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
