using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.LSR
{
	public class When_process_LSR_with_zero_page_y_addressing_mode_should
		: When_process_LSR_should<ZeroPageXAddressingMode>
	{
		public When_process_LSR_with_zero_page_y_addressing_mode_should()
			: base(new ZeroPageXAddressingMode(OpCode.LSR_ZeroPageX))
		{
		}
	}
}
