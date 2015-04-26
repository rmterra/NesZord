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
	public class Describe_microprocessor_accumulator_operations : nspec
	{
		private Fixture fixture;

		public void before_each()
		{
			this.fixture = new Fixture();
		}

		public void When_receives_load_the_accumulator_with_immediate_addressing_mode()
		{
			Microprocessor processor = null;
			byte expectedAccumulatorValue = 0x00;

			before = () =>
			{
				processor = new Microprocessor();
				expectedAccumulatorValue = this.fixture.Create<byte>();
			};

			act = () => { processor.Start(new byte[] { 0xa9, expectedAccumulatorValue }); };

			it["Should add 2 to program counter"] = () => { processor.ProgramCounter.should_be(2); };
			it["Should set accumulator with received value"] = () => { processor.Accumulator.should_be(expectedAccumulatorValue); };
		}
	}
}