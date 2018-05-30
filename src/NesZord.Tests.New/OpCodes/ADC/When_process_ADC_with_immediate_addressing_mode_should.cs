using NesZord.Core;

namespace NesZord.Tests.New.OpCodes.ADC
{
	public class When_process_ADC_with_immediate_addressing_mode_should : When_process_ADC_should
	{
		protected override void RunProgram()
			=> this.Processor.RunProgram(new byte[]
			{
				(byte)OpCode.LDA_Immediate, this.AccumulatorValue,
				(byte)OpCode.ADC_Immediate, this.ByteToAdd
			});

		protected override void SetByteToAdd(byte value)
			=> this.ByteToAdd = value;
	}
}
