using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.CMP
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
