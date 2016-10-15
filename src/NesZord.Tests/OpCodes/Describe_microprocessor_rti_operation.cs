using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_rti_operation : Describe_microprocessor_operation
	{
		public void When_use_implied_addressing_mode()
		{
			byte randomPage = 0x05;
			byte randomOffset = 0x77;

			this.RunProgram(() => new byte[]
			{
				(byte) OpCode.LDA_Immediate, randomPage,
				(byte) OpCode.PHA_Implied,
				(byte) OpCode.LDA_Immediate, randomOffset,
				(byte) OpCode.PHA_Implied,
				(byte) OpCode.SEC_Implied,
				(byte) OpCode.SED_Implied,
				(byte) OpCode.PHP_Implied,
				(byte) OpCode.CLC_Implied,
				(byte) OpCode.CLD_Implied,
				(byte) OpCode.RTI_Implied
			});

			this.CarryFlagShouldBeTrue();
			this.DecimalFlagShouldBeTrue();

			this.ZeroFlagShouldBeFalse();
			this.InterruptFlagShouldBeFalse();
			this.BreakFlagShouldBeFalse();
			this.OverflowFlagShouldBeFalse();
			this.NegativeFlagShouldBeFalse();

			it[$"should actual program counter be ${randomPage:x2}{randomOffset:x2}"] = () =>
			{
				var expectedProgramCounter = new MemoryLocation(randomOffset, randomPage).FullLocation;
				this.Processor.ProgramCounter.should_be(expectedProgramCounter + 1);
			};
		}
	}
}