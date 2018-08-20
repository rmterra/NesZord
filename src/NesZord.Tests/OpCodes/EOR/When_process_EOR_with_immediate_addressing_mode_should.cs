using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.EOR
{
	public class When_process_EOR_with_immediate_addressing_mode_should
		: When_process_EOR_should<ImmediateAddressingMode>
	{
		public When_process_EOR_with_immediate_addressing_mode_should()
			: base(new ImmediateAddressingMode(OpCode.EOR_Immediate))
		{
		}
	}
}
