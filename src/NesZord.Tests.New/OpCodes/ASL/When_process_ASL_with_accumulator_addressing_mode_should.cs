using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.ASL
{
	public class When_process_ASL_with_accumulator_addressing_mode_should
		: When_process_ASL_should<AccumulatorAddressingMode>
	{
		public When_process_ASL_with_accumulator_addressing_mode_should()
			: base(new AccumulatorAddressingMode(OpCode.ASL_Accumulator))
		{
		}
	}
}
