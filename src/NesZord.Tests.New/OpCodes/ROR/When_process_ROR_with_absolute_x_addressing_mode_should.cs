using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.ROR
{
	public class When_process_ROR_with_absolute_y_addressing_mode_should
		: When_process_ROR_should<AbsoluteXAddressingMode>
	{
		public When_process_ROR_with_absolute_y_addressing_mode_should()
			: base(new AbsoluteXAddressingMode(OpCode.ROR_AbsoluteX))
		{
		}
	}
}
