using AutoFixture;
using FluentAssertions;
using NesZord.Core;
using NesZord.Core.Memory;
using Xunit;

namespace NesZord.Tests.OpCodes.PHP
{
	public class When_process_PHP_should : When_process_opcode_should
	{
		[Fact]
		public void Value_at_first_stack_position_be_equal_to_0x09()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Memory.Read(MemoryMapper.INITIAL_STACK_OFFSET, MemoryMapper.STACK_PAGE).Should().Equals(0x09);
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
				(byte) OpCode.SEC_Implied,
				(byte) OpCode.SED_Implied,
				(byte) OpCode.PHP_Implied
			});
		}
	}
}
