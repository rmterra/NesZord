using FluentAssertions;
using NesZord.Core;
using Xunit;

namespace NesZord.Tests.New.OpCodes.SED
{
	public class When_process_SED_should : When_process_opcode_should
	{
		[Fact]
		public void Set_decimal_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Processor.Decimal.Should().BeTrue();
		}

		protected override void RunProgram()
		{
			this.Processor.RunProgram(new byte[]
			{
				(byte) OpCode.SED_Implied
			});
		}
	}
}
