using FluentAssertions;
using NesZord.Tests.AddressingMode;
using Xunit;

namespace NesZord.Tests.OpCodes.DEC
{
	public abstract class When_process_DEC_should<T> 
		: When_process_opcode_with_addressing_mode_should<T> where T : IAddressingMode
	{
		public When_process_DEC_should(T addressingMode)
			: base(addressingMode)
		{
			this.OperationByte = 0x05;
		}

		[Fact]
		public void Decrement_1_to_memory_value()
		{
			// Act
			this.RunProgram();

			// Assert
			this.OperationByte.Should().Equals(0x04);
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
		public void Set_zero_flag_given_that_memory_value_is_0x01()
		{
			// Arrange
			this.OperationByte = 0x01;

			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Zero.Should().BeTrue();
		}

		[Fact]
		public void Not_set_negative_flag_given_that_memory_value_is_0x01()
		{
			// Arrange
			this.OperationByte = 0x01;

			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Negative.Should().BeFalse();
		}

		[Fact]
		public void Not_set_zero_flag_given_that_sign_bit_of_memory_value_is_set()
		{
			// Arrange
			this.OperationByte = 0x81;

			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Zero.Should().BeFalse();
		}

		[Fact]
		public void Set_negative_flag_given_that_sign_bit_of_memory_value_is_set()
		{
			// Arrange
			this.OperationByte = 0x81;

			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Negative.Should().BeTrue();
		}
	}
}
