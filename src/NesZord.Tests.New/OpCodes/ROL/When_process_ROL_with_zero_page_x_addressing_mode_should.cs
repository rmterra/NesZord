using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.ROL
{
	public class When_process_ROL_with_zero_page_y_addressing_mode_should
		: When_process_ROL_should<ZeroPageXAddressingMode>
	{
		public When_process_ROL_with_zero_page_y_addressing_mode_should()
			: base(new ZeroPageXAddressingMode(OpCode.ROL_ZeroPageX))
		{
		}
	}
}
