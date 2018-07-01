using FluentAssertions;
using NesZord.Core;
using NesZord.Tests.New.AddressingMode;
using Xunit;

namespace NesZord.Tests.New.OpCodes.ROL
{
	public abstract class When_process_ROL_should<T> 
		: When_process_opcode_with_addressing_mode_should<T> where T : IAddressingMode
	{
		public When_process_ROL_should(T addressingMode)
			: base(addressingMode)
		{
			this.OperationByte = 0x05;
			this.Processor.RunProgram(new byte[] { (byte)OpCode.CLC_Implied });
		}

		[Fact]
		public void Memory_value_be_shifted_to_left()
		{
			// Act
			this.RunProgram();

			// Assert
			this.OperationByte.Should().Equals(0x0a);
		}

		[Fact]
		public void Memory_value_be_shifted_to_left_and_incremented_given_that_carry_flag_is_set()
		{
			// Arrange
			this.Processor.RunProgram(new byte[] { (byte)OpCode.SEC_Implied });

			// Act
			this.RunProgram();

			// Assert
			this.OperationByte.Should().Equals(0x0b);
		}

		[Fact]
		public void Not_set_carry_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Processor.Carry.Should().BeFalse();
		}

		[Fact]
		public void Not_set_zero_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Processor.Zero.Should().BeFalse();
		}

		[Fact]
		public void Not_set_negative_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Processor.Negative.Should().BeFalse();
		}

		[Fact]
		public void Set_carry_flag_given_that_byte_to_shift_has_sign_bit_set()
		{
			// Arrange
			this.OperationByte = 0x80;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Carry.Should().BeTrue();
		}

		[Fact]
		public void Set_zero_flag_given_that_byte_to_shift_has_sign_bit_set()
		{
			// Arrange
			this.OperationByte = 0x80;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Zero.Should().BeTrue();
		}

		[Fact]
		public void Set_negative_flag_given_that_shift_result_has_sign_bit_set()
		{
			// Arrange
			this.OperationByte = 0x40;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Negative.Should().BeTrue();
		}
	}
}
