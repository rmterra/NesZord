using NesZord.Core;
using NSpec;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_rts_operation : Describe_microprocessor_operation
	{
		public void When_use_implied_addressing_mode()
		{
			var randomPage = default(byte);
			var randomOffset = default(byte);

			before = () =>
			{
				randomPage = 0x06;
				randomOffset = 0x004;
			};

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
