using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.ASL
{
	public class When_process_ASL_with_accumulator_addressing_mode_should
		: When_process_ASL_should<AccumulatorAddressingMode>
	{
		public When_process_ASL_with_accumulator_addressing_mode_should()
			: base(new AccumulatorAddressingMode())
		{
		}

		protected override void RunProgram()
			=> this.Processor.RunProgram(new byte[] { (byte)OpCode.ASL_Accumulator });
	}
}
