using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.SBC
{
	public class When_process_SBC_with_zero_page_x_addressing_mode_should 
		: When_process_SBC_should<ZeroPageXAddressingMode>
	{
		public When_process_SBC_with_zero_page_x_addressing_mode_should() 
			: base(new ZeroPageXAddressingMode(OpCode.SBC_ZeroPageX))
		{
		}
	}
}
