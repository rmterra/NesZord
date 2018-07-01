using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.EOR
{
	public class When_process_EOR_with_absolute_x_addressing_mode_should
		: When_process_EOR_should<AbsoluteXAddressingMode>
	{
		public When_process_EOR_with_absolute_x_addressing_mode_should()
			: base(new AbsoluteXAddressingMode(OpCode.EOR_AbsoluteX))
		{
		}
	}
}
