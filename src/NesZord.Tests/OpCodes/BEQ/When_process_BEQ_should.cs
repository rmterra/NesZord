using FluentAssertions;
using NesZord.Core;
using Xunit;

namespace NesZord.Tests.OpCodes.BEQ
{
	public class When_process_BEQ_should : When_process_opcode_should
	{
		[Fact]
		public void Y_register_value_be_equal_0x07()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Y.Value.Should().Equals(0x07);
		}

		[Fact]
		public void Set_carry_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Carry.Should().BeTrue();
		}

		[Fact]
		public void Set_zero_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Cpu.Zero.Should().BeTrue();
		}

		protected override void RunProgram()
		{
			this.Cpu.RunProgram(new byte[]
			{
				(byte) OpCode.LDY_Immediate, 0x08,
				(byte) OpCode.DEY_Implied,
				(byte) OpCode.CPY_Immediate, 0x07,
				(byte) OpCode.BEQ_Relative, 0x03,
				(byte) OpCode.LDY_Immediate, 0xff,
				(byte) OpCode.BRK_Implied
			});
		}
	}
}
