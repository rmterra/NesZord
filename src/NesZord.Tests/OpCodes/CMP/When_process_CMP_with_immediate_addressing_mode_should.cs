using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.CMP
{
	public class When_process_CMP_with_immediate_addressing_mode_should 
		: When_process_CMP_should<ImmediateAddressingMode>
	{
		public When_process_CMP_with_immediate_addressing_mode_should() 
			: base(new ImmediateAddressingMode(OpCode.CMP_Immediate))
		{
		}
	}
}
