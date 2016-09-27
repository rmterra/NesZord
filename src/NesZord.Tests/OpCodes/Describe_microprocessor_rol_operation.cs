using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;
using System;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_rol_operation : Describe_microprocessor_operation
	{
		private bool hashCarry;

		private byte byteToShift;

		public void When_use_accumulator_addressing_mode()
		{
			this.RunProgram(() => new byte[]
			{
				(byte)(this.hashCarry ? OpCode.SEC_Implied : OpCode.CLC_Implied),
				(byte)OpCode.LDA_Immediate, this.byteToShift,
				(byte)OpCode.ROL_Accumulator
			});

			this.DefineSpec(
				b => this.byteToShift = b,
				() => this.Processor.Accumulator.Value);
		}

		public void When_use_zero_page_addressing_mode()
		{
			var randomOffset = default(byte);

			before = () => randomOffset = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[]
			{
				(byte)(this.hashCarry ? OpCode.SEC_Implied : OpCode.CLC_Implied),
				(byte)OpCode.ROL_ZeroPage, randomOffset
			});

			this.DefineSpec(
				(b) => this.Memory.WriteZeroPage(randomOffset, b),
				() => this.Memory.Read(randomOffset, Core.Memory.ZERO_PAGE));
		}

		public void When_use_zero_page_x_addressing_mode()
		{
			var randomOffset = default(byte);
			var xRegisterValue = default(byte);

			before = () =>
			{
				randomOffset = this.Fixture.Create<byte>();
				xRegisterValue = this.Fixture.Create<byte>();
			};

			this.RunProgram(() => new byte[]
			{
				(byte)(this.hashCarry ? OpCode.SEC_Implied : OpCode.CLC_Implied),
				(byte)OpCode.LDX_Immediate, xRegisterValue,
				(byte)OpCode.ROL_ZeroPageX, randomOffset
			});

			this.DefineSpec(
				(b) => this.Memory.WriteZeroPage((byte)(xRegisterValue + randomOffset), b),
				() => this.Memory.Read((byte)(xRegisterValue + randomOffset), Core.Memory.ZERO_PAGE));
		}

		public void When_use_absolute_addressing_mode()
		{
			var randomPage = default(byte);
			var randomOffset = default(byte);

			before = () =>
			{
				randomPage = this.Fixture.Create<byte>();
				randomOffset = this.Fixture.Create<byte>();
			};

			this.RunProgram(() => new byte[]
			{
				(byte)(this.hashCarry ? OpCode.SEC_Implied : OpCode.CLC_Implied),
				(byte)OpCode.ROL_Absolute, randomOffset, randomPage
			});

			this.DefineSpec(
				(b) => this.Memory.Write(randomOffset, randomPage, b),
				() => this.Memory.Read(randomOffset, randomPage));
		}

		public void When_use_absolute_x_addressing_mode()
		{
			var randomPage = default(byte);
			var randomOffset = default(byte);
			var xRegisterValue = default(byte);

			before = () =>
			{
				randomPage = this.Fixture.Create<byte>();
				randomOffset = this.Fixture.Create<byte>();
				xRegisterValue = this.Fixture.Create<byte>();
			};

			this.RunProgram(() => new byte[]
			{
				(byte)(this.hashCarry ? OpCode.SEC_Implied : OpCode.CLC_Implied),
				(byte)OpCode.LDX_Immediate, xRegisterValue,
				(byte)OpCode.ROL_AbsoluteX, randomOffset, randomPage
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

			it["should shift value to left"] = () => readShiftedValue?.Invoke().should_be(0x0a);

			this.CarryFlagShouldBeFalse();
			this.ZeroFlagShouldBeFalse();
			this.NegativeFlagShouldBeFalse();

			context["given that carry flag is set"] = () =>
			{
				before = () => this.hashCarry = true;
				it["should increment shifted value"] = () => readShiftedValue?.Invoke().should_be(0x0b);
			};

			context["given that byte to shift has sign bit set"] = () =>
			{
				before = () => setByteToShift(0x80);
				this.CarryFlagShouldBeTrue();
				this.ZeroFlagShouldBeTrue();
			};

			context["given that byte to shift has seventh bit set"] = () =>
			{
				before = () => setByteToShift(0x40);
				this.NegativeFlagShouldBeTrue();
			};
		}
	}
}