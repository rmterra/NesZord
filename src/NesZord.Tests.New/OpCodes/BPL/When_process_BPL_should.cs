using FluentAssertions;
using NesZord.Core;
using Xunit;

namespace NesZord.Tests.New.OpCodes.BPL
{
	public class When_process_BPL_should : When_process_opcode_should
	{
		[Fact]
		public void X_register_value_be_equal_0x7f()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Processor.X.Value.Should().Equals(0x7f);
		}

		[Fact]
		public void Not_set_negative_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Processor.Negative.Should().BeFalse();
		}

		protected override void RunProgram()
		{
			this.Processor.RunProgram(new byte[]
			{
				(byte) OpCode.LDX_Immediate, 0x80,
				(byte) OpCode.DEX_Implied,
				(byte) OpCode.BPL_Relative, 0x03,
				(byte) OpCode.LDX_Immediate, 0xff,
				(byte) OpCode.BRK_Implied
			});
		}
	}
}
