using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.LDX
{
	public class When_process_LDX_with_zero_page_addressing_mode_should
		: When_process_LDX_should<ZeroPageAddressingMode>
	{
		public When_process_LDX_with_zero_page_addressing_mode_should()
			: base(new ZeroPageAddressingMode(OpCode.LDX_ZeroPage))
		{
		}
	}
}
