using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.ASL
{
	public class When_process_ASL_with_zero_page_x_addressing_mode_should
		: When_process_ASL_should<ZeroPageXAddressingMode>
	{
		public When_process_ASL_with_zero_page_x_addressing_mode_should()
			: base(new ZeroPageXAddressingMode(OpCode.ASL_ZeroPageX))
		{
		}
	}
}
