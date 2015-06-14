using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesZord.Tests
{
	public class Describe_microprocessor_decrement_operations : nspec
	{
		private static readonly Fixture fixture = new Fixture();

		public void When_increment_the_value_of_x_register()
		{
			Microprocessor processor = null;
			byte expectedXRegisterValue = 0x00;

			before = () =>
			{
				processor = new Microprocessor();
				expectedXRegisterValue = fixture.Create<byte>();
			};

			act = () =>
			{
				processor.Start(new byte[]
				{
					(byte)OpCode.ImmediateLoadXRegister, (byte)(expectedXRegisterValue + 1),
					(byte)OpCode.DecrementValueAtX
				});
			};

			it["should increment 1 to program counter"] = () => { processor.ProgramCounter.should_be(3); };
			it["should decrement 1 to x register"] = () => { processor.X.should_be(expectedXRegisterValue); };
		}
	}
}