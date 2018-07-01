using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.STA
{
	public class When_process_STA_with_absolute_addressing_mode_should
		: When_process_STA_should<AbsoluteAddressingMode>
	{
		public When_process_STA_with_absolute_addressing_mode_should()
			: base(new AbsoluteAddressingMode(OpCode.STA_Absolute))
		{
		}
	}
}
