using FluentAssertions;
using NesZord.Core;
using Xunit;

namespace NesZord.Tests.New.OpCodes.NOP
{
	public class When_process_NOP_should : When_process_opcode_should
	{
		[Fact]
		public void Increment_1_to_program_counter()
		{
			// Act
			this.RunProgram();

			// Assert
			var incrementedValueAtProgramCounter = this.Processor.ProgramCounter - Core.Memory.PROGRAM_ROM_START - 1;
			incrementedValueAtProgramCounter.Should().Equals(1);
		}

		protected override void RunProgram()
		{
			this.Processor.RunProgram(new byte[] { (byte) OpCode.NOP_Implied });
		}
	}
}
