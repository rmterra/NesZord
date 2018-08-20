using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.LDY
{
	public class When_process_LDY_with_zero_page_addressing_mode_should
		: When_process_LDY_should<ZeroPageAddressingMode>
	{
		public When_process_LDY_with_zero_page_addressing_mode_should()
			: base(new ZeroPageAddressingMode(OpCode.LDY_ZeroPage))
		{
		}
	}
}
