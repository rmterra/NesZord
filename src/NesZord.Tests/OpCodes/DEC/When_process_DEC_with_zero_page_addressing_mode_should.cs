using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.DEC
{
	public class When_process_DEC_with_zero_page_addressing_mode_should
		: When_process_DEC_should<ZeroPageAddressingMode>
	{
		public When_process_DEC_with_zero_page_addressing_mode_should()
			: base(new ZeroPageAddressingMode(OpCode.DEC_ZeroPage))
		{
		}
	}
}
