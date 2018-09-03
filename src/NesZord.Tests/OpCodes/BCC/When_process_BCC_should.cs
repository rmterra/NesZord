using FluentAssertions;
using NesZord.Core;
using Xunit;

namespace NesZord.Tests.OpCodes.BCC
{
	public class When_process_BCC_should : When_process_opcode_should
	{
		[Fact]
		public void X_register_value_be_equal_0x05()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Cpu.X.Value.Should().Equals(0x05);
		}

		[Fact]
		public void Not_set_carry_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Carry.Should().BeFalse();
		}

		[Fact]
		public void Not_set_zero_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Zero.Should().BeFalse();
		}

		protected override void RunProgram()
		{
			this.Cpu.RunProgram(new byte[]
			{
				(byte) OpCode.LDX_Immediate, 0x05,
				(byte) OpCode.CPX_Immediate, 0x0a,
				(byte) OpCode.BCC_Relative, 0x03,
				(byte) OpCode.LDX_Immediate, 0xff,
				(byte) OpCode.BRK_Implied
			});
		}
	}
}
