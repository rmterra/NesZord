using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.ADC
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
