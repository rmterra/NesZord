using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.CMP
{
	public class When_process_CMP_with_indirect_indexed_addressing_mode_should
		: When_process_CMP_should<IndirectIndexedAddressingMode>
	{
		public When_process_CMP_with_indirect_indexed_addressing_mode_should()
			: base(new IndirectIndexedAddressingMode(OpCode.CMP_IndirectIndexed))
		{
		}
	}
}
