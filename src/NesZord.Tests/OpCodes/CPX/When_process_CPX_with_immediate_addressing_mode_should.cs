using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.CPX
{
	public class When_process_CPX_with_immediate_addressing_mode_should 
		: When_process_CPX_should<ImmediateAddressingMode>
	{
		public When_process_CPX_with_immediate_addressing_mode_should() 
			: base(new ImmediateAddressingMode(OpCode.CPX_Immediate))
		{
		}
	}
}
