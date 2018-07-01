using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.EOR
{
	public class When_process_EOR_with_indirect_indexed_addressing_mode_should
		: When_process_EOR_should<IndirectIndexedAddressingMode>
	{
		public When_process_EOR_with_indirect_indexed_addressing_mode_should()
			: base(new IndirectIndexedAddressingMode(OpCode.EOR_IndirectIndexed))
		{
		}
	}
}
