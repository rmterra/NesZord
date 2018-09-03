using FluentAssertions;
using NesZord.Core;
using NesZord.Tests.AddressingMode;
using Xunit;

namespace NesZord.Tests.OpCodes.ROR
{
	public abstract class When_process_ROR_should<T> 
		: When_process_opcode_with_addressing_mode_should<T> where T : IAddressingMode
	{
		public When_process_ROR_should(T addressingMode)
			: base(addressingMode)
		{
			this.OperationByte = 0x05;
			this.Cpu.RunProgram(new byte[] { (byte)OpCode.CLC_Implied });
		}

		[Fact]
		public void Memory_value_be_shifted_to_right()
		{
			// Act
			this.RunProgram();

			// Assert
			this.OperationByte.Should().Equals(0x02);
		}

		[Fact]
		public void Set_carry_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Carry.Should().BeTrue();
		}

		[Fact]
		public void Not_set_zero_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Zero.Should().BeFalse();
		}

		[Fact]
		public void Not_set_negative_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Negative.Should().BeFalse();
		}

		[Fact]
		public void Set_sign_bit_on_shifted_result_value_given_that_carry_flag_is_set()
		{
			// Arrange
			this.Cpu.RunProgram(new byte[] { (byte)OpCode.SEC_Implied });

			// Act
			this.RunProgram();

			// Assert
			this.OperationByte.Should().Equals(0x82);
		}

		[Fact]
		public void Set_negative_flag_given_that_shift_result_hash_sign_bit_set()
		{
			// Arrange
			this.Cpu.RunProgram(new byte[] { (byte)OpCode.SEC_Implied });

			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Negative.Should().BeTrue();
		}

		[Fact]
		public void Not_set_carry_flag_given_that_byte_to_shift_first_bit_is_not_set()
		{
			// Arrange
			this.OperationByte = 0x04;

			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Carry.Should().BeFalse();
		}

		[Fact]
		public void Set_carry_flag_given_that_byte_to_shift_is_0x00()
		{
			// Arrange
			this.OperationByte = 0x01;

			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Carry.Should().BeTrue();
		}

		[Fact]
		public void Set_zero_flag_given_that_byte_to_shift_is_0x00()
		{
			// Arrange
			this.OperationByte = 0x01;

			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Zero.Should().BeTrue();
		}
	}
}
