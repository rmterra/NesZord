using AutoFixture;
using NesZord.Core;

namespace NesZord.Tests.New.OpCodes.ADC
{
	public class When_process_ADC_with_indirect_indexed_addressing_mode_should : When_process_ADC_should
	{
		private static Fixture fixture = new Fixture();

		private byte randomOffset;

		private byte yRegisterValue;

		protected override void Initialize()
		{
			this.randomOffset = fixture.Create<byte>();
			this.yRegisterValue = fixture.Create<byte>();
		}

		protected override void RunProgram()
			=> this.Processor.RunProgram(new byte[]
			{
				(byte)OpCode.LDA_Immediate, this.AccumulatorValue,
				(byte)OpCode.LDY_Immediate, this.yRegisterValue,
				(byte)OpCode.ADC_IndirectIndexed, this.randomOffset
			});

		protected override void SetByteToAdd(byte value)
			=> this.Memory.MockIndirectIndexedMemoryWrite(this.randomOffset, this.yRegisterValue, value);
	}
}
