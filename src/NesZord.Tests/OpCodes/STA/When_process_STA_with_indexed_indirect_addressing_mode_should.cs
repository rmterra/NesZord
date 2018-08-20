using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.STA
{
	public class When_process_STA_with_indexed_indirect_addressing_mode_should
		: When_process_STA_should<IndexedIndirectAddressingMode>
	{
		public When_process_STA_with_indexed_indirect_addressing_mode_should()
			: base(new IndexedIndirectAddressingMode(OpCode.STA_IndexedIndirect))
		{
		}
	}
}
