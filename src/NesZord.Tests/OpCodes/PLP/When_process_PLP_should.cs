using FluentAssertions;
using NesZord.Core;
using Xunit;

namespace NesZord.Tests.OpCodes.PLP
{
	public class When_process_PLP_should : When_process_opcode_should
	{
		[Fact]
		public void Set_carry_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Carry.Should().BeTrue();
		}

		[Fact]
		public void Set_decimal_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Decimal.Should().BeTrue();
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
		public void Not_set_interrupt_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Interrupt.Should().BeFalse();
		}

		[Fact]
		public void Not_set_break_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Break.Should().BeFalse();
		}

		[Fact]
		public void Not_set_overflow_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Overflow.Should().BeFalse();
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
		public void Actual_stack_pointer_value_be_0xff()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Cpu.StackPointer.CurrentOffset.Should().Equals(0xff);
		}

		protected override void RunProgram()
		{
			this.Cpu.RunProgram(new byte[]
			{
				(byte) OpCode.SEC_Implied,
				(byte) OpCode.SED_Implied,
				(byte) OpCode.PHP_Implied,
				(byte) OpCode.CLC_Implied,
				(byte) OpCode.CLD_Implied,
				(byte) OpCode.PLP_Implied
			});
		}
	}
}
