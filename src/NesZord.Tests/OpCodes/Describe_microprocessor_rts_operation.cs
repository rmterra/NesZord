using NesZord.Core;
using NSpec;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_rts_operation : Describe_microprocessor_operation
	{
		public void When_use_implied_addressing_mode()
		{
			byte randomPage = 0x06;
			byte randomOffset = 0x04;

			this.RunProgram(() => new byte[]
			{
				(byte)OpCode.JSR_Absolute, randomOffset, randomPage,
				(byte)OpCode.BRK_Implied,
				(byte)OpCode.RTS_Implied,
			});

			it["should stack pointer be 0x0000 when program finishes"] = () =>
			{
				this.Processor.StackPointer.CurrentOffset.should_be(0xff);
			};

			it["should finishes program counter at 0x0604 address"] = () => this.Processor.ProgramCounter.should_be(0x0604);
		}
	}
}