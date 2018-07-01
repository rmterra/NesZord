using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.DEC
{
	public class When_process_DEC_with_absolute_x_addressing_mode_should
		: When_process_DEC_should<AbsoluteXAddressingMode>
	{
		public When_process_DEC_with_absolute_x_addressing_mode_should()
			: base(new AbsoluteXAddressingMode(OpCode.DEC_AbsoluteX))
		{
		}
	}
}
