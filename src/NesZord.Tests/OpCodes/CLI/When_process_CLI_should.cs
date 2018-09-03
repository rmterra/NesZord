using FluentAssertions;
using NesZord.Core;
using Xunit;

namespace NesZord.Tests.OpCodes.CLI
{
	public class When_process_CLI_should : When_process_opcode_should
	{
		[Fact]
		public void Not_set_interrupt_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Interrupt.Should().BeFalse();
		}

		protected override void RunProgram()
		{
			this.Cpu.RunProgram(new byte[]
			{
				(byte) OpCode.SEI_Implied,
				(byte) OpCode.CLI_Implied
			});
		}
	}
}
