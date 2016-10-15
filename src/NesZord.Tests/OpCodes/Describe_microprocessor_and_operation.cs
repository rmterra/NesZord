using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;
using System;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_and_operation : Describe_microprocessor_operation
	{
		private byte accumulatorValue;

		public void When_use_immediate_addressing_mode()
		{
			var byteToCompare = default(byte);

			this.RunProgram(() => new byte[]
			{
				(byte) OpCode.LDA_Immediate, this.accumulatorValue,
				(byte) OpCode.AND_Immediate, byteToCompare
			});

			this.DefineSpecs(b => byteToCompare = b);
		}

		public void When_use_zero_page_addressing_mode()
		{
			var randomOffset = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[]
			{
				(byte) OpCode.LDA_Immediate, this.accumulatorValue,
				(byte) OpCode.AND_ZeroPage, randomOffset
			});

			this.DefineSpecs(b => this.Memory.WriteZeroPage(randomOffset, b));
		}

		public void When_use_zero_page_x_addressing_mode()
		{
			var	randomOffset = this.Fixture.Create<byte>();
			var	xRegisterValue = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[]
			{
				(byte) OpCode.LDA_Immediate, this.accumulatorValue,
				(byte) OpCode.LDX_Immediate, xRegisterValue,
				(byte) OpCode.AND_ZeroPageX, randomOffset
			});

			this.DefineSpecs(b => this.Memory.WriteZeroPage((byte)(xRegisterValue + randomOffset), b));
		}

		public void When_use_absolute_addressing_mode()
		{
			var	randomOffset = this.Fixture.Create<byte>();
			var	randomPage = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[]
			{
				(byte) OpCode.LDA_Immediate, this.accumulatorValue,
				(byte) OpCode.AND_Absolute, randomOffset, randomPage
			});

			this.DefineSpecs(b => this.Memory.Write(randomOffset, randomPage, b));
		}

		public void When_use_absolute_x_addressing_mode()
		{
			var	randomOffset = this.Fixture.Create<byte>();
			var	randomPage = this.Fixture.Create<byte>();
			var	xRegisterValue = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[]
			{
				(byte) OpCode.LDA_Immediate, this.accumulatorValue,
				(byte) OpCode.LDX_Immediate, xRegisterValue,
				(byte) OpCode.AND_AbsoluteX, randomOffset, randomPage
			});

			this.DefineSpecs(b => this.Memory.Write(new MemoryLocation(randomOffset, randomPage).Sum(xRegisterValue), b));
		}

		public void When_use_absolute_y_addressing_mode()
		{
			var	randomOffset = this.Fixture.Create<byte>();
			var	randomPage = this.Fixture.Create<byte>();
			var	yRegisterValue = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[]
			{
				(byte) OpCode.LDA_Immediate, this.accumulatorValue,
				(byte) OpCode.LDY_Immediate, yRegisterValue,
				(byte) OpCode.AND_AbsoluteY, randomOffset, randomPage
			});

			this.DefineSpecs(b => this.Memory.Write(new MemoryLocation(randomOffset, randomPage).Sum(yRegisterValue), b));
		}

		public void When_use_indexed_indirect_addressing_mode()
		{
			var	randomOffset = this.Fixture.Create<byte>();
			var	xRegisterValue = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[]
			{
				(byte) OpCode.LDA_Immediate, this.accumulatorValue,
				(byte) OpCode.LDX_Immediate, xRegisterValue,
				(byte) OpCode.AND_IndexedIndirect, randomOffset
			});

			this.DefineSpecs(b => this.Memory.MockIndexedIndirectMemoryWrite(randomOffset, xRegisterValue, b));
		}

		public void When_use_indirect_indexed_addressing_mode()
		{
			var	randomOffset = this.Fixture.Create<byte>();
			var	yRegisterValue = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[]
			{
				(byte) OpCode.LDA_Immediate, this.accumulatorValue,
				(byte) OpCode.LDY_Immediate, yRegisterValue,
				(byte) OpCode.AND_IndirectIndexed, randomOffset
			});

			this.DefineSpecs(b => this.Memory.MockIndirectIndexedMemoryWrite(randomOffset, yRegisterValue, b));
		}

		private void DefineSpecs(Action<byte> setByteToCompare)
		{
			before = () => 
			{
				this.accumulatorValue = 0xa9;
				setByteToCompare?.Invoke(0x05);
			};

			it["should set bitwise 'and' result on accumulator"] =  () => this.Processor.Accumulator.Value.should_be(0x01);

			this.ZeroFlagShouldBeFalse();
			this.NegativeFlagShouldBeFalse();

			context["given that accumulator sign bit is set"] = () =>
			{
				before = () =>
				{
					this.accumulatorValue = 0x80;
					setByteToCompare?.Invoke(0x80);
				};

				this.NegativeFlagShouldBeTrue();
			};

			context["given that operation result over accumulator is 0x00"] = () => 
			{
				before = () =>
				{
					this.accumulatorValue = 0x00;
					setByteToCompare?.Invoke(0x00);
				};

				this.ZeroFlagShouldBeTrue();
			};
		}
	}
}