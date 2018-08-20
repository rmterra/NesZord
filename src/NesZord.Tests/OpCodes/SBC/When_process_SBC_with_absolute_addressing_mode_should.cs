using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.SBC
{
	public class When_process_SBC_with_absolute_addressing_mode_should
		: When_process_SBC_should<AbsoluteAddressingMode>
	{
		public When_process_SBC_with_absolute_addressing_mode_should()
			: base(new AbsoluteAddressingMode(OpCode.SBC_Absolute))
		{
		}
	}
}
