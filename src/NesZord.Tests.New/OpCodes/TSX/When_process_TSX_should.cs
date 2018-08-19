using AutoFixture;
using FluentAssertions;
using NesZord.Core;
using Xunit;

namespace NesZord.Tests.New.OpCodes.TSX
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
			this.Processor.X.Value.Should().Equals(this.Processor.StackPointer.CurrentOffset);
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
		public void Not_set_zero_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Processor.Zero.Should().BeFalse();
		}

		[Fact]
		public void Set_zero_flag_given_that_stack_pointer_value_is_0x00()
		{
			// Act
			this.Processor.RunProgram(new byte[]
			{
				(byte)OpCode.LDX_Immediate, 0x00,
				(byte)OpCode.TXS_Implied,
				(byte)OpCode.TSX_Implied
			});

			// Assert
			this.Processor.Zero.Should().BeTrue();
		}

		protected override void RunProgram()
		{
			this.Processor.RunProgram(new byte[]
			{
				(byte)OpCode.TSX_Implied
			});
		}
	}
}
