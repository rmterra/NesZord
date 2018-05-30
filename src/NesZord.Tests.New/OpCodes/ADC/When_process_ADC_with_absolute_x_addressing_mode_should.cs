using AutoFixture;
using NesZord.Core;

namespace NesZord.Tests.New.OpCodes.ADC
{
	public class When_process_ADC_with_absolute_x_addressing_mode_should : When_process_ADC_should
	{
		private static Fixture fixture = new Fixture();

		private byte randomOffset;

		private byte randomPage;

		private byte xRegisterValue;

		protected override void Initialize()
		{
			this.randomOffset = fixture.Create<byte>();
			this.randomPage = fixture.Create<byte>();
			this.xRegisterValue = fixture.Create<byte>();
		}

		protected override void RunProgram()
			=> this.Processor.RunProgram(new byte[]
			{
				(byte)OpCode.LDA_Immediate, this.AccumulatorValue,
				(byte)OpCode.LDX_Immediate, this.xRegisterValue,
				(byte)OpCode.ADC_AbsoluteX, this.randomOffset, this.randomPage
			});

		protected override void SetByteToAdd(byte value)
			=> this.Memory.Write(new MemoryLocation(this.randomOffset, this.randomPage).Sum(this.xRegisterValue), value);
	}
}
