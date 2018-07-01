using FluentAssertions;
using NesZord.Tests.New.AddressingMode;
using Xunit;

namespace NesZord.Tests.New.OpCodes.STA
{
	public abstract class When_process_STA_should<T> 
		: When_process_opcode_with_addressing_mode_should<T> where T : IAddressingMode
	{
		public When_process_STA_should(T addressingMode)
			: base(addressingMode)
		{
		}

		[Fact]
		public void Store_the_accumulator_value_at_memory()
		{
			// Arrange
			this.Processor.Accumulator.Value = 0x05;

			// Act
			this.RunProgram();

			// Assert
			this.OperationByte.Should().Equals(0x05);
		}
	}
}
