using NesZord.Core;
using NSpec;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_jsr_operation : Describe_microprocessor_operation
	{
		public void When_use_absolute_addressing_mode()
		{
			var randomPage = default(byte);
			var randomOffset = default(byte);

			before = () =>
			{
				randomPage = 0x06;
				randomOffset = 0x020;
			};

			this.RunProgram(() => new byte[] 
			{
				(byte)OpCode.JSR_Absolute, randomOffset, randomPage,
				(byte)OpCode.BRK_Implied
			});

			it["should stack return page address"] = () =>  this.Memory.Read(0xff, Core.Memory.STACK_PAGE).should_be(0x06);
			it["should stack return offset address"] = () => this.Memory.Read(0xfe, Core.Memory.STACK_PAGE).should_be(0x03);

			it["should move program counter to 0x6020 address"] = () => this.Processor.ProgramCounter.should_be(0x0621);
		}
	}
}
