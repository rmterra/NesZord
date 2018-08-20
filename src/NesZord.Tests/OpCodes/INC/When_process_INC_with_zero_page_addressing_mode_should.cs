using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.INC
{
	public class When_process_INC_with_zero_page_addressing_mode_should
		: When_process_INC_should<ZeroPageAddressingMode>
	{
		public When_process_INC_with_zero_page_addressing_mode_should()
			: base(new ZeroPageAddressingMode(OpCode.INC_ZeroPage))
		{
		}
	}
}
