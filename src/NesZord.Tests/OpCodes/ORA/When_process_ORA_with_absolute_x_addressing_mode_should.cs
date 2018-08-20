using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.ORA
{
	public class When_process_ORA_with_absolute_x_addressing_mode_should
		: When_process_ORA_should<AbsoluteXAddressingMode>
	{
		public When_process_ORA_with_absolute_x_addressing_mode_should()
			: base(new AbsoluteXAddressingMode(OpCode.ORA_AbsoluteX))
		{
		}
	}
}
