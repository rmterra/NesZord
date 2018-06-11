using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.BIT
{
	public class When_process_BIT_with_zero_page_addressing_mode_should
		: When_process_BIT_should<ZeroPageAddressingMode>
	{
		public When_process_BIT_with_zero_page_addressing_mode_should() 
			: base(new ZeroPageAddressingMode())
		{
		}

		protected override void RunProgram()
		{
			this.Processor.RunProgram(new byte[]
				{
					(byte) OpCode.LDA_Immediate, this.Processor.Accumulator.Value,
					(byte) OpCode.BIT_ZeroPage, this.AddressingMode.RandomOffset
				});
		}
	}
}
