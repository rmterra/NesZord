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

		public void When_load_the_y_register_with_immediate_addressing_mode()
		{
			Microprocessor processor = null;
			byte expectedYValue = 0x00;

			before = () =>
			{
				processor = new Microprocessor();
				expectedYValue = fixture.Create<byte>();
			};

			act = () =>
			{
				byte operation = (byte)OpCode.ImmediateLoadYRegister;
				processor.RunProgram(new byte[] { operation, expectedYValue });
			};

			it["should set y register with received value"] = () => { processor.Y.should_be(expectedYValue); };
		}

		public void When_load_the_x_register_with_immediate_addressing_mode()
		{
			Microprocessor processor = null;
			byte expectedXValue = 0x00;

			before = () =>
			{
				processor = new Microprocessor();
				expectedXValue = fixture.Create<byte>();
			};

			act = () =>
			{
				byte operation = (byte)OpCode.ImmediateLoadXRegister;
				processor.RunProgram(new byte[] { operation, expectedXValue });
			};

			it["should set x register with received value"] = () => { processor.X.should_be(expectedXValue); };
		}

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
				processor.RunProgram(new byte[] { operation, expectedAccumulatorValue });
			};

			it["should set accumulator with received value"] = () => { processor.Accumulator.should_be(expectedAccumulatorValue); };
		}
	}
}