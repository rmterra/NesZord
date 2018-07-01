using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.LDA
{
	public class When_process_LDA_with_absolute_y_addressing_mode_should
		: When_process_LDA_should<AbsoluteYAddressingMode>
	{
		public When_process_LDA_with_absolute_y_addressing_mode_should()
			: base(new AbsoluteYAddressingMode(OpCode.LDA_AbsoluteY))
		{
		}
	}
}
