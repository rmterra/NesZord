using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.LSR
{
	public class When_process_LSR_with_absolute_y_addressing_mode_should
		: When_process_LSR_should<AbsoluteXAddressingMode>
	{
		public When_process_LSR_with_absolute_y_addressing_mode_should()
			: base(new AbsoluteXAddressingMode(OpCode.LSR_AbsoluteX))
		{
		}
	}
}
