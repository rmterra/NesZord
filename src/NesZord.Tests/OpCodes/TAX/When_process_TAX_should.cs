using AutoFixture;
using FluentAssertions;
using NesZord.Core;
using Xunit;

namespace NesZord.Tests.OpCodes.TAX
{
	public class When_process_TAX_should : When_process_opcode_should
	{
		private Fixture fixture;

		public When_process_TAX_should()
		{
			this.fixture = new Fixture();
		}

		[Fact]
		public void Transfer_accumulator_value_to_x()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Cpu.X.Value.Should().Equals(this.Cpu.Accumulator.Value);
		}

		protected override void RunProgram()
		{
			this.Cpu.RunProgram(new byte[]
			{
				(byte)OpCode.LDA_Immediate, fixture.Create<byte>(),
				(byte)OpCode.TAX_Implied
			});
		}
	}
}
