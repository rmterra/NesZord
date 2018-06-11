using FluentAssertions;
using NesZord.Core;
using Xunit;

namespace NesZord.Tests.New.OpCodes.BVS
{
	public class When_process_BVS_should : When_process_opcode_should
	{
		[Fact]
		public void Accumulator_value_be_equal_0x00()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Processor.Accumulator.Value.Should().Equals(0x00);
		}

		[Fact]
		public void Set_overflow_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Processor.Overflow.Should().BeTrue();
		}

		protected override void RunProgram()
		{
			this.Processor.RunProgram(new byte[]
			{
				(byte) OpCode.LDA_Immediate, 0xff,
				(byte) OpCode.ADC_Immediate, 0x01,
				(byte) OpCode.BVS_Relative, 0x03,
				(byte) OpCode.LDA_Immediate, 0xff,
				(byte) OpCode.BRK_Implied
			});
		}
	}
}
