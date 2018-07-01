using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.LSR
{
	public class When_process_LSR_with_zero_page_addressing_mode_should
		: When_process_LSR_should<ZeroPageAddressingMode>
	{
		public When_process_LSR_with_zero_page_addressing_mode_should()
			: base(new ZeroPageAddressingMode(OpCode.LSR_ZeroPage))
		{
		}
	}
}
