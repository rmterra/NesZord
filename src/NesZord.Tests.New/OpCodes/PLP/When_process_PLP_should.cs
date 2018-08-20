using FluentAssertions;
using NesZord.Core;
using Xunit;

namespace NesZord.Tests.New.OpCodes.PLP
{
	public class When_process_PLP_should : When_process_opcode_should
	{
		[Fact]
		public void Set_carry_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Processor.Carry.Should().BeTrue();
		}

		[Fact]
		public void Set_decimal_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Processor.Decimal.Should().BeTrue();
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
		public void Not_set_interrupt_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Processor.Interrupt.Should().BeFalse();
		}

		[Fact]
		public void Not_set_break_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Processor.Break.Should().BeFalse();
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
		public void Actual_stack_pointer_value_be_0xff()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Processor.StackPointer.CurrentOffset.Should().Equals(0xff);
		}

		protected override void RunProgram()
		{
			this.Processor.RunProgram(new byte[]
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
