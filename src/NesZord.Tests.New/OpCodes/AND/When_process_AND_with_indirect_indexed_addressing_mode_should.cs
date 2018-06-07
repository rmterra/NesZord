using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.AND
{
	public class When_process_AND_with_indirect_indexed_addressing_mode_should 
		: When_process_AND_should<IndirectIndexedAddressingMode>
	{
		public When_process_AND_with_indirect_indexed_addressing_mode_should() 
			: base(new IndirectIndexedAddressingMode())
		{
		}

		protected override void RunProgram()
			=> this.Processor.RunProgram(new byte[]
			{
				(byte) OpCode.LDA_Immediate, this.AccumulatorValue,
				(byte) OpCode.LDY_Immediate, this.AddressingMode.YRegisterValue,
				(byte) OpCode.AND_IndirectIndexed, this.AddressingMode.RandomOffset
			});
	}
}
