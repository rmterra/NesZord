using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.ASL
{
	public class When_process_ASL_with_zero_page_addressing_mode_should
		: When_process_ASL_should<ZeroPageAddressingMode>
	{
		public When_process_ASL_with_zero_page_addressing_mode_should()
			: base(new ZeroPageAddressingMode(OpCode.ASL_ZeroPageX))
		{
		}
	}
}
