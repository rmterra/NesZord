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
	public class Describe_microprocessor_decrement_operations : nspec
	{
		private static readonly Fixture fixture = new Fixture();

		private Microprocessor processor;

		public void before_each() { this.processor = new Microprocessor(new MemoryMock()); }

		public void When_decrement_the_value_of_x_register()
		{
			var expectedXRegisterValue = default(byte);

			before = () => { expectedXRegisterValue = fixture.Create<byte>(); };

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.ImmediateLDX, (byte)(expectedXRegisterValue + 1),
					(byte)OpCode.DEX
				});
			};

			it["should decrement 1 to x register"] = () => { processor.X.should_be(expectedXRegisterValue); };
		}

		public void When_decrement_the_value_of_y_register()
		{
			var expectedYRegisterValue = default(byte);

			before = () => { expectedYRegisterValue = fixture.Create<byte>(); };

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.ImmediateLDY, (byte)(expectedYRegisterValue + 1),
					(byte)OpCode.DEY
				});
			};

			it["should decrement 1 to y register"] = () => { processor.Y.should_be(expectedYRegisterValue); };
		}
	}
}