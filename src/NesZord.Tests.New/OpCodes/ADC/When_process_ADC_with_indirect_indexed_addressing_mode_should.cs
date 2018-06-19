using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.ADC
{
	public class When_process_ADC_with_indirect_indexed_addressing_mode_should
		: When_process_ADC_should<IndirectIndexedAddressingMode>
	{
		public When_process_ADC_with_indirect_indexed_addressing_mode_should()
			: base(new IndirectIndexedAddressingMode(OpCode.ADC_IndirectIndexed))
		{
		}
	}
}
