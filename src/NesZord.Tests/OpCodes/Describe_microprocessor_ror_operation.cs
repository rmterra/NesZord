using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_ror_operation : nspec
	{
		private static readonly Fixture fixture = new Fixture();

		private bool hashCarry;

		private byte byteToShift;

		private MemoryMock memory;

		private Microprocessor processor;

		public void before_each()
		{
			hashCarry = false;
			this.byteToShift = 0x05;
			this.memory = new MemoryMock();
			this.processor = new Microprocessor(this.memory);
		}

		public void When_use_accumulator_addressing_mode()
		{
			act = () => this.processor.RunProgram(new byte[]
			{
				(byte)(this.hashCarry ? OpCode.SEC_Implied : OpCode.CLC_Implied),
				(byte)OpCode.LDA_Immediate, this.byteToShift,
				(byte)OpCode.ROR_Accumulator
			});

			this.DefineSpec(
				b => this.byteToShift = b,
				() => this.processor.Accumulator.Value);
		}

		public void When_use_zero_page_addressing_mode()
		{
			var randomOffset = default(byte);

			before = () =>
			{
				randomOffset = fixture.Create<byte>();
				this.memory.WriteZeroPage(randomOffset, this.byteToShift);
			};

			act = () => this.processor.RunProgram(new byte[]
			{
				(byte)(this.hashCarry ? OpCode.SEC_Implied : OpCode.CLC_Implied),
				(byte)OpCode.ROR_ZeroPage, randomOffset
			});

			this.DefineSpec(
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
				this.memory.WriteZeroPage((byte)(xRegisterValue + randomOffset), byteToShift);
			};

			act = () => this.processor.RunProgram(new byte[]
			{
				(byte)(this.hashCarry ? OpCode.SEC_Implied : OpCode.CLC_Implied),
				(byte)OpCode.LDX_Immediate, xRegisterValue,
				(byte)OpCode.ROR_ZeroPageX, randomOffset
			});

			this.DefineSpec(
				(b) => this.memory.WriteZeroPage((byte)(xRegisterValue + randomOffset), b),
				() => this.memory.Read((byte)(xRegisterValue + randomOffset), Memory.ZERO_PAGE));
		}

		public void When_use_absolute_addressing_mode()
		{
			var randomPage = default(byte);
			var randomOffset = default(byte);

			before = () =>
			{
				randomPage = fixture.Create<byte>();
				randomOffset = fixture.Create<byte>();
				this.memory.Write(randomOffset, randomPage, byteToShift);
			};

			act = () => this.processor.RunProgram(new byte[]
			{
				(byte)(this.hashCarry ? OpCode.SEC_Implied : OpCode.CLC_Implied),
				(byte)OpCode.ROR_Absolute, randomOffset, randomPage
			});

			this.DefineSpec(
				(b) => this.memory.Write(randomOffset, randomPage, b),
				() => this.memory.Read(randomOffset, randomPage));
		}

		public void When_use_absolute_x_addressing_mode()
		{
			var randomPage = default(byte);
			var randomOffset = default(byte);
			var xRegisterValue = default(byte);

			before = () =>
			{
				randomPage = fixture.Create<byte>();
				randomOffset = fixture.Create<byte>();
				xRegisterValue = fixture.Create<byte>();
				this.memory.Write((byte)(xRegisterValue + randomOffset), randomPage, byteToShift);
			};

			act = () => this.processor.RunProgram(new byte[]
			{
				(byte)(this.hashCarry ? OpCode.SEC_Implied : OpCode.CLC_Implied),
				(byte)OpCode.LDX_Immediate, xRegisterValue,
				(byte)OpCode.ROR_AbsoluteX, randomOffset, randomPage
			});

			this.DefineSpec(
				(b) => this.memory.Write((byte)(xRegisterValue + randomOffset), randomPage, b),
				() => this.memory.Read((byte)(xRegisterValue + randomOffset), randomPage));
		}

		private void DefineSpec(Action<byte> setByteToShift, Func<byte> readShiftedValue)
		{
			it["should shift value to right"] = () => readShiftedValue?.Invoke().should_be(0x02);

			it["should set carry flag"] = () => this.processor.Carry.should_be_true();
			it["should not set zero flag"] = () => this.processor.Zero.should_be_false();
			it["should not set negative flag"] = () => this.processor.Negative.should_be_false();

			context["given that carry flag is set"] = () =>
			{
				before = () => this.hashCarry = true;
				it["should set seventh bit of shifted value"] = () => readShiftedValue?.Invoke().should_be(0x82);
				it["should set negative flag"] = () => this.processor.Negative.should_be_true();
			};

			context["given that first bit of byte to shift is not set"] = () =>
			{
				before = () => setByteToShift(0x04);
				it["should not set carry flag"] = () => this.processor.Carry.should_be_false();
			};

			context["given that byte to shift is 0x01"] = () =>
			{
				before = () => setByteToShift(0x01);
				it["should set carry flag"] = () => this.processor.Carry.should_be_true();
				it["should set zero flag"] = () => this.processor.Zero.should_be_true();
			};
		}
	}
}