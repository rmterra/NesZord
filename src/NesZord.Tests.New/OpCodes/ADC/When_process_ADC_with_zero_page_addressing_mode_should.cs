using AutoFixture;
using NesZord.Core;

namespace NesZord.Tests.New.OpCodes.ADC
{
	public class When_process_ADC_with_zero_page_addressing_mode_should : When_process_ADC_should
	{
		private static Fixture fixture = new Fixture();

		private byte randomOffset;

		protected override void Initialize()
			=> this.randomOffset = fixture.Create<byte>();

		protected override void RunProgram()
			=> this.Processor.RunProgram(new byte[]
			{
				(byte)OpCode.LDA_Immediate, this.AccumulatorValue,
				(byte)OpCode.ADC_ZeroPage, randomOffset
			});

		protected override void SetByteToAdd(byte value)
			=> this.Memory.WriteZeroPage(this.randomOffset, value);
	}
}
