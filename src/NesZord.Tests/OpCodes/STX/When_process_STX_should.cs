using FluentAssertions;
using NesZord.Tests.AddressingMode;
using Xunit;

namespace NesZord.Tests.OpCodes.STX
{
	public abstract class When_process_STX_should<T> 
		: When_process_opcode_with_addressing_mode_should<T> where T : IAddressingMode
	{
		public When_process_STX_should(T addressingMode)
			: base(addressingMode)
		{
		}

		[Fact]
		public void Store_the_x_register_value_at_memory()
		{
			// Arrange
			this.Cpu.X.Value = 0x05;

			// Act
			this.RunProgram();

			// Assert
			this.OperationByte.Should().Equals(0x05);
		}
	}
}
