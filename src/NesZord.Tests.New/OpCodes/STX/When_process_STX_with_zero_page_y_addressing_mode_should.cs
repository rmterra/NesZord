using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.STX
{
	public class When_process_STX_with_zero_page_y_addressing_mode_should
		: When_process_STX_should<ZeroPageYAddressingMode>
	{
		public When_process_STX_with_zero_page_y_addressing_mode_should()
			: base(new ZeroPageYAddressingMode(OpCode.STX_ZeroPageY))
		{
		}
	}
}
