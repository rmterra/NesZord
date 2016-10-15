using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;
using NesZord.Core.Extensions;
using System;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_eor_operation : Describe_microprocessor_operation
	{
		public void When_use_immediate_addressing_mode()
		{
			var accumulatorValue = default(byte);
			var byteToCompare = default(byte);

			this.RunProgram(() => new byte[]
			{
				(byte) OpCode.LDA_Immediate, accumulatorValue,
				(byte) OpCode.EOR_Immediate, byteToCompare
			});

			this.DefineSpecs(
				(b) => accumulatorValue = b,
				(b) => byteToCompare = b);
		}

		public void When_use_zero_page_addressing_mode()
		{
			var accumulatorValue = default(byte);
			var randomOffset = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[]
			{
				(byte) OpCode.LDA_Immediate, accumulatorValue,
				(byte) OpCode.EOR_ZeroPage, randomOffset
			});

			this.DefineSpecs(
				(b) => accumulatorValue = b,
				(b) => this.Memory.WriteZeroPage(randomOffset, b));
		}

		public void When_use_zero_page_x_addressing_mode()
		{
			var accumulatorValue = default(byte);
			var randomOffset = this.Fixture.Create<byte>();
			var xRegisterValue = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[]
			{
				(byte) OpCode.LDA_Immediate, accumulatorValue,
				(byte) OpCode.LDX_Immediate, xRegisterValue,
				(byte) OpCode.EOR_ZeroPageX, randomOffset
			});

			this.DefineSpecs(
				(b) => accumulatorValue = b,
				(b) => this.Memory.WriteZeroPage((byte)(xRegisterValue + randomOffset), b));
		}

		public void When_use_absolute_addressing_mode()
		{
			var accumulatorValue = default(byte);
			var randomOffset = this.Fixture.Create<byte>();
			var randomPage = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[]
			{
				(byte) OpCode.LDA_Immediate, accumulatorValue,
				(byte) OpCode.EOR_Absolute, randomOffset, randomPage
			});

			this.DefineSpecs(
				(b) => accumulatorValue = b,
				(b) => this.Memory.Write(randomOffset, randomPage, b));
		}

		public void When_use_absolute_x_addressing_mode()
		{
			var accumulatorValue = default(byte);
			var randomOffset = this.Fixture.Create<byte>();
			var randomPage = this.Fixture.Create<byte>();
			var xRegisterValue = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[]
			{
				(byte) OpCode.LDA_Immediate, accumulatorValue,
				(byte) OpCode.LDX_Immediate, xRegisterValue,
				(byte) OpCode.EOR_AbsoluteX, randomOffset, randomPage
			});

			this.DefineSpecs(
				(b) => accumulatorValue = b,
				(b) => this.Memory.Write(new MemoryLocation(randomOffset, randomPage).Sum(xRegisterValue), b));
		}

		public void When_use_absolute_y_addressing_mode()
		{
			var accumulatorValue = default(byte);
			var randomOffset = this.Fixture.Create<byte>();
			var randomPage = this.Fixture.Create<byte>();
			var yRegisterValue = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[]
			{
				(byte) OpCode.LDA_Immediate, accumulatorValue,
				(byte) OpCode.LDY_Immediate, yRegisterValue,
				(byte) OpCode.EOR_AbsoluteY, randomOffset, randomPage
			});

			this.DefineSpecs(
				(b) => accumulatorValue = b,
				(b) => this.Memory.Write(new MemoryLocation(randomOffset, randomPage).Sum(yRegisterValue), b));
		}

		public void When_use_indexed_indirect_addressing_mode()
		{
			var accumulatorValue = default(byte);
			var randomOffset = this.Fixture.Create<byte>();
			var xRegisterValue = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[]
			{
				(byte) OpCode.LDA_Immediate, accumulatorValue,
				(byte) OpCode.LDX_Immediate, xRegisterValue,
				(byte) OpCode.EOR_IndexedIndirect, randomOffset
			});

			this.DefineSpecs(
				(b) => accumulatorValue = b,
				(b) => this.Memory.MockIndexedIndirectMemoryWrite(randomOffset, xRegisterValue, b));
		}

		public void When_use_indirect_indexed_addressing_mode()
		{
			var accumulatorValue = default(byte);
			var randomOffset = this.Fixture.Create<byte>();
			var yRegisterValue = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[]
			{
				(byte) OpCode.LDA_Immediate, accumulatorValue,
				(byte) OpCode.LDY_Immediate, yRegisterValue,
				(byte) OpCode.EOR_IndirectIndexed, randomOffset
			});

			this.DefineSpecs(
				(b) => accumulatorValue = b,
				(b) => this.Memory.MockIndirectIndexedMemoryWrite(randomOffset, yRegisterValue, b));
		}

		private void DefineSpecs(Action<byte> setAccumulatorValue, Action<byte> setByteToCompare)
		{
			before = () =>
			{
				setAccumulatorValue?.Invoke(0xa9);
				setByteToCompare?.Invoke(0x05);
			};

			it["should set bitwise 'xor' result on accumulator"] = () => this.Processor.Accumulator.Value.should_be(0xac);

			this.ZeroFlagShouldBeFalse();
			this.NegativeFlagShouldBeTrue();

			context["given that bitwise 'xor' has sign bit is set"] = () =>
			{
				before = () =>
				{
					setAccumulatorValue?.Invoke(0x80);
					setByteToCompare?.Invoke(0x00);
				};

				this.NegativeFlagShouldBeTrue();
				this.ZeroFlagShouldBeFalse();
			};

			context["given that bitwise 'xor' has sign bit is 0x00"] = () =>
			{
				before = () =>
				{
					setAccumulatorValue?.Invoke(0x01);
					setByteToCompare?.Invoke(0x01);
				};

				this.ZeroFlagShouldBeTrue();
				this.NegativeFlagShouldBeFalse();
			};
		}
	}
}
