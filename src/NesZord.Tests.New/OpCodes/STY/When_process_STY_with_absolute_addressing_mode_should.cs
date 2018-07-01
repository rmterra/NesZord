using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.STY
{
	public class When_process_STY_with_absolute_addressing_mode_should
		: When_process_STY_should<AbsoluteAddressingMode>
	{
		public When_process_STY_with_absolute_addressing_mode_should()
			: base(new AbsoluteAddressingMode(OpCode.STY_Absolute))
		{
		}
	}
}
