﻿using FluentAssertions;
using NesZord.Tests.AddressingMode;
using Xunit;

namespace NesZord.Tests.OpCodes.LDA
{
	public abstract class When_process_LDA_should<T> 
		: When_process_opcode_with_addressing_mode_should<T> where T : IAddressingMode
	{
		public When_process_LDA_should(T addressingMode)
			: base(addressingMode)
		{
			this.OperationByte = 0x05;
		}

		[Fact]
		public void Set_accumulator_with_received_value()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Accumulator.Value.Should().Be(0x05);
		}

		[Fact]
		public void Set_zero_flag_given_that_new_accumulator_value_is_0x00()
		{
			// Arrange
			this.OperationByte = 0x00;

			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Zero.Should().BeTrue();
		}

		[Fact]
		public void Not_set_negative_flag_given_that_new_accumulator_value_is_0x00()
		{
			// Arrange
			this.OperationByte = 0x00;

			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Negative.Should().BeFalse();
		}

		[Fact]
		public void Not_set_zero_flag_given_that_new_accumulator_value_has_sign_bit_set()
		{
			// Arrange
			this.OperationByte = 0x80;

			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Zero.Should().BeFalse();
		}

		[Fact]
		public void Set_negative_flag_given_that_new_accumulator_has_sign_bit_set()
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
