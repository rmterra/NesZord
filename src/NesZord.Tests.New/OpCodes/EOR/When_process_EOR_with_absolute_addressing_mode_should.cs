using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.EOR
{
	public class When_process_EOR_with_absolute_addressing_mode_should
		: When_process_EOR_should<AbsoluteAddressingMode>
	{
		public When_process_EOR_with_absolute_addressing_mode_should()
			: base(new AbsoluteAddressingMode(OpCode.EOR_Absolute))
		{
		}
	}
}
