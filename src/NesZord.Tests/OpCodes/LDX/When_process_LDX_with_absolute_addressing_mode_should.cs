using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.LDX
{
	public class When_process_LDX_with_absolute_addressing_mode_should
		: When_process_LDX_should<AbsoluteAddressingMode>
	{
		public When_process_LDX_with_absolute_addressing_mode_should()
			: base(new AbsoluteAddressingMode(OpCode.LDX_Absolute))
		{
		}
	}
}
