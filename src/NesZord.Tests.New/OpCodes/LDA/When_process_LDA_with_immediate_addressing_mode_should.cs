using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.LDA
{
	public class When_process_LDA_with_immediate_addressing_mode_should
		: When_process_LDA_should<ImmediateAddressingMode>
	{
		public When_process_LDA_with_immediate_addressing_mode_should()
			: base(new ImmediateAddressingMode(OpCode.LDA_Immediate))
		{
		}
	}
}
