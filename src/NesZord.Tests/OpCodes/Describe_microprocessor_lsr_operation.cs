using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;
using System;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_lsr_operation : Describe_microprocessor_operation
	{
		public void When_use_accumulator_addressing_mode()
		{
			var accumulatorValue = default(byte);

			this.RunProgram(() => new byte[]
			{
				(byte) OpCode.LDA_Immediate, accumulatorValue,
				(byte) OpCode.LSR_Accumulator
			});

			this.DefineSpecs(
				(b) => accumulatorValue = b,
				() => this.Processor.Accumulator.Value);
		}

		public void When_zero_page_addressing_mode()
		{
			var randomOffset = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[] { (byte)OpCode.LSR_ZeroPage, randomOffset });

			this.DefineSpecs(() => new MemoryLocation(randomOffset, Core.Memory.ZERO_PAGE));
		}

		public void When_zero_page_x_addressing_mode()
		{
			var randomOffset = this.Fixture.Create<byte>();
			var xRegisterValue = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[]
			{
				(byte) OpCode.LDX_Immediate, xRegisterValue,
				(byte) OpCode.LSR_ZeroPageX, randomOffset
			});

			this.DefineSpecs(() => new MemoryLocation((byte)(xRegisterValue + randomOffset), Core.Memory.ZERO_PAGE));
		}

		public void When_absolute_addressing_mode()
		{
			var randomOffset = this.Fixture.Create<byte>();
			var randomPage = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[] { (byte)OpCode.LSR_Absolute, randomOffset, randomPage });

			this.DefineSpecs(() => new MemoryLocation(randomOffset, randomPage));
		}

		public void When_absolute_x_addressing_mode()
		{
			var randomOffset = this.Fixture.Create<byte>();
			var randomPage = this.Fixture.Create<byte>();
			var xRegisterValue = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[]
			{
				(byte)OpCode.LDX_Immediate, xRegisterValue,
				(byte)OpCode.LSR_AbsoluteX, randomOffset, randomPage
			});

			this.DefineSpecs(() => new MemoryLocation(randomOffset, randomPage).Sum(xRegisterValue));
		}

		private void DefineSpecs(Func<MemoryLocation> getLocation)
		{
			this.DefineSpecs(
				(b) => this.Memory.Write(getLocation?.Invoke(), b),
				() => this.Memory.Read(getLocation?.Invoke()));
		}

		private void DefineSpecs(Action<byte> setValueToBeShifted, Func<byte> getValueToBeShifted)
		{
			before = () => setValueToBeShifted?.Invoke(0x06);

			it["should value be shifted to right"] = () => getValueToBeShifted?.Invoke().should_be(0x03);

			this.CarryFlagShouldBeFalse();
			this.ZeroFlagShouldBeFalse();
			this.NegativeFlagShouldBeFalse();

			context["given that value to be shifted has it first bit set"] = () =>
			{
				before = () => setValueToBeShifted?.Invoke(0x05);

				this.CarryFlagShouldBeTrue();
				this.ZeroFlagShouldBeFalse();
				this.NegativeFlagShouldBeFalse();
			};

			context["given that shifted value is 0x00"] = () =>
			{
				before = () => setValueToBeShifted?.Invoke(0x01);

				this.CarryFlagShouldBeTrue();
				this.ZeroFlagShouldBeTrue();
			};
		}
	}
}