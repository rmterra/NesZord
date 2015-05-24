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
			int expectedProgramCounter = 0;

			before = () =>
			{
				processor = new Microprocessor();
				program = new byte[]
				{
					(byte)OpCode.ImmediateLoadAccumulator, fixture.Create<byte>(),
					(byte)OpCode.TransferFromAccumulatorToX,
					(byte)OpCode.Break,
					(byte)OpCode.ImmediateAddWithCarry, fixture.Create<byte>()
				};

				expectedProgramCounter = program.Length - 2;
			};

			act = () => { processor.Start(program); };

			it["should ignore all commands after execute it"] = () => { processor.ProgramCounter.should_be(expectedProgramCounter); };
		}
	}
}