using FluentAssertions;
using NesZord.Core;
using Xunit;

namespace NesZord.Tests.OpCodes.SEC
{
	public class When_process_SEC_should : When_process_opcode_should
	{
		[Fact]
		public void Set_carry_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Carry.Should().BeTrue();
		}

		protected override void RunProgram()
		{
			this.Cpu.RunProgram(new byte[]
			{
				(byte) OpCode.SEC_Implied
			});
		}
	}
}
