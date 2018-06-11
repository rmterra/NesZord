using FluentAssertions;
using NesZord.Core;
using NesZord.Tests.New.AddressingMode;
using Xunit;

namespace NesZord.Tests.New.OpCodes.ADC
{
	public abstract class When_process_ADC_should<T> 
		: When_process_opcode_with_addressing_mode_should<T> where T : IAddressingMode
	{
		public When_process_ADC_should(T addressingMode)
			: base(addressingMode)
		{
			this.AccumulatorValue = 0x05;
			this.OperationByte = 0x05;
		}

		protected byte AccumulatorValue { get; private set; }

		[Fact]
		public void Add_the_specified_value_to_accumulator()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Processor.Accumulator.Value.Should().Be(0x0a);
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
		public void Add_carry_flag_to_final_result_given_that_it_is_set()
		{
			// Arrange
			this.Processor.RunProgram(new byte[] { (byte)OpCode.SEC_Implied });

			//Act
			this.RunProgram();

			// Assert
			this.Processor.Accumulator.Value.Should().Be(0x0b);
		}

		[Fact]
		public void Set_overflow_flag_given_that_accumulator_sign_bit_is_set()
		{
			// Arrange
			this.AccumulatorValue = 0xff;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Overflow.Should().BeTrue();
		}

		[Fact]
		public void Set_negative_flag_given_that_accumulator_sign_bit_is_set()
		{
			// Arrange
			this.AccumulatorValue = 0xff;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Negative.Should().BeTrue();
		}

		[Fact]
		public void Set_zero_flag_given_that_result_on_accumulator_is_0x00()
		{
			// Arrange
			this.AccumulatorValue = 0x00;
			this.OperationByte = 0x00;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Zero.Should().BeTrue();
		}

		[Fact]
		public void Set_carry_flag_given_that_result_on_accumulator_is_negative()
		{
			// Arrange
			this.OperationByte = 0xff;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Carry.Should().BeTrue();
		}

		[Fact]
		public void Set_carry_flag_given_that_decimal_flag_is_set_and_added_byte_is_0x95()
		{
			// Arrange
			this.Processor.RunProgram(new byte[] { (byte)OpCode.SED_Implied });
			this.OperationByte = 0x95;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Carry.Should().BeTrue();
		}

		[Fact]
		public void Not_set_carry_flag_given_that_decimal_flag_is_set_and_added_byte_is_0x02()
		{
			// Arrange
			this.Processor.RunProgram(new byte[] { (byte)OpCode.SED_Implied });
			this.OperationByte = 0x02;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Carry.Should().BeFalse();
		}

		[Fact]
		public void Set_carry_flag_given_that_decimal_flag_is_not_set_and_added_byte_is_0xff()
		{
			// Arrange
			this.OperationByte = 0xff;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Carry.Should().BeTrue();
		}

		[Fact]
		public void Not_set_carry_flag_given_that_decimal_flag_is_not_set_and_added_byte_is_0x00()
		{
			// Arrange
			this.OperationByte = 0x00;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Carry.Should().BeFalse();
		}
	}
}
