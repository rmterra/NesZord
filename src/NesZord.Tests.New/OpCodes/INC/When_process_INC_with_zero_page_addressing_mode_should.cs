using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.INC
{
	public class When_process_INC_with_zero_page_addressing_mode_should
		: When_process_INC_should<ZeroPageAddressingMode>
	{
		public When_process_INC_with_zero_page_addressing_mode_should()
			: base(new ZeroPageAddressingMode(OpCode.INC_ZeroPage))
		{
		}
	}
}
