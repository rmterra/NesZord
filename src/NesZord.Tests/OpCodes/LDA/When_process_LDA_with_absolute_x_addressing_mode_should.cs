using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.LDA
{
	public class When_process_LDA_with_absolute_x_addressing_mode_should
		: When_process_LDA_should<AbsoluteXAddressingMode>
	{
		public When_process_LDA_with_absolute_x_addressing_mode_should()
			: base(new AbsoluteXAddressingMode(OpCode.LDA_AbsoluteX))
		{
		}
	}
}
