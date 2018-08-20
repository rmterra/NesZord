using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.SBC
{
	public class When_process_SBC_with_immediate_addressing_mode_should
		: When_process_SBC_should<ImmediateAddressingMode>
	{
		public When_process_SBC_with_immediate_addressing_mode_should()
			: base(new ImmediateAddressingMode(OpCode.SBC_Immediate))
		{
		}
	}
}
