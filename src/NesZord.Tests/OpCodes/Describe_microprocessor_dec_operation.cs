using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;
using System;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_dec_operation : nspec
	{
		private static readonly Fixture fixture = new Fixture();

		private MemoryMock memory;

		private Microprocessor processor;

		public void before_each()
		{
			this.memory = new MemoryMock();
			this.processor = new Microprocessor(this.memory);
		}

		public void When_use_zero_page_addressing_mode()
		{
			var randomOffset = default(byte);

			before = () => randomOffset = fixture.Create<byte>();

			act = () => processor.RunProgram(new byte[] { (byte)OpCode.DEC_ZeroPage, randomOffset });

			this.DefineSpecs(
				(b) => this.memory.WriteZeroPage(randomOffset, b),
				() => this.memory.Read(randomOffset, Memory.ZERO_PAGE));
		}

		public void When_use_zero_page_x_addressing_mode()
		{
			var randomOffset = default(byte);
			var xRegisterValue = default(byte);

			before = () =>
			{
				randomOffset = fixture.Create<byte>();
				xRegisterValue = fixture.Create<byte>();
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDX_Immediate, xRegisterValue,
					(byte)OpCode.DEC_ZeroPageX, randomOffset
				});
			};

			this.DefineSpecs(
				(b) => this.memory.WriteZeroPage((byte)(xRegisterValue + randomOffset), b),
				() => this.memory.Read((byte)(xRegisterValue + randomOffset), Memory.ZERO_PAGE));
		}

		public void When_use_absolute_addressing_mode()
		{
			var randomOffset = default(byte);
			var randomPage = default(byte);

			before = () =>
			{
				randomOffset = fixture.Create<byte>();
				randomPage = fixture.Create<byte>();
			};

			act = () => processor.RunProgram(new byte[] { (byte)OpCode.DEC_Absolute, randomOffset, randomPage });

			this.DefineSpecs(
				(b) => this.memory.Write(randomOffset, randomPage, b),
				() => this.memory.Read(randomOffset, randomPage));
		}

		public void When_use_absolute_x_addressing_mode()
		{
			var randomOffset = default(byte);
			var randomPage = default(byte);
			var xRegisterValue = default(byte);

			before = () =>
			{
				randomOffset = fixture.Create<byte>();
				randomPage = fixture.Create<byte>();
				xRegisterValue = fixture.Create<byte>();
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDX_Immediate, xRegisterValue,
					(byte)OpCode.DEC_AbsoluteX, randomOffset, randomPage
				});
			};

			this.DefineSpecs(
				(b) => this.memory.Write((byte)(xRegisterValue + randomOffset), randomPage, b),
				() => this.memory.Read((byte)(xRegisterValue + randomOffset), randomPage));
		}

		private void DefineSpecs(Action<byte> setByteToDecrement, Func<byte> getDecrementedByte)
		{
			before = () => setByteToDecrement(0x05);

			it["should decrement 1 to memory value"] = () => getDecrementedByte().should_be(0x04);

			context["given that memory value is 0x01"] = () =>
			{
				before = () => setByteToDecrement(0x01);

				it["should set zero flag"] = () => { processor.Zero.should_be_true(); };
				it["should not set negative flag"] = () => { processor.Negative.should_be_false(); };
			};

			context["given that sign bit of memory value is set"] = () =>
			{
				before = () => setByteToDecrement(0x81);

				it["should not set zero flag"] = () => { processor.Zero.should_be_false(); };
				it["should set negative flag"] = () => { processor.Negative.should_be_true(); };
			};
		}
	}
}