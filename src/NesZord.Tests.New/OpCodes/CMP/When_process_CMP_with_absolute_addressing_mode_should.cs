using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.CMP
{
	public class When_process_CMP_with_absolute_addressing_mode_should
		: When_process_CMP_should<AbsoluteAddressingMode>
	{
		public When_process_CMP_with_absolute_addressing_mode_should()
			: base(new AbsoluteAddressingMode(OpCode.CMP_Absolute))
		{
		}
	}
}
