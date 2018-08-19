using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.ORA
{
	public class When_process_ORA_with_zero_page_x_addressing_mode_should
		: When_process_ORA_should<ZeroPageXAddressingMode>
	{
		public When_process_ORA_with_zero_page_x_addressing_mode_should()
			: base(new ZeroPageXAddressingMode(OpCode.ORA_ZeroPageX))
		{
		}
	}
}
