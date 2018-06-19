using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.AND
{
	public class When_process_AND_with_zero_page_addressing_mode_should
		: When_process_AND_should<ZeroPageAddressingMode>
	{
		public When_process_AND_with_zero_page_addressing_mode_should()
			: base(new ZeroPageAddressingMode(OpCode.AND_ZeroPage))
		{
		}
	}
}
