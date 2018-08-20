using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.AND
{
	public class When_process_AND_with_absolute_x_addressing_mode_should
		: When_process_AND_should<AbsoluteXAddressingMode>
	{
		public When_process_AND_with_absolute_x_addressing_mode_should()
			: base(new AbsoluteXAddressingMode(OpCode.AND_AbsoluteX))
		{
		}
	}
}
