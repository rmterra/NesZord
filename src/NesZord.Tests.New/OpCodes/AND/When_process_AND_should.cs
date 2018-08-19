using FluentAssertions;
using NesZord.Tests.New.AddressingMode;
using Xunit;

namespace NesZord.Tests.New.OpCodes.AND
{
	public abstract class When_process_AND_should<T>
		: When_process_opcode_with_addressing_mode_should<T> where T : IAddressingMode
	{
		public When_process_AND_should(T addressingMode)
			: base(addressingMode)
		{
			this.Processor.Accumulator.Value = 0xa9;
			this.OperationByte = 0x05;
		}

		[Fact]
		public void Set_bitwise_AND_result_on_Accumulator()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Processor.Accumulator.Value.Should().Be(0x01);
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
		public void Set_negative_flag_given_that_accumulator_sign_bit_is_set()
		{
			// Arrange
			this.Processor.Accumulator.Value = 0x80;
			this.OperationByte = 0x80;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Negative.Should().BeTrue();
		}

		[Fact]
		public void Set_zero_flag_given_that_operation_result_over_accumulator_is_0x00()
		{
			// Arrange
			this.Processor.Accumulator.Value = 0x00;
			this.OperationByte = 0x00;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Zero.Should().BeTrue();
		}
	}
}
