using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.ROR
{
	public class When_process_ROR_with_immediate_addressing_mode_should
		: When_process_ROR_should<AccumulatorAddressingMode>
	{
		public When_process_ROR_with_immediate_addressing_mode_should()
			: base(new AccumulatorAddressingMode(OpCode.ROR_Accumulator))
		{
		}
	}
}
