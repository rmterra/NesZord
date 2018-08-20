using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.LDY
{
	public class When_process_LDY_with_absolute_addressing_mode_should
		: When_process_LDY_should<AbsoluteAddressingMode>
	{
		public When_process_LDY_with_absolute_addressing_mode_should()
			: base(new AbsoluteAddressingMode(OpCode.LDY_Absolute))
		{
		}
	}
}
