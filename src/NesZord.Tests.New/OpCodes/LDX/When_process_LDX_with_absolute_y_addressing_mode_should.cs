using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.LDX
{
	public class When_process_LDX_with_absolute_y_addressing_mode_should
		: When_process_LDX_should<AbsoluteYAddressingMode>
	{
		public When_process_LDX_with_absolute_y_addressing_mode_should()
			: base(new AbsoluteYAddressingMode(OpCode.LDX_AbsoluteY))
		{
		}
	}
}
