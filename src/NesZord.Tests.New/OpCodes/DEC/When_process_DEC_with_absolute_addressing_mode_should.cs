using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.DEC
{
	public class When_process_DEC_with_absolute_addressing_mode_should
		: When_process_DEC_should<AbsoluteAddressingMode>
	{
		public When_process_DEC_with_absolute_addressing_mode_should()
			: base(new AbsoluteAddressingMode(OpCode.DEC_Absolute))
		{
		}
	}
}
