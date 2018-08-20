using FluentAssertions;
using NesZord.Tests.AddressingMode;
using Xunit;

namespace NesZord.Tests.OpCodes.JMP
{
	public abstract class When_process_JMP_should<T> 
		: When_process_opcode_with_addressing_mode_should<T> where T : IAddressingMode
	{
		public When_process_JMP_should(T addressingMode)
			: base(addressingMode)
		{
			this.Processor.Accumulator.Value = 0x05;
			this.OperationByte = 0x05;
		}

		[Fact]
		public void Set_program_counter_as_memory_location_value()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Processor.ProgramCounter.Should().Equals(0x05);
		}
	}
}
