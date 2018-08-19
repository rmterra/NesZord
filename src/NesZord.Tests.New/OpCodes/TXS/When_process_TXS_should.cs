using AutoFixture;
using FluentAssertions;
using NesZord.Core;
using Xunit;

namespace NesZord.Tests.New.OpCodes.TXS
{
	public class When_process_TXS_should : When_process_opcode_should
	{
		private Fixture fixture;

		public When_process_TXS_should()
		{
			this.fixture = new Fixture();
		}

		[Fact]
		public void Transfer_x_value_to_stack_pointer()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Processor.StackPointer.CurrentOffset.Should().Equals(this.Processor.X.Value);
		}

		protected override void RunProgram()
		{
			this.Processor.RunProgram(new byte[]
			{
				(byte)OpCode.LDX_Immediate, fixture.Create<byte>(),
				(byte)OpCode.TXS_Implied
			});
		}
	}
}
