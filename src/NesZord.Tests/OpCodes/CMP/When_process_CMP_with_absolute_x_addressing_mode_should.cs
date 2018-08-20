using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.CMP
{
	public class When_process_CMP_with_absolute_x_addressing_mode_should
		: When_process_CMP_should<AbsoluteXAddressingMode>
	{
		public When_process_CMP_with_absolute_x_addressing_mode_should()
			: base(new AbsoluteXAddressingMode(OpCode.CMP_AbsoluteX))
		{
		}
	}
}
