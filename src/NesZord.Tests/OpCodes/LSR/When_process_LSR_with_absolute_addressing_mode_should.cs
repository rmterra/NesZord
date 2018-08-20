using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.LSR
{
	public class When_process_LSR_with_absolute_addressing_mode_should
		: When_process_LSR_should<AbsoluteAddressingMode>
	{
		public When_process_LSR_with_absolute_addressing_mode_should()
			: base(new AbsoluteAddressingMode(OpCode.LSR_Absolute))
		{
		}
	}
}
