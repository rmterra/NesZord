using FluentAssertions;
using NesZord.Core;
using Xunit;

namespace NesZord.Tests.OpCodes.CLD
{
	public class When_process_CLD_should : When_process_opcode_should
	{
		[Fact]
		public void Not_set_decimal_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Processor.Decimal.Should().BeFalse();
		}

		protected override void RunProgram()
		{
			this.Processor.RunProgram(new byte[]
			{
				(byte) OpCode.SED_Implied,
				(byte) OpCode.CLD_Implied
			});
		}
	}
}
