using AutoFixture;
using FluentAssertions;
using NesZord.Core;
using Xunit;

namespace NesZord.Tests.OpCodes.TXA
{
	public class When_process_TXA_should : When_process_opcode_should
	{
		private Fixture fixture;

		public When_process_TXA_should()
		{
			this.fixture = new Fixture();
		}

		[Fact]
		public void Transfer_x_value_to_accumulator()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Accumulator.Value.Should().Equals(this.Cpu.X.Value);
		}

		protected override void RunProgram()
		{
			this.Cpu.RunProgram(new byte[]
			{
				(byte)OpCode.LDX_Immediate, fixture.Create<byte>(),
				(byte)OpCode.TXA_Implied
			});
		}
	}
}
