using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.ADC
{
	public class When_process_ADC_with_immediate_addressing_mode_should
		: When_process_ADC_should<ImmediateAddressingMode>
	{
		public When_process_ADC_with_immediate_addressing_mode_should()
			: base(new ImmediateAddressingMode(OpCode.ADC_Immediate))
		{
		}
	}
}
