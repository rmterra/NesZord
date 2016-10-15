using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;
using System;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_sbc_operation : Describe_microprocessor_operation
	{
		private byte accumulatorValue;

		public void When_use_immediate_addressing_mode()
		{
			var byteToSubtract = default(byte);

			this.RunProgram(() => new byte[]
			{
				(byte)OpCode.LDA_Immediate, accumulatorValue,
				(byte)OpCode.SBC_Immediate, byteToSubtract
			});

			this.DefineSpecs((b) => byteToSubtract = b);
		}

		public void When_use_zero_page_addressing_mode()
		{
			var randomOffset = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[]
			{
				(byte)OpCode.LDA_Immediate, this.accumulatorValue,
				(byte)OpCode.SBC_ZeroPage, randomOffset
			});

			this.DefineSpecs((b) => this.Memory.WriteZeroPage(randomOffset, b));
		}

		public void When_use_zero_page_x_addressing_mode()
		{
			var randomOffset = this.Fixture.Create<byte>();
			var xRegisterValue = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[]
			{
				(byte)OpCode.LDA_Immediate, this.accumulatorValue,
				(byte)OpCode.LDX_Immediate, xRegisterValue,
				(byte)OpCode.SBC_ZeroPageX, randomOffset
			});

			this.DefineSpecs((b) => this.Memory.WriteZeroPage((byte)(xRegisterValue + randomOffset), b));
		}

		public void When_use_absolute_addressing_mode()
		{
			var randomPage = this.Fixture.Create<byte>();
			var randomOffset = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[]
			{
				(byte)OpCode.LDA_Immediate, this.accumulatorValue,
				(byte)OpCode.SBC_Absolute, randomOffset, randomPage
			});

			this.DefineSpecs((b) => this.Memory.Write(randomOffset, randomPage, b));
		}

		public void When_use_absolute_x_addressing_mode()
		{
			var randomPage = this.Fixture.Create<byte>();
			var randomOffset = this.Fixture.Create<byte>();
			var xRegisterValue = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[]
			{
				(byte)OpCode.LDA_Immediate, this.accumulatorValue,
				(byte)OpCode.LDX_Immediate, xRegisterValue,
				(byte)OpCode.SBC_AbsoluteX, randomOffset, randomPage
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
				(byte)OpCode.LDA_Immediate, this.accumulatorValue,
				(byte)OpCode.LDY_Immediate, yRegisterValue,
				(byte)OpCode.SBC_AbsoluteY, randomOffset, randomPage
			});

			this.DefineSpecs((b) => this.Memory.Write(new MemoryLocation(randomOffset, randomPage).Sum(yRegisterValue), b));
		}

		public void When_use_indexed_indirect_addressing_mode()
		{
			var randomOffset = this.Fixture.Create<byte>();
			var xRegisterValue = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[]
			{
				(byte)OpCode.LDA_Immediate, this.accumulatorValue,
				(byte)OpCode.LDX_Immediate, xRegisterValue,
				(byte)OpCode.SBC_IndexedIndirect, randomOffset
			});

			this.DefineSpecs((b) => this.Memory.MockIndexedIndirectMemoryWrite(randomOffset, xRegisterValue, b));
		}

		public void When_use_indirect_indexed_addressing_mode()
		{
			var	randomOffset = this.Fixture.Create<byte>();
			var	yRegisterValue = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[]
			{
				(byte)OpCode.LDA_Immediate, this.accumulatorValue,
				(byte)OpCode.LDY_Immediate, yRegisterValue,
				(byte)OpCode.SBC_IndirectIndexed, randomOffset
			});

			this.DefineSpecs((b) => this.Memory.MockIndirectIndexedMemoryWrite(randomOffset, yRegisterValue, b));
		}

		private void DefineSpecs(Action<byte> setByteToSubtract)
		{
			this.before = () => 
			{
				this.accumulatorValue = 0x0b;
				setByteToSubtract?.Invoke(0x05);
			};

			it["should subtract the specified value to accumulator"] = () => this.Processor.Accumulator.Value.should_be(0x05);

			this.CarryFlagShouldBeTrue();
			this.OverflowFlagShouldBeFalse();
			this.NegativeFlagShouldBeFalse();
			this.ZeroFlagShouldBeFalse();

			context["given that carry flag is set"] = () =>
			{
				before = () => this.Processor.RunProgram(new byte[] { (byte)OpCode.SEC_Implied });

				it["should not use be it to calculate the final result"] = () => { this.Processor.Accumulator.Value.should_be(0x06); };
			};

			context["given that accumulator sign bit is set"] = () =>
			{
				before = () => { this.accumulatorValue = 0xff; };

				this.OverflowFlagShouldBeTrue();
				this.NegativeFlagShouldBeTrue();
			};

			context["given that calculated result is 0x00"] = () =>
			{
				before = () =>
				{
					this.accumulatorValue = 0x01;
					setByteToSubtract?.Invoke(0x00);
				};

				this.ZeroFlagShouldBeTrue();
			};

			context["given that decimal flag is set"] = () =>
			{
				before = () => { this.Processor.RunProgram(new byte[] { (byte)OpCode.SED_Implied }); };

				context["and calculated result is lower than 0"] = () =>
				{
					before = () =>
					{
						this.accumulatorValue = 0x02;
						setByteToSubtract?.Invoke(0x95);
					};

					this.OverflowFlagShouldBeTrue();
				};

				context["and calculated result is greater than 0"] = () =>
				{
					before = () => this.accumulatorValue = 0x09;

					this.OverflowFlagShouldBeFalse();
				};
			};

			context["given that decimal flag is not set"] = () =>
			{
				context["and calculated result is greater than 127"] = () =>
				{
					before = () => this.accumulatorValue = 0xff;

					this.OverflowFlagShouldBeTrue();
				};

				context["and calculated result is lower than -128"] = () =>
				{
					before = () => setByteToSubtract?.Invoke(0xff);

					this.OverflowFlagShouldBeTrue();
				};

				context["and calculated result is between -128 and 127"] = () =>
				{
					this.OverflowFlagShouldBeFalse();
				};
			};
		}
	}
}