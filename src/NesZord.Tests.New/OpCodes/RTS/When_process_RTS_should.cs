using FluentAssertions;
using NesZord.Core;
using Xunit;

namespace NesZord.Tests.New.OpCodes.RTS
{
	public class When_process_RTS_should : When_process_opcode_should
	{
		[Fact]
		public void Stack_pointer_be_0x0000_when_program_finishes()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Processor.StackPointer.CurrentOffset.Should().Equals(0xff);
		}

		[Fact]
		public void Finishes_program_counter_at_0x0604_address()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Processor.ProgramCounter.Should().Equals(0x0604);
		}

		protected override void RunProgram()
		{
			this.Processor.RunProgram(new byte[] 
			{
				(byte)OpCode.JSR_Absolute, 0x04, 0x06,
				(byte)OpCode.BRK_Implied,
				(byte)OpCode.RTS_Implied,
			});
		}
	}
}
