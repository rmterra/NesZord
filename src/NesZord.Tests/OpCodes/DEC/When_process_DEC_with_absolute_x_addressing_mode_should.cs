using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.DEC
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
