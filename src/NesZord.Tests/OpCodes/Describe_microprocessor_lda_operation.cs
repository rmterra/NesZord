using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;
using System;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_lda_operation : Describe_microprocessor_operation
	{
		public void When_use_immediate_addressing_mode()
		{
			var accumulatorValue = default(byte);

			this.RunProgram(() => new byte[] { (byte)OpCode.LDA_Immediate, accumulatorValue });

			this.DefineSpecs((b) => accumulatorValue = b);
		}

		public void When_use_zero_page_addressing_mode()
		{
			var randomOffset = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[] { (byte)OpCode.LDA_ZeroPage, randomOffset });

			this.DefineSpecs((b) => this.Memory.WriteZeroPage(randomOffset, b));
		}

		public void When_use_zero_x_page_addressing_mode()
		{
			var randomOffset = this.Fixture.Create<byte>();
			var xRegisterValue = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[]
			{
				(byte)OpCode.LDX_Immediate, xRegisterValue,
				(byte)OpCode.LDA_ZeroPageX, randomOffset
			});

			this.DefineSpecs((b) => this.Memory.WriteZeroPage((byte)(xRegisterValue + randomOffset), b));
		}

		public void When_use_absolute_addressing_mode()
		{
			var randomPage = this.Fixture.Create<byte>();
			var randomOffset = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[] { (byte)OpCode.LDA_Absolute, randomOffset, randomPage });

			this.DefineSpecs((b) => this.Memory.Write(randomOffset, randomPage, b));
		}

		public void When_use_absolute_x_addressing_mode()
		{
			var randomPage = this.Fixture.Create<byte>();
			var randomOffset = this.Fixture.Create<byte>();
			var xRegisterValue = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[]
			{
				(byte)OpCode.LDX_Immediate, xRegisterValue,
				(byte)OpCode.LDA_AbsoluteX, randomOffset, randomPage
			});

			this.DefineSpecs((b) => this.Memory.Write(new MemoryLocation(randomOffset, randomPage).Sum(xRegisterValue), b));
		}

		public void When_use_absolute_y_addressing_mode()
		{
			var randomPage = this.Fixture.Create<byte>();
			var randomOffset = this.Fixture.Create<byte>();
			var yRegisterValue = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[]
			{
				(byte)OpCode.LDY_Immediate, yRegisterValue,
				(byte)OpCode.LDA_AbsoluteY, randomOffset, randomPage
			});

			this.DefineSpecs((b) => this.Memory.Write(new MemoryLocation(randomOffset, randomPage).Sum(yRegisterValue), b));
		}

		public void When_use_indexed_indirect_addressing_mode()
		{
			var randomOffset = this.Fixture.Create<byte>();
			var xRegisterValue = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[]
			{
				(byte)OpCode.LDX_Immediate, xRegisterValue,
				(byte)OpCode.LDA_IndexedIndirect, randomOffset
			});

			this.DefineSpecs((b) => this.Memory.MockIndexedIndirectMemoryWrite(randomOffset, xRegisterValue, b));
		}

		public void When_use_indirect_indexed_addressing_mode()
		{
			var randomOffset = this.Fixture.Create<byte>();
			var yRegisterValue = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[]
			{
				(byte)OpCode.LDY_Immediate, yRegisterValue,
				(byte)OpCode.LDA_IndirectIndexed, randomOffset
			});

			this.DefineSpecs((b) => this.Memory.MockIndirectIndexedMemoryWrite(randomOffset, yRegisterValue, b));
		}

		private void DefineSpecs(Action<byte> setValue)
		{
			before = () => setValue?.Invoke(0x05);

			it["should set accumulator with received value"] = () => this.Processor.Accumulator.Value.should_be(0x05);

			context["given that new accumulator value is 0x00"] = () =>
			{
				before = () => setValue?.Invoke(0x00);

				this.ZeroFlagShouldBeTrue();
				this.NegativeFlagShouldBeFalse();
			};

			context["given that new accumulator has sign bit set"] = () =>
			{
				before = () => setValue?.Invoke(0x80);

				this.ZeroFlagShouldBeFalse();
				this.NegativeFlagShouldBeTrue();
			};
		}
	}
}