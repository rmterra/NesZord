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
	public class Describe_microprocessor_transfer_operations : nspec
	{
		private static readonly Fixture fixture = new Fixture();

		public void When_transfer_from_accumulator_to_x_register()
		{
			Microprocessor processor = null;

			before = () => { processor = new Microprocessor(); };

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.ImmediateLoadAccumulator, fixture.Create<byte>(),
					(byte)OpCode.TransferFromAccumulatorToX
				});
			};

			it["x must be equal to accumulator"] = () => { processor.X.should_be(processor.Accumulator); };
		}
	}
}