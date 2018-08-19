using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.SBC
{
	public class When_process_SBC_with_absolute_y_addressing_mode_should
		: When_process_SBC_should<AbsoluteYAddressingMode>
	{
		public When_process_SBC_with_absolute_y_addressing_mode_should()
			: base(new AbsoluteYAddressingMode(OpCode.SBC_AbsoluteY))
		{
		}
	}
}
