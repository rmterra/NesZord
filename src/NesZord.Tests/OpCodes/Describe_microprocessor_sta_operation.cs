using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;
using System;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_sta_operation : Describe_microprocessor_operation
	{
		private byte accumulatorValue = default(byte);

		public void When_use_zero_page_mode()
		{
			var randomOffset = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[]
			{
				(byte)OpCode.LDA_Immediate, this.accumulatorValue,
				(byte)OpCode.STA_ZeroPage, randomOffset
			});

			this.DefineSpecs(() => this.Memory.Read(randomOffset, Core.Memory.ZERO_PAGE));
		}

		public void When_use_zero_page_x_mode()
		{
			var randomOffset = this.Fixture.Create<byte>();
			var xRegisterValue = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[]
			{
				(byte)OpCode.LDX_Immediate, xRegisterValue,
				(byte)OpCode.LDA_Immediate, this.accumulatorValue,
				(byte)OpCode.STA_ZeroPageX, randomOffset
			});

			this.DefineSpecs(() => this.Memory.Read((byte)(randomOffset + xRegisterValue), Core.Memory.ZERO_PAGE));
		}

		public void When_use_absolute_addressing_mode()
		{
			var randomOffset = this.Fixture.Create<byte>();
			var randomPage = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[]
			{
				(byte)OpCode.LDA_Immediate, this.accumulatorValue,
				(byte)OpCode.STA_Absolute, randomOffset, randomPage
			});

			this.DefineSpecs(() => this.Memory.Read(randomOffset, randomPage));
		}

		public void When_use_absolute_y_addressing_mode()
		{
			var randomOffset = this.Fixture.Create<byte>();
			var randomPage = this.Fixture.Create<byte>();
			var yRegisterValue = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[]
			{
				(byte)OpCode.LDY_Immediate, yRegisterValue,
				(byte)OpCode.LDA_Immediate, this.accumulatorValue,
				(byte)OpCode.STA_AbsoluteY, randomOffset, randomPage
			});

			this.DefineSpecs(() => this.Memory.Read(new MemoryLocation(randomOffset, randomPage).Sum(yRegisterValue)));
		}

		public void When_use_absolute_x_addressing_mode()
		{
			var randomOffset = this.Fixture.Create<byte>();
			var randomPage = this.Fixture.Create<byte>();
			var xRegisterValue = this.Fixture.Create<byte>();

			act = () =>
			{
				this.Processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDX_Immediate, xRegisterValue,
					(byte)OpCode.LDA_Immediate, this.accumulatorValue,
					(byte)OpCode.STA_AbsoluteX, randomOffset, randomPage
				});
			};

			this.DefineSpecs(() => this.Memory.Read(new MemoryLocation(randomOffset, randomPage).Sum(xRegisterValue)));
		}

		public void When_use_indexed_indirect_mode()
		{
			var randomOffset = this.Fixture.Create<byte>();
			var xRegisterValue = this.Fixture.Create<byte>();

			var indirectOffset = this.Fixture.Create<byte>();
			var indirectPage = this.Fixture.Create<byte>();

			var computedOffset = (byte)(xRegisterValue + randomOffset);

			this.RunProgram(() => new byte[]
			{
				(byte)OpCode.LDY_Immediate, indirectOffset,
				(byte)OpCode.STY_ZeroPage, computedOffset,

				(byte)OpCode.LDY_Immediate, indirectPage,
				(byte)OpCode.STY_ZeroPage, (byte)(computedOffset + 1),

				(byte)OpCode.LDX_Immediate, xRegisterValue,

				(byte)OpCode.LDA_Immediate, this.accumulatorValue,
				(byte)OpCode.STA_IndexedIndirect, randomOffset
			});

			this.DefineSpecs(() => this.Memory.Read(indirectOffset, indirectPage));
		}

		public void When_use_indirect_indexed_mode()
		{
			var randomOffset = this.Fixture.Create<byte>();
			var yRegisterValue = this.Fixture.Create<byte>();

			var indirectOffset = this.Fixture.Create<byte>();
			var indirectPage = this.Fixture.Create<byte>();

			var targetLocation = new MemoryLocation(indirectOffset, indirectPage).Sum(yRegisterValue);

			this.RunProgram(() => new byte[]
			{
				(byte)OpCode.LDY_Immediate, indirectOffset,
				(byte)OpCode.STY_ZeroPage, randomOffset,

				(byte)OpCode.LDY_Immediate, indirectPage,
				(byte)OpCode.STY_ZeroPage, (byte)(randomOffset + 1),

				(byte)OpCode.LDY_Immediate, yRegisterValue,

				(byte)OpCode.LDA_Immediate, this.accumulatorValue,
				(byte)OpCode.STA_IndirectIndexed, randomOffset
			});

			this.DefineSpecs(() => this.Memory.Read(targetLocation));
		}

		private void DefineSpecs(Func<byte> getExpectedValue)
		{
			before = () => this.accumulatorValue = 0x05;

			it["should store the accumulator value at memory"] = () => { getExpectedValue?.Invoke().should_be(0x05); };
		}
	}
}