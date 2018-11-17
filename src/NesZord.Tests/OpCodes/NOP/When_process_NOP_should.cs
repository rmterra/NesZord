using FluentAssertions;
using NesZord.Core;
using NesZord.Core.Memory;
using Xunit;

namespace NesZord.Tests.OpCodes.NOP
{
	public class When_process_NOP_should : When_process_opcode_should
	{
		[Fact]
		public void Increment_1_to_program_counter()
		{
			// Act
			this.RunProgram();

			// Assert
			var incrementedValueAtProgramCounter = this.Cpu.ProgramCounter - Core.Emulator.PROGRAM_ROM_START - 1;
			incrementedValueAtProgramCounter.Should().Equals(1);
		}

		protected override void RunProgram()
		{
			this.Cpu.RunProgram(new byte[] { (byte) OpCode.NOP_Implied });
		}
	}
}
