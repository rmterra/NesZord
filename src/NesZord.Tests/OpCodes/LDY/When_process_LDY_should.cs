using FluentAssertions;
using NesZord.Tests.AddressingMode;
using Xunit;

namespace NesZord.Tests.OpCodes.LDY
{
	public abstract class When_process_LDY_should<T> 
		: When_process_opcode_with_addressing_mode_should<T> where T : IAddressingMode
	{
		public When_process_LDY_should(T addressingMode)
			: base(addressingMode)
		{
			this.OperationByte = 0x05;
		}

		[Fact]
		public void Set_y_register_with_received_value()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Y.Value.Should().Be(0x05);
		}

		[Fact]
		public void Set_zero_flag_given_that_new_y_register_value_is_0x00()
		{
			// Arrange
			this.OperationByte = 0x00;

			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Zero.Should().BeTrue();
		}

		[Fact]
		public void Not_set_negative_flag_given_that_new_y_register_value_is_0x00()
		{
			// Arrange
			this.OperationByte = 0x00;

			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Negative.Should().BeFalse();
		}

		[Fact]
		public void Not_set_zero_flag_given_that_new_y_register_value_has_sign_bit_set()
		{
			// Arrange
			this.OperationByte = 0x80;

			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Zero.Should().BeFalse();
		}

		[Fact]
		public void Set_negative_flag_given_that_new_y_register_has_sign_bit_set()
		{
			// Arrange
			this.OperationByte = 0x80;

			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Negative.Should().BeTrue();
		}
	}
}
