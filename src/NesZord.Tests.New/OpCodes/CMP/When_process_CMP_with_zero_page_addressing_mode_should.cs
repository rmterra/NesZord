using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.CMP
{
	public class When_process_CMP_with_zero_page_addressing_mode_should
		: When_process_CMP_should<ZeroPageAddressingMode>
	{
		public When_process_CMP_with_zero_page_addressing_mode_should()
			: base(new ZeroPageAddressingMode(OpCode.CMP_ZeroPage))
		{
		}
	}
}
