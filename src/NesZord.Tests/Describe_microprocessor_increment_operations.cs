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
	public class Describe_microprocessor_increment_operations : nspec
	{
		private static readonly Fixture fixture = new Fixture();

		public void When_increment_the_value_of_x_register()
		{
			Microprocessor processor = null;

			before = () => { processor = new Microprocessor(); };

			act = () => { processor.RunProgram(new byte[] { (byte)OpCode.IncrementValueAtX }); };

			it["should increment 1 to x register"] = () => { processor.X.should_be(0x01); };
		}
	}
}