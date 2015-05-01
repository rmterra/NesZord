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
	public class Describe_microprocessor_accumulator_operations : nspec
	{
		private static readonly Fixture fixture = new Fixture();

		public void When_receives_load_the_accumulator_with_immediate_addressing_mode()
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

		public void When_receives_store_accumulator_with_absolute_addressing_mode()
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

			it["Should increment 3 to program counter"] = () => { processor.ProgramCounter.should_be(5); };
			it["Should store the accumulator value in $0200"] = () =>
			{
				processor.ValueAt(0x20, 0x00).should_be(processor.Accumulator);
			};
		}
	}
}