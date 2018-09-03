using AutoFixture;
using FluentAssertions;
using NesZord.Core;
using Xunit;

namespace NesZord.Tests.OpCodes.TYA
{
	public class When_process_TYA_should : When_process_opcode_should
	{
		private Fixture fixture;

		public When_process_TYA_should()
		{
			this.fixture = new Fixture();
		}

		[Fact]
		public void Transfer_y_value_to_accumulator()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Accumulator.Value.Should().Equals(this.Cpu.Y.Value);
		}

		protected override void RunProgram()
		{
			this.Cpu.RunProgram(new byte[]
			{
				(byte)OpCode.LDY_Immediate, fixture.Create<byte>(),
				(byte)OpCode.TYA_Implied
			});
		}
	}
}
