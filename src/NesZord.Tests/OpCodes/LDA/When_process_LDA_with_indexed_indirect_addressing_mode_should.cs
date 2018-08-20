using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.LDA
{
	public class When_process_LDA_with_indexed_indirect_addressing_mode_should
		: When_process_LDA_should<IndexedIndirectAddressingMode>
	{
		public When_process_LDA_with_indexed_indirect_addressing_mode_should()
			: base(new IndexedIndirectAddressingMode(OpCode.LDA_IndexedIndirect))
		{
		}
	}
}
