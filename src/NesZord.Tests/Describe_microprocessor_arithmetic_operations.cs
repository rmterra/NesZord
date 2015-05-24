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
	public class Describe_microprocessor_arithmetic_operations : nspec
	{
		private static readonly Fixture fixture = new Fixture();

		public void When_add_with_carry_with_immediate_addressing_mode()
		{
			Microprocessor processor = null;
			byte byteToAdd = default(byte);
			int programCounterBeforeAdd = 3;

			before = () =>
			{
				processor = new Microprocessor();
				byteToAdd = fixture.Create<byte>();
			};

			act = () =>
			{
				processor.Start(new byte[]
				{
					(byte)OpCode.ImmediateLoadAccumulator, fixture.Create<byte>(),
					(byte)OpCode.TransferFromAccumulatorToX,
					(byte)OpCode.ImmediateAddWithCarry, byteToAdd
				});
			};

			it["should increment 2 to program counter"] = () => { processor.ProgramCounter.should_be(programCounterBeforeAdd + 2); };
			it["should add the specified value to accumulator"] = () =>
			{
				int resultWithCarry = processor.X + byteToAdd;
				processor.Accumulator.should_be((byte)resultWithCarry & 0xff);
			};

			context["given a byte greater than #ff"] = () =>
			{
				before = () => { byteToAdd = 0xff; };
				it["should turn on carry flag"] = () => { processor.Carry.should_be(true); };
			};

			context["given a byte lower than #ff"] = () =>
			{
				before = () => { byteToAdd = 0x00; };
				it["should not turn on carry flag"] = () => { processor.Carry.should_be(false); };
			};
		}
	}
}