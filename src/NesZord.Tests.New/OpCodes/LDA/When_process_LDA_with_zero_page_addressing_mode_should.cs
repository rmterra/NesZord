using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.LDA
{
	public class When_process_LDA_with_zero_page_addressing_mode_should
		: When_process_LDA_should<ZeroPageAddressingMode>
	{
		public When_process_LDA_with_zero_page_addressing_mode_should()
			: base(new ZeroPageAddressingMode(OpCode.LDA_ZeroPage))
		{
		}
	}
}
