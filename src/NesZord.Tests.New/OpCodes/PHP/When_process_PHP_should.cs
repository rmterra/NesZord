using AutoFixture;
using FluentAssertions;
using NesZord.Core;
using Xunit;

namespace NesZord.Tests.New.OpCodes.PHP
{
	public class When_process_PHP_should : When_process_opcode_should
	{
		[Fact]
		public void Value_at_first_stack_position_be_equal_to_0x09()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Memory.Read(Core.Memory.INITIAL_STACK_OFFSET, Core.Memory.STACK_PAGE).Should().Equals(0x09);
		}

		[Fact]
		public void Actual_stack_pointer_value_be_0xfe()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Processor.StackPointer.CurrentOffset.Should().Equals(0xfe);
		}

		protected override void RunProgram()
		{
			this.Processor.RunProgram(new byte[]
			{
				(byte) OpCode.SEC_Implied,
				(byte) OpCode.SED_Implied,
				(byte) OpCode.PHP_Implied
			});
		}
	}
}
