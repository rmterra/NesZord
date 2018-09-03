using FluentAssertions;
using NesZord.Tests.AddressingMode;
using Xunit;

namespace NesZord.Tests.OpCodes.LSR
{
	public abstract class When_process_LSR_should<T> 
		: When_process_opcode_with_addressing_mode_should<T> where T : IAddressingMode
	{
		public When_process_LSR_should(T addressingMode)
			: base(addressingMode)
		{
			this.OperationByte = 0x06;
		}

		[Fact]
		public void Memory_value_be_shifted_to_right()
		{
			// Act
			this.RunProgram();

			// Assert
			this.OperationByte.Should().Equals(0x03);
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

		[Fact]
		public void Not_set_negative_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Negative.Should().BeFalse();
		}

		[Fact]
		public void Set_carry_flag_given_that_byte_to_shift_has_sign_bit_set()
		{
			// Arrange
			this.OperationByte = 0x05;

			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Carry.Should().BeTrue();
		}

		[Fact]
		public void Not_set_zero_flag_given_that_byte_to_shift_has_sign_bit_set()
		{
			// Arrange
			this.OperationByte = 0x05;

			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Zero.Should().BeFalse();
		}

		[Fact]
		public void Not_set_negative_flag_given_that_byte_to_shift_has_sign_bit_set()
		{
			// Arrange
			this.OperationByte = 0x05;

			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Negative.Should().BeFalse();
		}

		[Fact]
		public void Set_carry_flag_given_that_shifted_value_result_is_0x00()
		{
			// Arrange
			this.OperationByte = 0x01;

			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Carry.Should().BeTrue();
		}

		[Fact]
		public void Set_zero_flag_given_that_shifted_value_result_is_0x00()
		{
			// Arrange
			this.OperationByte = 0x01;

			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Zero.Should().BeTrue();
		}
	}
}
