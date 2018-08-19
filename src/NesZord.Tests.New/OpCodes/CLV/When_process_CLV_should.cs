using FluentAssertions;
using NesZord.Core;
using Xunit;

namespace NesZord.Tests.New.OpCodes.CLV
{
	public class When_process_CLV_should : When_process_opcode_should
	{
		[Fact]
		public void Not_set_overflow_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Processor.Overflow.Should().BeFalse();
		}

		protected override void RunProgram()
		{
			this.Processor.RunProgram(new byte[]
			{
				(byte) OpCode.CLV_Implied
			});
		}
	}
}
