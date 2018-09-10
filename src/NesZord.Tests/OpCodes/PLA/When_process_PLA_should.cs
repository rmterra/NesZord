using FluentAssertions;
using NesZord.Core;
using NesZord.Core.Memory;
using Xunit;

namespace NesZord.Tests.OpCodes.PLA
{
	public class When_process_PLA_should : When_process_opcode_should
	{
		public When_process_PLA_should()
		{
			this.Cpu.Accumulator.Value = 0x05;
		}

		[Fact]
		public void Accumulator_value_be_0x05()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Accumulator.Value.Should().Equals(0x05);
		}

		[Fact]
		public void Keep_negative_flag_initial_value()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Negative.Should().BeFalse();
		}

		[Fact]
		public void Keep_zero_flag_initial_value()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Zero.Should().BeFalse();
		}

		[Fact]
		public void Value_at_first_stack_position_be_equal_to_accumulator()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Memory.Read(MemoryMapper.INITIAL_STACK_OFFSET, MemoryMapper.STACK_PAGE)
				.Should().Equals(this.Cpu.Accumulator.Value);
		}

		[Fact]
		public void Actual_stack_pointer_be_0xff()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Cpu.StackPointer.CurrentOffset.Should().Equals(0xff);
		}

		[Fact]
		public void Set_zero_flag_given_that_pulled_value_is_0x00()
		{
			// Arrange
			this.Cpu.Accumulator.Value = 0x00;

			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Zero.Should().BeTrue();
		}

		[Fact]
		public void Set_negative_flag_given_that_pulled_value_has_sign_bit_set()
		{
			// Arrange
			this.Cpu.Accumulator.Value = 0x80;

			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Negative.Should().BeTrue();
		}

		protected override void RunProgram()
		{
			this.Cpu.RunProgram(new byte[]
			{
				(byte) OpCode.PHA_Implied,
				(byte) OpCode.LDA_Immediate, 0xff,
				(byte) OpCode.PLA_Implied,
			});
		}
	}
}
