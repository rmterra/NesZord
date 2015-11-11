using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_system_functions : nspec
	{
		private static readonly Fixture fixture = new Fixture();

		public void When_call_break()
		{
			Microprocessor processor = null;
			var program = new byte[0];

			before = () =>
			{
				processor = new Microprocessor(new Memory());
				program = new byte[]
				{
					(byte)OpCode.LDA_Immediate, fixture.Create<byte>(),
					(byte)OpCode.TAX_Implied,
					(byte)OpCode.BRK_Implied,
					(byte)OpCode.ADC_Immediate, 0xff
				};
			};

			act = () => { processor.RunProgram(program); };

			it["should ignore all commands after execute it"] = () => { processor.Carry.should_be_false(); };
		}
	}
}