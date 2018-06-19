using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.BIT
{
	public class When_process_BIT_with_zero_page_addressing_mode_should
		: When_process_BIT_should<ZeroPageAddressingMode>
	{
		public When_process_BIT_with_zero_page_addressing_mode_should()
			: base(new ZeroPageAddressingMode(OpCode.BIT_ZeroPage))
		{
		}
	}
}
