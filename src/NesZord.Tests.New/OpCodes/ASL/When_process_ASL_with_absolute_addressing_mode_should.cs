using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.ASL
{
	public class When_process_ASL_with_absolute_addressing_mode_should 
		: When_process_ASL_should<AbsoluteAddressingMode>
	{
		public When_process_ASL_with_absolute_addressing_mode_should() 
			: base(new AbsoluteAddressingMode())
		{
		}

		protected override void RunProgram()
			=> this.Processor.RunProgram(new byte[] 
			{
				(byte)OpCode.ASL_Absolute, this.AddressingMode.RandomOffset, this.AddressingMode.RandomPage
			});
	}
}
