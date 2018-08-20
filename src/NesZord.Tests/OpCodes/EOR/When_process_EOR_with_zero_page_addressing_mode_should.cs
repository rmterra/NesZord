using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.EOR
{
	public class When_process_EOR_with_zero_page_addressing_mode_should
		: When_process_EOR_should<ZeroPageAddressingMode>
	{
		public When_process_EOR_with_zero_page_addressing_mode_should()
			: base(new ZeroPageAddressingMode(OpCode.EOR_ZeroPage))
		{
		}
	}
}
