using FluentAssertions;
using NesZord.Tests.AddressingMode;
using Xunit;

namespace NesZord.Tests.OpCodes.STY
{
	public abstract class When_process_STY_should<T> 
		: When_process_opcode_with_addressing_mode_should<T> where T : IAddressingMode
	{
		public When_process_STY_should(T addressingMode)
			: base(addressingMode)
		{
		}

		[Fact]
		public void Store_the_y_register_value_at_memory()
		{
			// Arrange
			this.Processor.Y.Value = 0x05;

			// Act
			this.RunProgram();

			// Assert
			this.OperationByte.Should().Equals(0x05);
		}
	}
}
