using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.ORA
{
	public class When_process_ORA_with_zero_page_addressing_mode_should
		: When_process_ORA_should<ZeroPageAddressingMode>
	{
		public When_process_ORA_with_zero_page_addressing_mode_should()
			: base(new ZeroPageAddressingMode(OpCode.ORA_ZeroPage))
		{
		}
	}
}
