using AutoFixture;
using FluentAssertions;
using NesZord.Core;
using NesZord.Core.Memory;
using Xunit;

namespace NesZord.Tests.OpCodes.PHA
{
	public class When_process_PHA_should : When_process_opcode_should
	{
		private Fixture fixture;

		public When_process_PHA_should()
		{
			this.fixture = new Fixture();
		}

		[Fact]
		public void Value_at_first_stack_position_be_equal_to_accumulator()
		{
			// Act
			this.RunProgram();

			// Assert
			var stackValue = this.Emulator.Read(Core.Emulator.INITIAL_STACK_OFFSET, Core.Emulator.STACK_PAGE);
			stackValue.Should().Equals(this.Cpu.Accumulator.Value);
		}

		[Fact]
		public void Actual_stack_pointer_value_be_0xfe()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Cpu.StackPointer.CurrentOffset.Should().Equals(0xfe);
		}

		protected override void RunProgram()
		{
			this.Cpu.RunProgram(new byte[]
			{
				(byte) OpCode.LDA_Immediate, this.fixture.Create<byte>(),
				(byte) OpCode.PHA_Implied
			});
		}
	}
}
