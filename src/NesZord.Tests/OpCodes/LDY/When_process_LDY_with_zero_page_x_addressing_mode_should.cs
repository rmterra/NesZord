using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.LDY
{
	public class When_process_LDY_with_zero_page_y_addressing_mode_should
		: When_process_LDY_should<ZeroPageXAddressingMode>
	{
		public When_process_LDY_with_zero_page_y_addressing_mode_should()
			: base(new ZeroPageXAddressingMode(OpCode.LDY_ZeroPageX))
		{
		}
	}
}
