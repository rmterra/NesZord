using FluentAssertions;
using NesZord.Core;
using Xunit;

namespace NesZord.Tests.OpCodes.DEY
{
	public class When_process_DEY_should : When_process_opcode_should
	{
		public When_process_DEY_should()
		{
			this.Cpu.Y.Value = 0x05;
		}

		[Fact]
		public void Decrement_1_to_x_register()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Y.Value.Should().Equals(0x04);
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
		public void Set_zero_flag_given_that_x_register_is_0x01()
		{
			// Arrange
			this.Cpu.Y.Value = 0x01;

			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Zero.Should().BeTrue();
		}

		[Fact]
		public void Not_set_negative_flag_given_that_x_register_is_0x01()
		{
			// Arrange
			this.Cpu.Y.Value = 0x01;

			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Negative.Should().BeFalse();
		}

		[Fact]
		public void Not_set_zero_flag_given_that_sign_bit_of_x_register_is_set()
		{
			// Arrange
			this.Cpu.Y.Value = 0x81;

			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Zero.Should().BeFalse();
		}

		[Fact]
		public void Set_negative_flag_given_that_sign_bit_of_x_register_is_set()
		{
			// Arrange
			this.Cpu.Y.Value = 0x81;

			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Negative.Should().BeTrue();
		}

		protected override void RunProgram()
		{
			this.Cpu.RunProgram(new byte[] { (byte) OpCode.DEY_Implied });
		}
	}
}
