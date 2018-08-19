using FluentAssertions;
using NesZord.Core;
using NesZord.Tests.New.AddressingMode;
using Xunit;

namespace NesZord.Tests.New.OpCodes.SBC
{
	public abstract class When_process_SBC_should<T> 
		: When_process_opcode_with_addressing_mode_should<T> where T : IAddressingMode
	{
		public When_process_SBC_should(T addressingMode)
			: base(addressingMode)
		{
			this.Processor.Accumulator.Value = 0x0b;
			this.OperationByte = 0x05;
		}

		[Fact]
		public void Subtract_the_specified_value_to_accumulator()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Processor.Accumulator.Value.Should().Be(0x05);
		}

		[Fact]
		public void Set_carry_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Processor.Carry.Should().BeTrue();
		}

		[Fact]
		public void Not_set_overflow_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Processor.Overflow.Should().BeFalse();
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
		public void Not_set_zero_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Processor.Zero.Should().BeFalse();
		}

		[Fact]
		public void Subtract_carry_flag_to_final_result_given_that_is_set()
		{
			// Arrange
			this.Processor.RunProgram(new byte[] { (byte)OpCode.SEC_Implied });

			//Act
			this.RunProgram();

			// Assert
			this.Processor.Accumulator.Value.Should().Be(0x06);
		}

		[Fact]
		public void Set_overflow_flag_given_that_accumulator_sign_bit_is_set()
		{
			// Arrange
			this.Processor.Accumulator.Value = 0xff;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Overflow.Should().BeTrue();
		}

		[Fact]
		public void Set_negative_flag_given_that_accumulator_sign_bit_is_set()
		{
			// Arrange
			this.Processor.Accumulator.Value = 0xff;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Negative.Should().BeTrue();
		}

		[Fact]
		public void Set_zero_flag_given_that_result_on_accumulator_is_0x00()
		{
			// Arrange
			this.Processor.Accumulator.Value = 0x01;
			this.OperationByte = 0x00;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Zero.Should().BeTrue();
		}

		[Fact]
		public void Set_overflow_flag_given_that_decimal_flag_is_set_and_operation_result_is_negative()
		{
			// Arrange
			this.Processor.Accumulator.Value = 0x02;
			this.OperationByte = 0x03;
			this.Processor.RunProgram(new byte[] { (byte)OpCode.SED_Implied });

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Overflow.Should().BeTrue();
		}

		[Fact]
		public void Not_set_overflow_flag_given_that_decimal_flag_is_set_and_operation_result_is_positive()
		{
			// Arrange
			this.Processor.Accumulator.Value = 0x03;
			this.OperationByte = 0x02;
			this.Processor.RunProgram(new byte[] { (byte)OpCode.SED_Implied });

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Overflow.Should().BeFalse();
		}

		[Fact]
		public void Set_overflow_flag_given_that_decimal_flag_is_not_set_and_operation_result_greater_than_127()
		{
			// Arrange
			this.OperationByte = 0xff;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Overflow.Should().BeTrue();
		}

		[Fact]
		public void Set_overflow_flag_given_that_decimal_flag_is_not_set_and_operation_result_is_lower_128_negative()
		{
			// Arrange
			this.OperationByte = 0xff;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Overflow.Should().BeTrue();
		}

		[Fact]
		public void Not_set_overflow_flag_given_that_decimal_flag_is_not_set_and_operation_result_is_between_128_negative_and_127()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Processor.Overflow.Should().BeFalse();
		}
	}
}
