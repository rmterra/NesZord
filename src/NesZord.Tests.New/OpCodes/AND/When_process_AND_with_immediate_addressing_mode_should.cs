using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.AND
{
	public class When_process_AND_with_immediate_addressing_mode_should 
		: When_process_AND_should<ImmediateAddressingMode>
	{
		public When_process_AND_with_immediate_addressing_mode_should() 
			: base(new ImmediateAddressingMode())
		{
		}

		protected override void RunProgram()
			=> this.Processor.RunProgram(new byte[]
			{
				(byte) OpCode.LDA_Immediate, this.AccumulatorValue,
				(byte) OpCode.AND_Immediate, this.AddressingMode.OperationByte
			});
	}
}
