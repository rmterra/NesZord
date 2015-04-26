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
	public class DescribeAccumulatorOperations : nspec
	{
		private Fixture fixture;

		public void before_each()
		{
			this.fixture = new Fixture();
		}

		public void When_load_the_accumulator_with_immediate_addressing_mode()
		{
			Microprocessor processor = null;

			before = () => { processor = new Microprocessor(); };

			act = () => { processor.Start(new byte[] { 0xa9, this.fixture.Create<byte>() }); };

			it["should add 2 to program counter"] = () => { processor.ProgramCounter.should_be(2); };
		}
	}
}