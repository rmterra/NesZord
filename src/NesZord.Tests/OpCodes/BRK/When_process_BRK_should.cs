using AutoFixture;
using FluentAssertions;
using NesZord.Core;
using Xunit;

namespace NesZord.Tests.OpCodes.BRK
{
	public class When_process_BRK_should : When_process_opcode_should
	{
		private Fixture fixture;

		public When_process_BRK_should()
		{
			this.fixture = new Fixture();
		}

		[Fact]
		public void Ignore_all_commands_after_execute_BRK()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Processor.Carry.Should().BeFalse();
		}

		protected override void RunProgram()
		{
			this.Processor.RunProgram(new byte[]
			{
				(byte)OpCode.LDA_Immediate, fixture.Create<byte>(),
				(byte)OpCode.TAX_Implied,
				(byte)OpCode.BRK_Implied,
				(byte)OpCode.ADC_Immediate, 0xff
			});
		}
	}
}
