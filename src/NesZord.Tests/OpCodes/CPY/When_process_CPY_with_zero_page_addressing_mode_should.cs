using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.CPY
{
	public class When_process_CPY_with_zero_page_addressing_mode_should
		: When_process_CPY_should<ZeroPageAddressingMode>
	{
		public When_process_CPY_with_zero_page_addressing_mode_should()
			: base(new ZeroPageAddressingMode(OpCode.CPY_ZeroPage))
		{
		}
	}
}
