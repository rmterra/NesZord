using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.SBC
{
	public class When_process_SBC_with_zero_page_addressing_mode_should
		: When_process_SBC_should<ZeroPageAddressingMode>
	{
		public When_process_SBC_with_zero_page_addressing_mode_should()
			: base(new ZeroPageAddressingMode(OpCode.SBC_ZeroPage))
		{
		}
	}
}
