using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.INC
{
	public class When_process_INC_with_absolute_addressing_mode_should
		: When_process_INC_should<AbsoluteAddressingMode>
	{
		public When_process_INC_with_absolute_addressing_mode_should()
			: base(new AbsoluteAddressingMode(OpCode.INC_Absolute))
		{
		}
	}
}
