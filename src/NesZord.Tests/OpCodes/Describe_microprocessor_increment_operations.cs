using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_increment_operations : nspec
	{
		private static readonly Fixture fixture = new Fixture();

		private Microprocessor processor;

		public void before_each() { this.processor = new Microprocessor(new Memory()); }

		public void When_increment_the_value_of_y_register()
		{
			act = () => { this.processor.RunProgram(new byte[] { (byte)OpCode.IncrementValueAtY }); };

			it["should increment 1 to y register"] = () => { processor.Y.should_be(0x01); };
		}

		public void When_increment_the_value_of_x_register()
		{
			act = () => { this.processor.RunProgram(new byte[] { (byte)OpCode.IncrementValueAtX }); };

			it["should increment 1 to x register"] = () => { processor.X.should_be(0x01); };
		}
	}
}