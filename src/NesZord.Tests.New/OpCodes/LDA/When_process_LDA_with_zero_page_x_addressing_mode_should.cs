using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.LDA
{
	public class When_process_LDA_with_zero_page_x_addressing_mode_should 
		: When_process_LDA_should<ZeroPageXAddressingMode>
	{
		public When_process_LDA_with_zero_page_x_addressing_mode_should() 
			: base(new ZeroPageXAddressingMode(OpCode.LDA_ZeroPageX))
		{
		}
	}
}
