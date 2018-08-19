using FluentAssertions;
using NesZord.Tests.New.AddressingMode;
using Xunit;

namespace NesZord.Tests.New.OpCodes.EOR
{
	public abstract class When_process_EOR_should<T> 
		: When_process_opcode_with_addressing_mode_should<T> where T : IAddressingMode
	{
		public When_process_EOR_should(T addressingMode)
			: base(addressingMode)
		{
			this.Processor.Accumulator.Value = 0xa9;
			this.OperationByte = 0x05;
		}

		[Fact]
		public void Set_bitwise_XOR_result_on_accumulator()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Processor.Accumulator.Value.Should().Be(0xac);
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
		public void Set_negative_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Processor.Negative.Should().BeTrue();
		}

		[Fact]
		public void Not_set_zero_flag_given_that_bitwise_xor_result_has_sign_bit_set()
		{
			// Arrange
			this.Processor.Accumulator.Value = 0x80;
			this.OperationByte = 0x00;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Zero.Should().BeFalse();
		}

		[Fact]
		public void Set_negative_flag_given_that_bitwise_xor_result_has_sign_bit_set()
		{
			// Arrange
			this.Processor.Accumulator.Value = 0x80;
			this.OperationByte = 0x00;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Negative.Should().BeTrue();
		}

		[Fact]
		public void Set_zero_flag_given_that_bitwise_xor_result_is_0x00()
		{
			// Arrange
			this.Processor.Accumulator.Value = 0x01;
			this.OperationByte = 0x01;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Zero.Should().BeTrue();
		}

		[Fact]
		public void Not_set_negative_flag_given_that_bitwise_xor_result_is_0x00()
		{
			// Arrange
			this.Processor.Accumulator.Value = 0x01;
			this.OperationByte = 0x01;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Negative.Should().BeFalse();
		}
	}
}
