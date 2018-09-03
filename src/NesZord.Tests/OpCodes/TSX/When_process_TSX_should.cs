using AutoFixture;
using FluentAssertions;
using NesZord.Core;
using Xunit;

namespace NesZord.Tests.OpCodes.TSX
{
	public class When_process_TSX_should : When_process_opcode_should
	{
		private Fixture fixture;

		public When_process_TSX_should()
		{
			this.fixture = new Fixture();
		}

		[Fact]
		public void Transfer_stack_pointer_to_x()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Cpu.X.Value.Should().Equals(this.Cpu.StackPointer.CurrentOffset);
		}

		[Fact]
		public void Set_negative_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Negative.Should().BeTrue();
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
		public void Set_zero_flag_given_that_stack_pointer_value_is_0x00()
		{
			// Act
			this.Cpu.RunProgram(new byte[]
			{
				(byte)OpCode.LDX_Immediate, 0x00,
				(byte)OpCode.TXS_Implied,
				(byte)OpCode.TSX_Implied
			});

			// Assert
			this.Cpu.Zero.Should().BeTrue();
		}

		protected override void RunProgram()
		{
			this.Cpu.RunProgram(new byte[]
			{
				(byte)OpCode.TSX_Implied
			});
		}
	}
}
