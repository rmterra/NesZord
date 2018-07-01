﻿using FluentAssertions;
using NesZord.Tests.New.AddressingMode;
using Xunit;

namespace NesZord.Tests.New.OpCodes.CPY
{
	public abstract class When_process_CPY_should<T>
		: When_process_opcode_with_addressing_mode_should<T> where T : IAddressingMode
	{
		public When_process_CPY_should(T addressingMode)
			: base(addressingMode)
		{
			this.Processor.Y.Value = 0x05;
		}

		[Fact]
		public void Not_set_negative_flag_given_that_y_register_value_is_lower_than_compared_byte()
		{
			// Arrange
			this.OperationByte = 0xff;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Negative.Should().BeFalse();
		}

		[Fact]
		public void Not_set_carry_flag_given_that_y_register_value_is_lower_than_compared_byte()
		{
			// Arrange
			this.OperationByte = 0xff;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Carry.Should().BeFalse();
		}

		[Fact]
		public void Not_set_zero_flag_given_that_y_register_value_is_lower_than_compared_byte()
		{
			// Arrange
			this.OperationByte = 0xff;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Zero.Should().BeFalse();
		}

		[Fact]
		public void Not_set_negative_flag_given_that_y_register_value_is_equal_than_compared_byte()
		{
			// Arrange
			this.OperationByte = 0x05;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Negative.Should().BeFalse();
		}

		[Fact]
		public void Set_carry_flag_given_that_y_register_value_is_equal_than_compared_byte()
		{
			// Arrange
			this.OperationByte = 0x05;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Carry.Should().BeTrue();
		}

		[Fact]
		public void Set_zero_flag_given_that_y_register_value_is_equal_than_compared_byte()
		{
			// Arrange
			this.OperationByte = 0x05;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Zero.Should().BeTrue();
		}

		[Fact]
		public void Not_set_negative_flag_given_that_y_register_value_is_greater_than_compared_byte()
		{
			// Arrange
			this.OperationByte = 0x00;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Negative.Should().BeFalse();
		}

		[Fact]
		public void Set_carry_flag_given_that_y_register_value_is_greater_than_compared_byte()
		{
			// Arrange
			this.OperationByte = 0x00;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Carry.Should().BeTrue();
		}

		[Fact]
		public void Not_set_zero_flag_given_that_y_register_value_is_greater_than_compared_byte()
		{
			// Arrange
			this.OperationByte = 0x00;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Zero.Should().BeFalse();
		}

		[Fact]
		public void Set_negative_flag_given_that_compared_result_is_greater_than_0x80()
		{
			// Arrange
			this.Processor.Y.Value = 0xff;
			this.OperationByte = 0x00;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Negative.Should().BeTrue();
		}

		[Fact]
		public void Set_carry_flag_given_that_compared_result_is_greater_than_0x80()
		{
			// Arrange
			this.Processor.Y.Value = 0xff;
			this.OperationByte = 0x00;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Carry.Should().BeTrue();
		}

		[Fact]
		public void Not_set_zero_flag_given_that_compared_result_is_greater_than_0x80()
		{
			// Arrange
			this.Processor.Y.Value = 0xff;
			this.OperationByte = 0x00;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Zero.Should().BeFalse();
		}
	}
}
