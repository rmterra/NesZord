using FluentAssertions;
using NesZord.Tests.New.AddressingMode;
using Xunit;

namespace NesZord.Tests.New.OpCodes.ASL
{
	public abstract class When_process_ASL_should<T>
		: When_process_opcode_with_addressing_mode_should<T> where T : IAddressingMode
	{
		public When_process_ASL_should(T addressingMode) 
			: base(addressingMode)
		{
			this.OperationByte = 0x05;
		}

		[Fact]
		public void Byte_to_test_be_shifted_to_left()
		{
			// Act
			this.RunProgram();

			// Assert
			this.OperationByte.Should().Equals(0x0a);
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
		public void Set_carry_flag_given_that_byte_to_shift_has_sign_bit_set()
		{
			// Arrage
			this.OperationByte = 0x80;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Carry.Should().BeTrue();
		}

		[Fact]
		public void Set_negative_flag_given_that_shift_result_has_sign_bit_set()
		{
			// Arrage
			this.OperationByte = 0x40;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Negative.Should().BeTrue();
		}

		[Fact]
		public void Set_zero_flag_given_that_shift_result_is_0x00()
		{
			// Arrage
			this.OperationByte = 0x80;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Zero.Should().BeTrue();
		}
	}
}
