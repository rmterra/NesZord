using NesZord.Core;
using NSpec;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_nop_operation : nspec
	{
		public void When_use_implied_addressing_mode()
		{
			var processor = default(Microprocessor);

			before = () => 
			{
				processor = new Microprocessor(new MemoryMock());
			};

			act = () => 
			{
				processor.RunProgram(new byte[] { (byte) OpCode.NOP_Implied });
			};

			it["should increment 1 to program counter"] = () => 
			{
				var incrementedValueAtProgramCounter = processor.ProgramCounter - Memory.PROGRAM_ROM_START - 1;
				incrementedValueAtProgramCounter.should_be(1);
            };
		}
	}
}
