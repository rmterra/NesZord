using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.SBC
{
	public class When_process_SBC_with_indexed_indirect_addressing_mode_should
		: When_process_SBC_should<IndexedIndirectAddressingMode>
	{
		public When_process_SBC_with_indexed_indirect_addressing_mode_should()
			: base(new IndexedIndirectAddressingMode(OpCode.SBC_IndexedIndirect))
		{
		}
	}
}
