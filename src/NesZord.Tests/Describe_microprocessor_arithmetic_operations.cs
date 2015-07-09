using Microsoft.VisualStudio.TestTools.UnitTesting;
using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;
using System;
using System.Collections.Generic;
using System.IO;
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

			before = () =>
			{
				processor = new Microprocessor();
				byteToAdd = fixture.Create<byte>();
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.ImmediateLoadAccumulator, fixture.Create<byte>(),
					(byte)OpCode.TransferFromAccumulatorToX,
					(byte)OpCode.ImmediateAddWithCarry, byteToAdd
				});
			};

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
				it["should not turn on carry flag"] = () => { processor.Carry.should_be_false(); };
			};
		}

		public void When_compare_x_register_with_memory_with_immediate_addressing_mode()
		{
			Microprocessor processor = null;
			byte byteToCompare = default(byte);

			before = () => { processor = new Microprocessor(); };

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.ImmediateLoadXRegister, 0x05,
					(byte)OpCode.ImmediateCompareXRegister, byteToCompare
				});
			};

			context["given that x register value is lower than compared byte"] = () =>
			{
				before = () => { byteToCompare = 0xff; };
				it["should not turn on carry flag"] = () => { processor.Carry.should_be_false(); };
				it["should not turn on zero flag"] = () => { processor.Zero.should_be_false(); };
			};

			context["given that x register value is equal than compared byte"] = () =>
			{
				before = () => { byteToCompare = 0x05; };
				it["should turn on carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should turn on zero flag"] = () => { processor.Zero.should_be_true(); };
			};

			context["given that x register value is grater than compared byte"] = () =>
			{
				before = () => { byteToCompare = 0x00; };
				it["should turn on carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should not turn on zero flag"] = () => { processor.Zero.should_be_false(); };
			};
		}
	}
}