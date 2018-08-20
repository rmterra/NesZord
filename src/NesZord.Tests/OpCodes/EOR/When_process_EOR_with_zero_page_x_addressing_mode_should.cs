using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.EOR
{
	public class When_process_EOR_with_zero_page_x_addressing_mode_should 
		: When_process_EOR_should<ZeroPageXAddressingMode>
	{
		public When_process_EOR_with_zero_page_x_addressing_mode_should() 
			: base(new ZeroPageXAddressingMode(OpCode.EOR_ZeroPageX))
		{
		}
	}
}
