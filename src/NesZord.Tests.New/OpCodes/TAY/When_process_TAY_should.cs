using AutoFixture;
using FluentAssertions;
using NesZord.Core;
using Xunit;

namespace NesZord.Tests.New.OpCodes.TAY
{
	public class When_process_TAY_should : When_process_opcode_should
	{
		private Fixture fixture;

		public When_process_TAY_should()
		{
			this.fixture = new Fixture();
		}

		[Fact]
		public void Transfer_accumulator_value_to_y()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Processor.Y.Value.Should().Equals(this.Processor.Accumulator.Value);
		}

		protected override void RunProgram()
		{
			this.Processor.RunProgram(new byte[]
			{
				(byte)OpCode.LDA_Immediate, fixture.Create<byte>(),
				(byte)OpCode.TAY_Implied
			});
		}
	}
}
