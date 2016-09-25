using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;
using System;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_ora_operation : Describe_microprocessor_operation
	{
		public void When_use_immediate_addressing_mode()
		{
			var accumulatorValue = default(byte);
			var byteToCompare = default(byte);

			this.RunProgram(() => new byte[]
			{
				(byte) OpCode.LDA_Immediate, accumulatorValue,
				(byte) OpCode.ORA_Immediate, byteToCompare
			});

			this.DefineSpecs(
				(b) => accumulatorValue = b,
				(b) => byteToCompare = b);
		}

		public void When_use_zero_page_addressing_mode()
		{
			var randomOffset = default(byte);
			var accumulatorValue = default(byte);

			before = () => randomOffset = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[]
			{
				(byte) OpCode.LDA_Immediate, accumulatorValue,
				(byte) OpCode.ORA_ZeroPage, randomOffset
			});

			this.DefineSpecs(
				(b) => accumulatorValue = b,
				(b) => this.Memory.WriteZeroPage(randomOffset, b));
		}

		public void When_use_zero_page_x_addressing_mode()
		{
			var randomOffset = default(byte);
			var xRegisterValue = default(byte);
			var accumulatorValue = default(byte);

			before = () =>
			{
				randomOffset = this.Fixture.Create<byte>();
				xRegisterValue = this.Fixture.Create<byte>();
			};

			this.RunProgram(() => new byte[]
			{
				(byte) OpCode.LDA_Immediate, accumulatorValue,
				(byte) OpCode.LDX_Immediate, xRegisterValue,
				(byte) OpCode.ORA_ZeroPageX, randomOffset
			});

			this.DefineSpecs(
				(b) => accumulatorValue = b,
				(b) => this.Memory.WriteZeroPage((byte)(xRegisterValue + randomOffset), b));
		}

		public void When_use_absolute_addressing_mode()
		{
			var randomOffset = default(byte);
			var randomPage = default(byte);
			var accumulatorValue = default(byte);

			before = () =>
			{
				randomOffset = this.Fixture.Create<byte>();
				randomPage = this.Fixture.Create<byte>();
			};

			this.RunProgram(() => new byte[]
			{
				(byte) OpCode.LDA_Immediate, accumulatorValue,
				(byte) OpCode.ORA_Absolute, randomOffset, randomPage
			});

			this.DefineSpecs(
				(b) => accumulatorValue = b,
				(b) => this.Memory.Write(randomOffset, randomPage, b));
		}

		public void When_use_absolute_x_addressing_mode()
		{
			var randomOffset = default(byte);
			var randomPage = default(byte);
			var xRegisterValue = default(byte);
			var accumulatorValue = default(byte);

			before = () =>
			{
				randomOffset = this.Fixture.Create<byte>();
				randomPage = this.Fixture.Create<byte>();
				xRegisterValue = this.Fixture.Create<byte>();
			};

			this.RunProgram(() => new byte[]
			{
				(byte) OpCode.LDA_Immediate, accumulatorValue,
				(byte) OpCode.LDX_Immediate, xRegisterValue,
				(byte) OpCode.ORA_AbsoluteX, randomOffset, randomPage
			});

			this.DefineSpecs(
				(b) => accumulatorValue = b,
				(b) => this.Memory.Write(new MemoryLocation(randomOffset, randomPage).Sum(xRegisterValue), b));
		}

		public void When_use_absolute_y_addressing_mode()
		{
			var randomOffset = default(byte);
			var randomPage = default(byte);
			var yRegisterValue = default(byte);
			var accumulatorValue = default(byte);

			before = () =>
			{
				randomOffset = this.Fixture.Create<byte>();
				randomPage = this.Fixture.Create<byte>();
				yRegisterValue = this.Fixture.Create<byte>();
			};

			this.RunProgram(() => new byte[]
			{
				(byte) OpCode.LDA_Immediate, accumulatorValue,
				(byte) OpCode.LDY_Immediate, yRegisterValue,
				(byte) OpCode.ORA_AbsoluteY, randomOffset, randomPage
			});

			this.DefineSpecs(
				(b) => accumulatorValue = b,
				(b) => this.Memory.Write(new MemoryLocation(randomOffset, randomPage).Sum(yRegisterValue), b));
		}

		public void When_use_indexed_indirect_addressing_mode()
		{
			var randomOffset = default(byte);
			var xRegisterValue = default(byte);
			var accumulatorValue = default(byte);

			before = () =>
			{
				randomOffset = this.Fixture.Create<byte>();
				xRegisterValue = this.Fixture.Create<byte>();
			};

			this.RunProgram(() => new byte[]
			{
				(byte) OpCode.LDA_Immediate, accumulatorValue,
				(byte) OpCode.LDX_Immediate, xRegisterValue,
				(byte) OpCode.ORA_IndexedIndirect, randomOffset
			});

			this.DefineSpecs(
				(b) => accumulatorValue = b,
				(b) => this.Memory.MockIndexedIndirectMemoryWrite(randomOffset, xRegisterValue, b));
		}

		public void When_use_indirect_indexed_addressing_mode()
		{
			var randomOffset = default(byte);
			var yRegisterValue = default(byte);
			var accumulatorValue = default(byte);

			before = () =>
			{
				randomOffset = this.Fixture.Create<byte>();
				yRegisterValue = this.Fixture.Create<byte>();
			};

			this.RunProgram(() => new byte[]
			{
				(byte) OpCode.LDA_Immediate, accumulatorValue,
				(byte) OpCode.LDY_Immediate, yRegisterValue,
				(byte) OpCode.ORA_IndirectIndexed, randomOffset
			});

			this.DefineSpecs(
				(b) => accumulatorValue = b,
				(b) => this.Memory.MockIndirectIndexedMemoryWrite(randomOffset, yRegisterValue, b));
		}

		private void DefineSpecs(Action<byte> setAccumulator, Action<byte> setByteToCompare)
		{
			before = () =>
			{
				setAccumulator?.Invoke(0x05);
				setByteToCompare?.Invoke(0x02);
			};

			it["should set bitwise 'or' result on accumulator"] = () => this.Processor.Accumulator.Value.should_be(0x07);

			this.ZeroFlagShouldBeFalse();
			this.NegativeFlagShouldBeFalse();

			context["given thar bitwise 'or' result is equal 0x00"] = () =>
			{
				before = () =>
				{
					setAccumulator?.Invoke(0x00);
					setByteToCompare?.Invoke(0x00);
				};

				this.ZeroFlagShouldBeTrue();
				this.NegativeFlagShouldBeFalse();
			};

			context["given thar bitwise 'or' result has sign bit set"] = () =>
			{
				before = () =>
				{
					setAccumulator?.Invoke(0x05);
					setByteToCompare?.Invoke(0x80);
				};

				this.ZeroFlagShouldBeFalse();
				this.NegativeFlagShouldBeTrue();
			};
		}
	}
}