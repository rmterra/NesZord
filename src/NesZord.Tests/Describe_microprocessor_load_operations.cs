using NesZord.Core;
using NesZord.Core.Extensions;
using NSpec;
using Ploeh.AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesZord.Tests
{
	public class Describe_microprocessor_load_operations : nspec
	{
		private static readonly Fixture fixture = new Fixture();

		public void When_load_the_accumulator_with_immediate_addressing_mode()
		{
			Microprocessor processor = null;
			byte expectedAccumulatorValue = 0x00;

			before = () =>
			{
				processor = new Microprocessor();
				expectedAccumulatorValue = fixture.Create<byte>();
			};

			act = () =>
			{
				byte operation = (byte)OpCode.ImmediateLoadAccumulator;
				processor.Start(new byte[] { operation, expectedAccumulatorValue });
			};

			it["Should increment 2 to program counter"] = () => { processor.ProgramCounter.should_be(2); };
			it["Should set accumulator with received value"] = () => { processor.Accumulator.should_be(expectedAccumulatorValue); };
		}
	}
}