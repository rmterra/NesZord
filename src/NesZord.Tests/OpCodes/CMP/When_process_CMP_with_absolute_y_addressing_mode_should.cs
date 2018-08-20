using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.CMP
{
	public class When_process_CMP_with_absolute_y_addressing_mode_should
		: When_process_CMP_should<AbsoluteYAddressingMode>
	{
		public When_process_CMP_with_absolute_y_addressing_mode_should()
			: base(new AbsoluteYAddressingMode(OpCode.CMP_AbsoluteY))
		{
		}
	}
}
