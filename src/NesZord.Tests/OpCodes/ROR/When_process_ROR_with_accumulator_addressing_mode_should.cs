using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.ROR
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
