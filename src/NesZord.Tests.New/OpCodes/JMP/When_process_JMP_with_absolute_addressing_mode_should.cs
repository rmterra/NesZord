using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.JMP
{
	public class When_process_JMP_with_absolute_addressing_mode_should
		: When_process_JMP_should<AbsoluteAddressingMode>
	{
		public When_process_JMP_with_absolute_addressing_mode_should()
			: base(new AbsoluteAddressingMode(OpCode.JMP_Absolute))
		{
		}
	}
}
