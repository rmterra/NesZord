using FluentAssertions;
using NesZord.Core;
using Xunit;

namespace NesZord.Tests.OpCodes.SED
{
	public class When_process_SED_should : When_process_opcode_should
	{
		[Fact]
		public void Set_decimal_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Decimal.Should().BeTrue();
		}

		protected override void RunProgram()
		{
			this.Cpu.RunProgram(new byte[]
			{
				(byte) OpCode.SED_Implied
			});
		}
	}
}
