using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.JMP
{
	public class When_process_JMP_with_indirect_addressing_mode_should
		: When_process_JMP_should<IndirectIndexedAddressingMode>
	{
		public When_process_JMP_with_indirect_addressing_mode_should()
			: base(new IndirectIndexedAddressingMode(OpCode.JMP_Indirect))
		{
		}
	}
}
