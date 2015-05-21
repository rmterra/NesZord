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
	public class Describe_microprocessor_store_operations : nspec
	{
		private static readonly Fixture fixture = new Fixture();

		public void When_store_accumulator_with_absolute_addressing_mode()
		{
			Microprocessor processor = null;

			before = () => { processor = new Microprocessor(); };

			act = () =>
			{
				processor.Start(new byte[]
				{
					(byte)OpCode.ImmediateLoadAccumulator, fixture.Create<byte>(),
					(byte)OpCode.AbsoluteStoreAccumulator, 0x00, 0x20
				});
			};

			it["should increment 3 to program counter"] = () => { processor.ProgramCounter.should_be(5); };
			it["should store the accumulator value in $0200"] = () =>
			{
				processor.ValueAt(0x20, 0x00).should_be(processor.Accumulator);
			};
		}
	}
}