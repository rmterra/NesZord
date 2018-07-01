using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.STA
{
	public class When_process_STA_with_indirect_indexed_addressing_mode_should
		: When_process_STA_should<IndirectIndexedAddressingMode>
	{
		public When_process_STA_with_indirect_indexed_addressing_mode_should()
			: base(new IndirectIndexedAddressingMode(OpCode.STA_IndirectIndexed))
		{
		}
	}
}
