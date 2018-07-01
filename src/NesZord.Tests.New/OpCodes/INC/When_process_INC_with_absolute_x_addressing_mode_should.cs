using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.INC
{
	public class When_process_INC_with_absolute_x_addressing_mode_should
		: When_process_INC_should<AbsoluteXAddressingMode>
	{
		public When_process_INC_with_absolute_x_addressing_mode_should()
			: base(new AbsoluteXAddressingMode(OpCode.INC_AbsoluteX))
		{
		}
	}
}
