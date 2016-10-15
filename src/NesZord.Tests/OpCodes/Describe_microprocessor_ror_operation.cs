using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;
using System;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_ror_operation : Describe_microprocessor_operation
	{
		private bool hashCarry;

		private byte byteToShift;

		public void When_use_accumulator_addressing_mode()
		{
			this.RunProgram(() => new byte[]
			{
				(byte)(this.hashCarry ? OpCode.SEC_Implied : OpCode.CLC_Implied),
				(byte)OpCode.LDA_Immediate, this.byteToShift,
				(byte)OpCode.ROR_Accumulator
			});

			this.DefineSpec(
				b => this.byteToShift = b,
				() => this.Processor.Accumulator.Value);
		}

		public void When_use_zero_page_addressing_mode()
		{
			var randomOffset = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[]
			{
				(byte)(this.hashCarry ? OpCode.SEC_Implied : OpCode.CLC_Implied),
				(byte)OpCode.ROR_ZeroPage, randomOffset
			});

			this.DefineSpec(
				(b) => this.Memory.WriteZeroPage(randomOffset, b),
				() => this.Memory.Read(randomOffset, Core.Memory.ZERO_PAGE));
		}

		public void When_use_zero_page_x_addressing_mode()
		{
			var randomOffset = this.Fixture.Create<byte>();
			var xRegisterValue = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[]
			{
				(byte)(this.hashCarry ? OpCode.SEC_Implied : OpCode.CLC_Implied),
				(byte)OpCode.LDX_Immediate, xRegisterValue,
				(byte)OpCode.ROR_ZeroPageX, randomOffset
			});

			this.DefineSpec(
				(b) => this.Memory.WriteZeroPage((byte)(xRegisterValue + randomOffset), b),
				() => this.Memory.Read((byte)(xRegisterValue + randomOffset), Core.Memory.ZERO_PAGE));
		}

		public void When_use_absolute_addressing_mode()
		{
			var randomPage = this.Fixture.Create<byte>();
			var randomOffset = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[]
			{
				(byte)(this.hashCarry ? OpCode.SEC_Implied : OpCode.CLC_Implied),
				(byte)OpCode.ROR_Absolute, randomOffset, randomPage
			});

			this.DefineSpec(
				(b) => this.Memory.Write(randomOffset, randomPage, b),
				() => this.Memory.Read(randomOffset, randomPage));
		}

		public void When_use_absolute_x_addressing_mode()
		{
			var randomPage = this.Fixture.Create<byte>();
			var randomOffset = this.Fixture.Create<byte>();
			var xRegisterValue = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[]
			{
				(byte)(this.hashCarry ? OpCode.SEC_Implied : OpCode.CLC_Implied),
				(byte)OpCode.LDX_Immediate, xRegisterValue,
				(byte)OpCode.ROR_AbsoluteX, randomOffset, randomPage
			});

			this.DefineSpec(
				(b) => this.Memory.Write(new MemoryLocation(randomOffset, randomPage).Sum(xRegisterValue), b),
				() => this.Memory.Read(new MemoryLocation(randomOffset, randomPage).Sum(xRegisterValue)));
		}

		private void DefineSpec(Action<byte> setByteToShift, Func<byte> readShiftedValue)
		{
			before = () =>
			{
				this.hashCarry = false;
				setByteToShift(0x05);
			};

			it["should shift value to right"] = () => readShiftedValue?.Invoke().should_be(0x02);

			this.CarryFlagShouldBeTrue();
			this.ZeroFlagShouldBeFalse();
			this.NegativeFlagShouldBeFalse();

			context["given that carry flag is set"] = () =>
			{
				before = () => this.hashCarry = true;
				it["should set seventh bit of shifted value"] = () => readShiftedValue?.Invoke().should_be(0x82);
				this.NegativeFlagShouldBeTrue();
			};

			context["given that first bit of byte to shift is not set"] = () =>
			{
				before = () => setByteToShift(0x04);
				this.CarryFlagShouldBeFalse();
			};

			context["given that byte to shift is 0x01"] = () =>
			{
				before = () => setByteToShift(0x01);
				this.CarryFlagShouldBeTrue();
				this.ZeroFlagShouldBeTrue();
			};
		}
	}
}