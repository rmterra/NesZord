using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.CPX
{
	public class When_process_CPX_with_zero_page_addressing_mode_should
		: When_process_CPX_should<ZeroPageAddressingMode>
	{
		public When_process_CPX_with_zero_page_addressing_mode_should()
			: base(new ZeroPageAddressingMode(OpCode.CPX_ZeroPage))
		{
		}
	}
}
