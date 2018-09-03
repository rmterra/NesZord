using FluentAssertions;
using NesZord.Core;
using Xunit;

namespace NesZord.Tests.OpCodes.INY
{
	public class When_process_INY_should : When_process_opcode_should
	{
		public When_process_INY_should()
		{
			this.Cpu.Y.Value = 0x05;
		}

		[Fact]
		public void Increment_1_to_y_register()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Y.Value.Should().Equals(0x06);
		}

		[Fact]
		public void Not_set_zero_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Zero.Should().BeFalse();
		}

		[Fact]
		public void Not_set_negative_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Negative.Should().BeFalse();
		}

		[Fact]
		public void Set_zero_flag_given_that_y_register_is_0xff()
		{
			// Arrange
			this.Cpu.Y.Value = 0xff;

			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Zero.Should().BeTrue();
		}

		[Fact]
		public void Not_set_negative_flag_given_that_y_register_is_0xff()
		{
			// Arrange
			this.Cpu.Y.Value = 0xff;

			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Negative.Should().BeFalse();
		}

		[Fact]
		public void Not_set_zero_flag_given_that_sign_bit_of_y_register_is_set()
		{
			// Arrange
			this.Cpu.Y.Value = 0x7f;

			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Zero.Should().BeFalse();
		}

		[Fact]
		public void Set_negative_flag_given_that_sign_bit_of_y_register_is_set()
		{
			// Arrange
			this.Cpu.Y.Value = 0x7f;

			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Negative.Should().BeTrue();
		}

		protected override void RunProgram()
		{
			this.Cpu.RunProgram(new byte[] { (byte) OpCode.INY_Implied });
		}
	}
}
