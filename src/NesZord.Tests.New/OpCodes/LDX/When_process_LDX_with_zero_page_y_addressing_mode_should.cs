using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.LDX
{
	public class When_process_LDX_with_zero_page_y_addressing_mode_should
		: When_process_LDX_should<ZeroPageYAddressingMode>
	{
		public When_process_LDX_with_zero_page_y_addressing_mode_should()
			: base(new ZeroPageYAddressingMode(OpCode.LDX_ZeroPageY))
		{
		}
	}
}
