using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.ROR
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
