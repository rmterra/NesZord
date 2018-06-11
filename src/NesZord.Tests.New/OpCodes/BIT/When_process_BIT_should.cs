using FluentAssertions;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.BIT
{
	public abstract class When_process_BIT_should<T>
		: When_process_opcode_with_addressing_mode_should<T> where T : IAddressingMode
	{
		public When_process_BIT_should(T addressingMode) 
			: base(addressingMode)
		{
		}

		public void Not_set_negative_flag_given_that_sign_bit_is_not_set_on_accumulator_and_byte_to_test()
		{
			// Arrange
			this.Processor.Accumulator.Value = 0x05;
			this.OperationByte = 0x05;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Negative.Should().BeFalse();
		}

		public void Not_set_zero_flag_given_that_sign_bit_is_not_set_on_accumulator_and_byte_to_test()
		{
			// Arrange
			this.Processor.Accumulator.Value = 0x05;
			this.OperationByte = 0x05;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Zero.Should().BeFalse();
		}

		public void Not_set_overflow_flag_given_that_overflow_bit_is_not_set_on_accumulator_and_byte_to_test()
		{
			// Arrange
			this.Processor.Accumulator.Value = 0x05;
			this.OperationByte = 0x05;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Overflow.Should().BeFalse();
		}

		public void Not_set_zero_flag_given_that_overflow_bit_is_not_set_on_accumulator_and_byte_to_test()
		{
			// Arrange
			this.Processor.Accumulator.Value = 0x05;
			this.OperationByte = 0x05;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Zero.Should().BeFalse();
		}

		public void Not_set_negative_flag_given_that_sign_bit_is_not_set_on_accumulator_but_is_on_byte_to_test()
		{
			// Arrange
			this.Processor.Accumulator.Value = 0x05;
			this.OperationByte = 0x80;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Negative.Should().BeFalse();
		}

		public void Set_zero_flag_given_that_sign_bit_is_not_set_on_accumulator_but_is_on_byte_to_test()
		{
			// Arrange
			this.Processor.Accumulator.Value = 0x05;
			this.OperationByte = 0x80;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Zero.Should().BeTrue();
		}

		public void Not_set_overflow_flag_given_that_overflow_bit_is_not_set_on_accumulator_but_is_on_byte_to_test()
		{
			// Arrange
			this.Processor.Accumulator.Value = 0x05;
			this.OperationByte = 0x40;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Overflow.Should().BeFalse();
		}

		public void Set_zero_flag_given_that_overflow_bit_is_not_set_on_accumulator_but_is_on_byte_to_test()
		{
			// Arrange
			this.Processor.Accumulator.Value = 0x05;
			this.OperationByte = 0x40;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Zero.Should().BeTrue();
		}

		public void Set_negative_flag_given_that_sign_bit_is_set_on_accumulator_and_byte_to_test()
		{
			// Arrange
			this.Processor.Accumulator.Value = 0x80;
			this.OperationByte = 0x80;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Negative.Should().BeTrue();
		}

		public void Not_set_zero_flag_given_that_sign_bit_is_set_on_accumulator_and_byte_to_test()
		{
			// Arrange
			this.Processor.Accumulator.Value = 0x80;
			this.OperationByte = 0x80;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Zero.Should().BeFalse();
		}

		public void Set_overflow_flag_given_that_overflow_bit_is_set_on_accumulator_and_byte_to_test()
		{
			// Arrange
			this.Processor.Accumulator.Value = 0x40;
			this.OperationByte = 0x40;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Overflow.Should().BeTrue();
		}

		public void Not_set_zero_flag_given_that_overflow_bit_is_set_on_accumulator_and_byte_to_test()
		{
			// Arrange
			this.Processor.Accumulator.Value = 0x40;
			this.OperationByte = 0x40;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Zero.Should().BeFalse();
		}

		public void Set_zero_flag_given_that_all_bytes_from_accumulator_are_different_from_byte_to_test()
		{
			// Arrange
			this.Processor.Accumulator.Value = 0x40;
			this.OperationByte = 0xbf;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Zero.Should().BeTrue();
		}
	}
}
