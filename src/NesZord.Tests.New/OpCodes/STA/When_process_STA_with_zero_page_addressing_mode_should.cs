using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.STA
{
	public class When_process_STA_with_zero_page_addressing_mode_should
		: When_process_STA_should<ZeroPageAddressingMode>
	{
		public When_process_STA_with_zero_page_addressing_mode_should()
			: base(new ZeroPageAddressingMode(OpCode.STA_ZeroPage))
		{
		}
	}
}
