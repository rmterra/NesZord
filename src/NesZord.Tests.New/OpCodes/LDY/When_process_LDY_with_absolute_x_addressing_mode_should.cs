using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.LDY
{
	public class When_process_LDY_with_absolute_y_addressing_mode_should
		: When_process_LDY_should<AbsoluteXAddressingMode>
	{
		public When_process_LDY_with_absolute_y_addressing_mode_should()
			: base(new AbsoluteXAddressingMode(OpCode.LDY_AbsoluteX))
		{
		}
	}
}
