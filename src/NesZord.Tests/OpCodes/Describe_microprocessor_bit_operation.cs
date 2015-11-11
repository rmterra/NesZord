using NesZord.Core;
using NesZord.Core.Extensions;
using NSpec;
using Ploeh.AutoFixture;

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
			var testResult = default(byte);
			var randomOffset = default(byte);

			before = () => 
			{
				accumulatorValue = fixure.Create<byte>();
				randomOffset = fixure.Create<byte>();

				var memoryValue = fixure.Create<byte>();
				this.memory.WriteZeroPage(randomOffset, memoryValue);
				testResult = (byte)(accumulatorValue & memoryValue);
			};

			act = () => 
			{
				this.processor.RunProgram(new byte[] 
				{
					(byte) OpCode.LDA_Immediate, accumulatorValue,
					(byte) OpCode.BIT_ZeroPage, randomOffset
				});
			};

			it["should set negative flag equal to last test result bit"] = () =>
			{
				this.processor.Negative.should_be(testResult.GetBitAt(Microprocessor.SIGN_BIT_INDEX));
			};

			it["should set negative flag equal to last test result bit"] = () =>
			{
				this.processor.Overflow.should_be(testResult.GetBitAt(Microprocessor.OVERFLOW_BIT_INDEX));
			};

			it["should set zero flag when test result is 0"] = () => { this.processor.Zero.should_be(testResult == 0); };
		}

		public void When_use_absolute_addressing_mode()
		{
			var accumulatorValue = default(byte);
			var testResult = default(byte);
			var randomOffset = default(byte);
			var randomPage = default(byte);

			before = () =>
			{
				accumulatorValue = fixure.Create<byte>();
				randomOffset = fixure.Create<byte>();
				randomPage = fixure.Create<byte>();

				var memoryValue = fixure.Create<byte>();
				this.memory.Write(randomOffset, randomPage, memoryValue);
				testResult = (byte)(accumulatorValue & memoryValue);
			};

			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte) OpCode.LDA_Immediate, accumulatorValue,
					(byte) OpCode.BIT_Absolute, randomOffset, randomPage
				});
			};

			it["should set negative flag equal to last test result bit"] = () =>
			{
				this.processor.Negative.should_be(testResult.GetBitAt(Microprocessor.SIGN_BIT_INDEX));
			};

			it["should set negative flag equal to last test result bit"] = () =>
			{
				this.processor.Overflow.should_be(testResult.GetBitAt(Microprocessor.OVERFLOW_BIT_INDEX));
			};

			it["should set zero flag when test result is 0"] = () => { this.processor.Zero.should_be(testResult == 0); };
		}
	}
}
