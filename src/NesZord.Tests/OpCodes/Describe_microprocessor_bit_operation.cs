using NesZord.Core;
using NesZord.Core.Extensions;
using NSpec;
using Ploeh.AutoFixture;
using System;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_bit_operation : nspec
	{
		private static readonly Fixture fixure = new Fixture();

		private MemoryMock memory;

		private Microprocessor processor;

		public void before_each()
		{
			this.memory = new MemoryMock();
			this.processor = new Microprocessor(this.memory);
		}

		public void When_use_zero_page_addressing_mode()
		{
			var accumulatorValue = default(byte);
			var randomOffset = default(byte);

			before = () => randomOffset = fixure.Create<byte>();

			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte) OpCode.LDA_Immediate, accumulatorValue,
					(byte) OpCode.BIT_ZeroPage, randomOffset
				});
			};

			this.DefineSpecs(
				(b) => accumulatorValue = b,
				(b) => this.memory.WriteZeroPage(randomOffset, b));
		}

		public void When_use_absolute_addressing_mode()
		{
			var accumulatorValue = default(byte);
			var randomOffset = default(byte);
			var randomPage = default(byte);

			before = () =>
			{
				randomOffset = fixure.Create<byte>();
				randomPage = fixure.Create<byte>();
			};

			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte) OpCode.LDA_Immediate, accumulatorValue,
					(byte) OpCode.BIT_Absolute, randomOffset, randomPage
				});
			};

			this.DefineSpecs(
				(b) => accumulatorValue = b,
				(b) => this.memory.Write(randomOffset, randomPage, b));
		}

		private void DefineSpecs(Action<byte> setAccumulator, Action<byte> setByteToTest)
		{
			context["given that sign bit is not set on acccumulator and byte to test"] = () =>
			{
				before = () =>
				{
					setAccumulator?.Invoke(0x05);
					setByteToTest?.Invoke(0x05);
				};

				this.TestNegativeAndZero(false, false);
			};

			context["given that overflow bit is not set on acccumulator and byte to test"] = () =>
			{
				before = () =>
				{
					setAccumulator?.Invoke(0x05);
					setByteToTest?.Invoke(0x05);
				};

				this.TestOverflowAndZero(false, false);
			};

			context["given that sign bit is not set on acccumulator but is on byte to test"] = () =>
			{
				before = () =>
				{
					setAccumulator?.Invoke(0x05);
					setByteToTest?.Invoke(0x80);
				};

				this.TestNegativeAndZero(false, true);
			};

			context["given that overflow bit is not set on acccumulator but is on byte to test"] = () =>
			{
				before = () =>
				{
					setAccumulator?.Invoke(0x05);
					setByteToTest?.Invoke(0x40);
				};

				this.TestOverflowAndZero(false, true);
			};

			context["given that sign bit is set on acccumulator but not is on byte to test"] = () =>
			{
				before = () =>
				{
					setAccumulator?.Invoke(0x80);
					setByteToTest?.Invoke(0x05);
				};

				this.TestNegativeAndZero(false, true);
			};

			context["given that overflow bit is set on acccumulator but not is on byte to test"] = () =>
			{
				before = () =>
				{
					setAccumulator?.Invoke(0x40);
					setByteToTest?.Invoke(0x05);
				};

				this.TestOverflowAndZero(false, true);
			};

			context["given that sign bit is set on acccumulator and byte to test"] = () =>
			{
				before = () =>
				{
					setAccumulator?.Invoke(0x80);
					setByteToTest?.Invoke(0x80);
				};

				this.TestNegativeAndZero(true, false);
			};

			context["given that overflow bit is set on acccumulator and byte to test"] = () =>
			{
				before = () =>
				{
					setAccumulator?.Invoke(0x40);
					setByteToTest?.Invoke(0x40);
				};

				this.TestOverflowAndZero(true, false);
			};

			context["given that all bytes from accumulator are different from byte to test"] = () =>
			{
				before = () =>
				{
					setAccumulator?.Invoke(0x40);
					setByteToTest?.Invoke(0xbf);
				};

				this.TestZero(true);
			};
		}

		private void TestNegativeAndZero(bool negative, bool zero)
		{
			var negativeFlagSentence = negative ? "should set negative flag" : "should not set negative flag";
			it[negativeFlagSentence] = () => this.processor.Negative.should_be(negative);
			this.TestZero(zero);
		}

		private void TestOverflowAndZero(bool overflow, bool zero)
		{
			var overflowFlagSentence = overflow ? "should set overflow flag" : "should not set overflow flag";
			it[overflowFlagSentence] = () => this.processor.Overflow.should_be(overflow);
			this.TestZero(zero);
		}

		private void TestZero(bool zero)
		{
			var zeroFlagSentence = zero ? "should set zero flag" : "should not set zero flag";
			it[zeroFlagSentence] = () => this.processor.Zero.should_be(zero);
		}
	}
}