using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.CMP
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
