using FluentAssertions;
using NesZord.Core;
using Xunit;

namespace NesZord.Tests.OpCodes.BVC
{
	public class When_process_BVC_should : When_process_opcode_should
	{
		[Fact]
		public void Accumulator_value_be_equal_0xfe()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Accumulator.Value.Should().Equals(0xfe);
		}

		[Fact]
		public void Not_set_overflow_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Overflow.Should().BeFalse();
		}

		protected override void RunProgram()
		{
			this.Cpu.RunProgram(new byte[]
			{
				(byte) OpCode.LDA_Immediate, 0xfd,
				(byte) OpCode.ADC_Immediate, 0x01,
				(byte) OpCode.BVC_Relative, 0x03,
				(byte) OpCode.LDA_Immediate, 0xff,
				(byte) OpCode.BRK_Implied
			});
		}
	}
}
