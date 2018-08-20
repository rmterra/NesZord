using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.ADC
{
	public class When_process_ADC_with_indexed_indirect_addressing_mode_should
		: When_process_ADC_should<IndexedIndirectAddressingMode>
	{
		public When_process_ADC_with_indexed_indirect_addressing_mode_should()
			: base(new IndexedIndirectAddressingMode(OpCode.ADC_IndexedIndirect))
		{
		}
	}
}
