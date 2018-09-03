using FluentAssertions;
using NesZord.Core;
using Xunit;

namespace NesZord.Tests.OpCodes.SEI
{
	public class When_process_SEI_should : When_process_opcode_should
	{
		[Fact]
		public void Set_interrupt_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Interrupt.Should().BeTrue();
		}

		protected override void RunProgram()
		{
			this.Cpu.RunProgram(new byte[]
			{
				(byte) OpCode.SEI_Implied
			});
		}
	}
}
