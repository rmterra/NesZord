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
	public class Describe_microprocessor_system_functions : nspec
	{
		private static readonly Fixture fixture = new Fixture();

		public void When_call_break()
		{
			Microprocessor processor = null;
			byte[] program = new byte[0];

			before = () =>
			{
				processor = new Microprocessor();
				program = new byte[]
				{
					(byte)OpCode.ImmediateLoadAccumulator, fixture.Create<byte>(),
					(byte)OpCode.TransferFromAccumulatorToX,
					(byte)OpCode.Break,
					(byte)OpCode.ImmediateAddWithCarry, 0xff
				};
			};

			act = () => { processor.RunProgram(program); };

			it["should ignore all commands after execute it"] = () => { processor.Carry.should_be_false(); };
		}
	}
}