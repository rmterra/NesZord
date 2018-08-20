using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.STA
{
	public class When_process_STA_with_absolute_x_addressing_mode_should
		: When_process_STA_should<AbsoluteXAddressingMode>
	{
		public When_process_STA_with_absolute_x_addressing_mode_should()
			: base(new AbsoluteXAddressingMode(OpCode.STA_AbsoluteX))
		{
		}
	}
}
