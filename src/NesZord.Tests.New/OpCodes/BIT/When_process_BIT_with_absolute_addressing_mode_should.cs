using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.BIT
{
	public class When_process_BIT_with_absolute_addressing_mode_should
		: When_process_BIT_should<AbsoluteAddressingMode>
	{
		public When_process_BIT_with_absolute_addressing_mode_should() 
			: base(new AbsoluteAddressingMode())
		{
		}

		protected override void RunProgram()
		{
			this.Processor.RunProgram(new byte[]
				{
					(byte) OpCode.LDA_Immediate, this.Processor.Accumulator.Value,
					(byte) OpCode.BIT_Absolute, this.AddressingMode.RandomOffset, this.AddressingMode.RandomPage
				});
		}
	}
}
