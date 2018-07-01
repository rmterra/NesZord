using FluentAssertions;
using NesZord.Core;
using Xunit;

namespace NesZord.Tests.New.OpCodes.JSR
{
	public class When_process_JSR_should : When_process_opcode_should
	{
		[Fact]
		public void Stack_pop_page_address()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Memory.Read(0xff, Core.Memory.STACK_PAGE).Should().Equals(0x06);
		}

		[Fact]
		public void Stack_pop_offset_address()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Memory.Read(0xfe, Core.Memory.STACK_PAGE).Should().Equals(0x03);
		}

		[Fact]
		public void Move_program_counter_to_0x0621_address()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Processor.ProgramCounter.Should().Equals(0x0621);
		}

		protected override void RunProgram()
		{
			this.Processor.RunProgram(new byte[]
			{
				(byte)OpCode.JSR_Absolute, 0x020, 0x06,
				(byte)OpCode.BRK_Implied
			});
		}
	}
}