using NesZord.Core;
using Ploeh.AutoFixture;
using System;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_bit_operation : Describe_microprocessor_operation
	{
		public void When_use_zero_page_addressing_mode()
		{
			var accumulatorValue = default(byte);
			var randomOffset = default(byte);
			
			before = () => randomOffset = this.Fixture.Create<byte>();

			act = () =>
			{
				this.Processor.RunProgram(new byte[]
				{
					(byte) OpCode.LDA_Immediate, accumulatorValue,
					(byte) OpCode.BIT_ZeroPage, randomOffset
				});
			};

			this.DefineSpecs(
				(b) => accumulatorValue = b,
				(b) => this.Memory.WriteZeroPage(randomOffset, b));
		}

		public void When_use_absolute_addressing_mode()
		{
			var accumulatorValue = default(byte);
			var randomOffset = default(byte);
			var randomPage = default(byte);

			before = () =>
			{
				randomOffset = this.Fixture.Create<byte>();
				randomPage = this.Fixture.Create<byte>();
			};

			act = () =>
			{
				this.Processor.RunProgram(new byte[]
				{
					(byte) OpCode.LDA_Immediate, accumulatorValue,
					(byte) OpCode.BIT_Absolute, randomOffset, randomPage
				});
			};

			this.DefineSpecs(
				(b) => accumulatorValue = b,
				(b) => this.Memory.Write(randomOffset, randomPage, b));
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

				this.NegativeFlagShouldBeFalse();
				this.ZeroFlagShouldBeFalse();
			};

			context["given that overflow bit is not set on acccumulator and byte to test"] = () =>
			{
				before = () =>
				{
					setAccumulator?.Invoke(0x05);
					setByteToTest?.Invoke(0x05);
				};

				this.OverflowFlagShouldBeFalse();
				this.ZeroFlagShouldBeFalse();
			};

			context["given that sign bit is not set on acccumulator but is on byte to test"] = () =>
			{
				before = () =>
				{
					setAccumulator?.Invoke(0x05);
					setByteToTest?.Invoke(0x80);
				};

				this.NegativeFlagShouldBeFalse();
				this.ZeroFlagShouldBeTrue();
			};

			context["given that overflow bit is not set on acccumulator but is on byte to test"] = () =>
			{
				before = () =>
				{
					setAccumulator?.Invoke(0x05);
					setByteToTest?.Invoke(0x40);
				};

				this.OverflowFlagShouldBeFalse();
				this.ZeroFlagShouldBeTrue();
			};

			context["given that sign bit is set on acccumulator but not is on byte to test"] = () =>
			{
				before = () =>
				{
					setAccumulator?.Invoke(0x80);
					setByteToTest?.Invoke(0x05);
				};

				this.NegativeFlagShouldBeFalse();
				this.ZeroFlagShouldBeTrue();
			};

			context["given that overflow bit is set on acccumulator but not is on byte to test"] = () =>
			{
				before = () =>
				{
					setAccumulator?.Invoke(0x40);
					setByteToTest?.Invoke(0x05);
				};

				this.OverflowFlagShouldBeFalse();
				this.ZeroFlagShouldBeTrue();
			};

			context["given that sign bit is set on acccumulator and byte to test"] = () =>
			{
				before = () =>
				{
					setAccumulator?.Invoke(0x80);
					setByteToTest?.Invoke(0x80);
				};

				this.NegativeFlagShouldBeTrue();
				this.ZeroFlagShouldBeFalse();
			};

			context["given that overflow bit is set on acccumulator and byte to test"] = () =>
			{
				before = () =>
				{
					setAccumulator?.Invoke(0x40);
					setByteToTest?.Invoke(0x40);
				};

				this.OverflowFlagShouldBeTrue();
				this.ZeroFlagShouldBeFalse();
			};

			context["given that all bytes from accumulator are different from byte to test"] = () =>
			{
				before = () =>
				{
					setAccumulator?.Invoke(0x40);
					setByteToTest?.Invoke(0xbf);
				};

				this.ZeroFlagShouldBeTrue();
			};
		}
	}
}