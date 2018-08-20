using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.LDA
{
	public class When_process_LDA_with_absolute_addressing_mode_should
		: When_process_LDA_should<AbsoluteAddressingMode>
	{
		public When_process_LDA_with_absolute_addressing_mode_should()
			: base(new AbsoluteAddressingMode(OpCode.LDA_Absolute))
		{
		}
	}
}
