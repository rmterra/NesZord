using FluentAssertions;
using NesZord.Core;
using Xunit;

namespace NesZord.Tests.New.OpCodes.BMI
{
	public class When_process_BMI_should : When_process_opcode_should
	{
		[Fact]
		public void X_register_value_be_equal_0x80()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Processor.X.Value.Should().Equals(0x80);
		}

		[Fact]
		public void Set_negative_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Processor.Negative.Should().BeTrue();
		}

		protected override void RunProgram()
		{
			this.Processor.RunProgram(new byte[]
			{
				(byte) OpCode.LDX_Immediate, 0x7f,
				(byte) OpCode.INX_Implied,
				(byte) OpCode.BMI_Relative, 0xfd,
				(byte) OpCode.LDX_Immediate, 0xff,
				(byte) OpCode.BRK_Implied
			});
		}
	}
}
