using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.SBC
{
	public class When_process_SBC_with_indirect_indexed_addressing_mode_should
		: When_process_SBC_should<IndirectIndexedAddressingMode>
	{
		public When_process_SBC_with_indirect_indexed_addressing_mode_should()
			: base(new IndirectIndexedAddressingMode(OpCode.SBC_IndirectIndexed))
		{
		}
	}
}
