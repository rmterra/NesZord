using FluentAssertions;
using NesZord.Core;
using Xunit;

namespace NesZord.Tests.New.OpCodes.SEC
{
	public class When_process_SEC_should : When_process_opcode_should
	{
		[Fact]
		public void Set_carry_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Processor.Carry.Should().BeTrue();
		}

		protected override void RunProgram()
		{
			this.Processor.RunProgram(new byte[]
			{
				(byte) OpCode.SEC_Implied
			});
		}
	}
}
