using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;
using System;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_cpy_operation : nspec
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
			var byteToCompare = default(byte);

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDY_Immediate, 0x05,
					(byte)OpCode.CPY_Immediate, byteToCompare
				});
			};

			this.DefineSpecs((b) => byteToCompare = b);
		}

		public void When_use_zero_page_addressing_mode()
		{
			var randomOffset = default(byte);

			before = () => { randomOffset = fixture.Create<byte>(); };

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDY_Immediate, 0x05,
					(byte)OpCode.CPY_ZeroPage, randomOffset
				});
			};

			this.DefineSpecs((b) => this.memory.WriteZeroPage(randomOffset, b));
		}

		public void When_use_absolute_addressing_mode()
		{
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
					(byte)OpCode.LDY_Immediate, 0x05,
					(byte)OpCode.CPY_Absolute, randomOffset, randomPage
				});
			};

			this.DefineSpecs((b) => this.memory.Write(randomOffset, randomPage, b));
		}

		private void DefineSpecs(Action<byte> setByteToCompare)
		{
			context["given that y register value is lower than compared byte"] = () =>
			{
				before = () => { setByteToCompare(0xff); };
				it["should not set negative flag"] = () => { processor.Negative.should_be_false(); };
				it["should not set carry flag"] = () => { processor.Carry.should_be_false(); };
				it["should not set zero flag"] = () => { processor.Zero.should_be_false(); };
			};

			context["given that y register value is equal than compared byte"] = () =>
			{
				before = () => { setByteToCompare(0x05); };
				it["should not set negative flag"] = () => { processor.Negative.should_be_false(); };
				it["should set carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should set zero flag"] = () => { processor.Zero.should_be_true(); };
			};

			context["given that y register value is grater than compared byte"] = () =>
			{
				before = () => { setByteToCompare(0x00); };
				it["should not set negative flag"] = () => { processor.Negative.should_be_false(); };
				it["should set carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should not set zero flag"] = () => { processor.Zero.should_be_false(); };
			};
		}
	}
}