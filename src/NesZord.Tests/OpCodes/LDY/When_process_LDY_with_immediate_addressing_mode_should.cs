using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.LDY
{
	public class When_process_LDY_with_immediate_addressing_mode_should
		: When_process_LDY_should<ImmediateAddressingMode>
	{
		public When_process_LDY_with_immediate_addressing_mode_should()
			: base(new ImmediateAddressingMode(OpCode.LDY_Immediate))
		{
		}
	}
}
