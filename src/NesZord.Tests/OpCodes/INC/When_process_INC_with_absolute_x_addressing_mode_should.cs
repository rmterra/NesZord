using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.INC
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
