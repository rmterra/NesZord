using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.STA
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
