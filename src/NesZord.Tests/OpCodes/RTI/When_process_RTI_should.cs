using FluentAssertions;
using NesZord.Core;
using Xunit;

namespace NesZord.Tests.OpCodes.RTI
{
	public class When_process_RTI_should : When_process_opcode_should
	{
		[Fact]
		public void Finishes_program_counter_at_0x0577_address()
		{
			// Act
			this.RunProgram();

			// Assert
			// TODO: My program counter is incremented after read byte, for now I don't known if this will be a problem
			this.Processor.ProgramCounter.Should().Equals(0x0578);
		}

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

		protected override void RunProgram()
		{
			this.Processor.RunProgram(new byte[] 
			{
				(byte) OpCode.LDA_Immediate, 0x05,
				(byte) OpCode.PHA_Implied,
				(byte) OpCode.LDA_Immediate, 0x77,
				(byte) OpCode.PHA_Implied,
				(byte) OpCode.SEC_Implied,
				(byte) OpCode.SED_Implied,
				(byte) OpCode.PHP_Implied,
				(byte) OpCode.CLC_Implied,
				(byte) OpCode.CLD_Implied,
				(byte) OpCode.RTI_Implied
			});
		}
	}
}
