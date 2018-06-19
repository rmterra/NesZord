using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.AND
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
