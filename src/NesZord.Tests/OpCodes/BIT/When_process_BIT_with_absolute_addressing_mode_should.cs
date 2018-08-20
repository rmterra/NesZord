using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.BIT
{
	public class When_process_BIT_with_absolute_addressing_mode_should
		: When_process_BIT_should<AbsoluteAddressingMode>
	{
		public When_process_BIT_with_absolute_addressing_mode_should()
			: base(new AbsoluteAddressingMode(OpCode.BIT_Absolute))
		{
		}
	}
}
