using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.LDA
{
	public class When_process_LDA_with_indirect_indexed_addressing_mode_should
		: When_process_LDA_should<IndirectIndexedAddressingMode>
	{
		public When_process_LDA_with_indirect_indexed_addressing_mode_should()
			: base(new IndirectIndexedAddressingMode(OpCode.LDA_IndirectIndexed))
		{
		}
	}
}
