using FluentAssertions;
using NesZord.Core;
using Xunit;

namespace NesZord.Tests.OpCodes.CLC
{
	public class When_process_CLC_should : When_process_opcode_should
	{
		[Fact]
		public void Not_set_carry_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Carry.Should().BeFalse();
		}

		protected override void RunProgram()
		{
			this.Cpu.RunProgram(new byte[]
			{
				(byte) OpCode.SEC_Implied,
				(byte) OpCode.CLC_Implied
			});
		}
	}
}
