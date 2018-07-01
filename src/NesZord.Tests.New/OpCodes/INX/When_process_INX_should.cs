using FluentAssertions;
using NesZord.Core;
using Xunit;

namespace NesZord.Tests.New.OpCodes.INX
{
	public class When_process_INX_should : When_process_opcode_should
	{
		public When_process_INX_should()
		{
			this.Processor.X.Value = 0x05;
		}

		[Fact]
		public void Increment_1_to_x_register()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Processor.X.Value.Should().Equals(0x06);
		}

		[Fact]
		public void Not_set_zero_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Processor.Zero.Should().BeFalse();
		}

		[Fact]
		public void Not_set_negative_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Processor.Negative.Should().BeFalse();
		}

		[Fact]
		public void Set_zero_flag_given_that_x_register_is_0xff()
		{
			// Arrange
			this.Processor.X.Value = 0xff;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Zero.Should().BeTrue();
		}

		[Fact]
		public void Not_set_negative_flag_given_that_x_register_is_0xff()
		{
			// Arrange
			this.Processor.X.Value = 0xff;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Negative.Should().BeFalse();
		}

		[Fact]
		public void Not_set_zero_flag_given_that_sign_bit_of_x_register_is_set()
		{
			// Arrange
			this.Processor.X.Value = 0x7f;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Zero.Should().BeFalse();
		}

		[Fact]
		public void Set_negative_flag_given_that_sign_bit_of_x_register_is_set()
		{
			// Arrange
			this.Processor.X.Value = 0x7f;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Negative.Should().BeTrue();
		}

		protected override void RunProgram()
		{
			this.Processor.RunProgram(new byte[] { (byte) OpCode.INX_Implied });
		}
	}
}
