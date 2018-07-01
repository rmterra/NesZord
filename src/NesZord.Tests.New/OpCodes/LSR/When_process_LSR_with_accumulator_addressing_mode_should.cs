using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.LSR
{
	public class When_process_LSR_with_immediate_addressing_mode_should
		: When_process_LSR_should<AccumulatorAddressingMode>
	{
		public When_process_LSR_with_immediate_addressing_mode_should()
			: base(new AccumulatorAddressingMode(OpCode.LSR_Accumulator))
		{
		}
	}
}
