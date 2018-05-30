using AutoFixture;
using NesZord.Core;

namespace NesZord.Tests.New.OpCodes.ADC
{
	public class When_process_ADC_with_absolute_addressing_mode_should : When_process_ADC_should
	{
		private static Fixture fixture = new Fixture();

		private byte randomOffset;

		private byte randomPage;

		protected override void Initialize()
		{
			this.randomOffset = fixture.Create<byte>();
			this.randomPage = fixture.Create<byte>();
		}

		protected override void RunProgram()
			=> this.Processor.RunProgram(new byte[]
			{
				(byte)OpCode.LDA_Immediate, this.AccumulatorValue,
				(byte)OpCode.ADC_Absolute, this.randomOffset, this.randomPage
			});

		protected override void SetByteToAdd(byte value)
			=> this.Memory.Write(this.randomOffset, this.randomPage, value);
	}
}
