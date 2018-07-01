using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.STA
{
	public class When_process_STA_with_absolute_y_addressing_mode_should
		: When_process_STA_should<AbsoluteYAddressingMode>
	{
		public When_process_STA_with_absolute_y_addressing_mode_should()
			: base(new AbsoluteYAddressingMode(OpCode.STA_AbsoluteY))
		{
		}
	}
}
