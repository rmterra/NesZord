using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;
using System;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_cpx_operation : nspec
	{
		private static readonly Fixture fixture = new Fixture();

		private MemoryMock memory;

		private Microprocessor processor;

		public void before_each()
		{
			this.memory = new MemoryMock();
			this.processor = new Microprocessor(this.memory);
		}

		public void When_use_immediate_addressing_mode()
		{
			var xRegisterValue = default(byte);
			var byteToCompare = default(byte);

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDX_Immediate, xRegisterValue,
					(byte)OpCode.CPX_Immediate, byteToCompare
				});
			};

			this.DefineSpec(
				(b) => xRegisterValue = b,
				(b) => byteToCompare = b);
		}

		public void When_use_zero_page_addressing_mode()
		{
			var randomOffset = default(byte);
			var xRegisterValue = default(byte);

			before = () => randomOffset = fixture.Create<byte>();

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDX_Immediate, xRegisterValue,
					(byte)OpCode.CPX_ZeroPage, randomOffset
				});
			};

			this.DefineSpec(
				(b) => xRegisterValue = b,
				(b) => this.memory.WriteZeroPage(randomOffset, b));
		}

		public void When_use_absolute_addressing_mode()
		{
			var xRegisterValue = default(byte);
			var randomPage = default(byte);
			var randomOffset = default(byte);

			before = () =>
			{
				randomPage = fixture.Create<byte>();
				randomOffset = fixture.Create<byte>();
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDX_Immediate, xRegisterValue,
					(byte)OpCode.CPX_Absolute, randomOffset, randomPage
				});
			};

			this.DefineSpec(
				(b) => xRegisterValue = b,
				(b) => this.memory.Write(randomOffset, randomPage, b));
		}

		private void DefineSpec(Action<byte> setXRegisterValue, Action<byte> setByteToCompare)
		{
			before = () => { setXRegisterValue(0x05); };

			context["given that x register value is lower than compared byte"] = () =>
			{
				before = () => { setByteToCompare(0xff); };
				it["should not set on negative flag"] = () => { processor.Negative.should_be_false(); };
				it["should not set on carry flag"] = () => { processor.Carry.should_be_false(); };
				it["should not set on zero flag"] = () => { processor.Zero.should_be_false(); };
			};

			context["given that x register value is equal than compared byte"] = () =>
			{
				before = () => { setByteToCompare(0x05); };
				it["should not set on negative flag"] = () => { processor.Negative.should_be_false(); };
				it["should set carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should set zero flag"] = () => { processor.Zero.should_be_true(); };
			};

			context["given that x register value is greater than compared byte"] = () =>
			{
				before = () => { setByteToCompare(0x00); };
				it["should not set on negative flag"] = () => { processor.Negative.should_be_false(); };
				it["should set carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should not set on zero flag"] = () => { processor.Zero.should_be_false(); };
			};

			context["given compare result is greater than 0x80"] = () =>
			{
				before = () =>
				{
					setXRegisterValue(0xff);
					setByteToCompare(0x00);
				};

				it["should set negative flag"] = () => { processor.Negative.should_be_true(); };
				it["should set carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should not set on zero flag"] = () => { processor.Zero.should_be_false(); };
			};
		}
	}
}