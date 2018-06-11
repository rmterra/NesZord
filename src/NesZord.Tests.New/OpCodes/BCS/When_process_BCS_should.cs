using FluentAssertions;
using NesZord.Core;
using Xunit;

namespace NesZord.Tests.New.OpCodes.BCS
{
	public class When_process_BCS_should : When_process_opcode_should
	{
		[Fact]
		public void X_register_value_be_equal_0x0a()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Processor.X.Value.Should().Equals(0x0a);
		}

		[Fact]
		public void Set_carry_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Processor.Carry.Should().BeTrue();
		}

		[Fact]
		public void Not_set_zero_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Processor.Zero.Should().BeFalse();
		}

		protected override void RunProgram()
		{
			this.Processor.RunProgram(new byte[]
			{
				(byte) OpCode.LDX_Immediate, 0x0a,
				(byte) OpCode.CPX_Immediate, 0x05,
				(byte) OpCode.BCS_Relative, 0x03,
				(byte) OpCode.LDX_Immediate, 0xff,
				(byte) OpCode.BRK_Implied
			});
		}
	}
}
