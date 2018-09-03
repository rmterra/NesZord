using FluentAssertions;
using NesZord.Tests.AddressingMode;
using Xunit;

namespace NesZord.Tests.OpCodes.CPX
{
	public abstract class When_process_CPX_should<T>
		: When_process_opcode_with_addressing_mode_should<T> where T : IAddressingMode
	{
		public When_process_CPX_should(T addressingMode)
			: base(addressingMode)
		{
			this.Cpu.X.Value = 0x05;
		}

		[Fact]
		public void Not_set_negative_flag_given_that_x_register_value_is_lower_than_compared_byte()
		{
			// Arrange
			this.OperationByte = 0xff;

			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Negative.Should().BeFalse();
		}

		[Fact]
		public void Not_set_carry_flag_given_that_x_register_value_is_lower_than_compared_byte()
		{
			// Arrange
			this.OperationByte = 0xff;

			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Carry.Should().BeFalse();
		}

		[Fact]
		public void Not_set_zero_flag_given_that_x_register_value_is_lower_than_compared_byte()
		{
			// Arrange
			this.OperationByte = 0xff;

			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Zero.Should().BeFalse();
		}

		[Fact]
		public void Not_set_negative_flag_given_that_x_register_value_is_equal_than_compared_byte()
		{
			// Arrange
			this.OperationByte = 0x05;

			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Negative.Should().BeFalse();
		}

		[Fact]
		public void Set_carry_flag_given_that_x_register_value_is_equal_than_compared_byte()
		{
			// Arrange
			this.OperationByte = 0x05;

			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Carry.Should().BeTrue();
		}

		[Fact]
		public void Set_zero_flag_given_that_x_register_value_is_equal_than_compared_byte()
		{
			// Arrange
			this.OperationByte = 0x05;

			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Zero.Should().BeTrue();
		}

		[Fact]
		public void Not_set_negative_flag_given_that_x_register_value_is_greater_than_compared_byte()
		{
			// Arrange
			this.OperationByte = 0x00;

			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Negative.Should().BeFalse();
		}

		[Fact]
		public void Set_carry_flag_given_that_x_register_value_is_greater_than_compared_byte()
		{
			// Arrange
			this.OperationByte = 0x00;

			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Carry.Should().BeTrue();
		}

		[Fact]
		public void Not_set_zero_flag_given_that_x_register_value_is_greater_than_compared_byte()
		{
			// Arrange
			this.OperationByte = 0x00;

			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Zero.Should().BeFalse();
		}

		[Fact]
		public void Set_negative_flag_given_that_compared_result_is_greater_than_0x80()
		{
			// Arrange
			this.Cpu.X.Value = 0xff;
			this.OperationByte = 0x00;

			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Negative.Should().BeTrue();
		}

		[Fact]
		public void Set_carry_flag_given_that_compared_result_is_greater_than_0x80()
		{
			// Arrange
			this.Cpu.X.Value = 0xff;
			this.OperationByte = 0x00;

			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Carry.Should().BeTrue();
		}

		[Fact]
		public void Not_set_zero_flag_given_that_compared_result_is_greater_than_0x80()
		{
			// Arrange
			this.Cpu.X.Value = 0xff;
			this.OperationByte = 0x00;

			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Zero.Should().BeFalse();
		}
	}
}
