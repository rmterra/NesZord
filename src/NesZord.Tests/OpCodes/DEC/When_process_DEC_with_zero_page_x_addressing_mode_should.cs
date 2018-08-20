using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.DEC
{
	public class When_process_DEC_with_zero_page_x_addressing_mode_should 
		: When_process_DEC_should<ZeroPageXAddressingMode>
	{
		public When_process_DEC_with_zero_page_x_addressing_mode_should() 
			: base(new ZeroPageXAddressingMode(OpCode.DEC_ZeroPageX))
		{
		}
	}
}
