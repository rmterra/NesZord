using AutoFixture;
using NesZord.Core;

namespace NesZord.Tests.New.OpCodes.ADC
{
	public class When_process_ADC_with_absolute_y_addressing_mode_should : When_process_ADC_should
	{
		private static Fixture fixture = new Fixture();

		private byte randomOffset;

		private byte randomPage;

		private byte yRegisterValue;

		protected override void Initialize()
		{
			this.randomOffset = fixture.Create<byte>();
			this.randomPage = fixture.Create<byte>();
			this.yRegisterValue = fixture.Create<byte>();
		}

		protected override void RunProgram()
			=> this.Processor.RunProgram(new byte[]
			{
				(byte)OpCode.LDA_Immediate, this.AccumulatorValue,
				(byte)OpCode.LDY_Immediate, this.yRegisterValue,
				(byte)OpCode.ADC_AbsoluteY, this.randomOffset, this.randomPage
			});

		protected override void SetByteToAdd(byte value)
			=> this.Memory.Write(new MemoryLocation(this.randomOffset, this.randomPage).Sum(this.yRegisterValue), value);
	}
}
