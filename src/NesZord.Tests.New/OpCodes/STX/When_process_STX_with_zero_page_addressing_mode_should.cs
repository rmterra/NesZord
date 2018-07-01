using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.STX
{
	public class When_process_STX_with_zero_page_addressing_mode_should
		: When_process_STX_should<ZeroPageAddressingMode>
	{
		public When_process_STX_with_zero_page_addressing_mode_should()
			: base(new ZeroPageAddressingMode(OpCode.STX_ZeroPage))
		{
		}
	}
}
