using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;
using System;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_adc_operation : Describe_microprocessor_operation
	{
		private byte accumulatorValue;

		public void When_use_immediate_addressing_mode()
		{
			var byteToAdd = default(byte);

			this.RunProgram(() => new byte[]
			{
				(byte)OpCode.LDA_Immediate, accumulatorValue,
				(byte)OpCode.ADC_Immediate, byteToAdd
			});

			this.DefineSpecs(b => byteToAdd = b);
		}

		public void When_use_zero_page_addressing_mode()
		{
			var randomOffset = default(byte);

			this.RunProgram(() => new byte[]
			{
				(byte)OpCode.LDA_Immediate, this.accumulatorValue,
				(byte)OpCode.ADC_ZeroPage, randomOffset
			});

			this.DefineSpecs(b => this.Memory.WriteZeroPage(randomOffset, b));
		}

		public void When_use_zero_page_x_addressing_mode()
		{
			var xRegisterValue = default(byte);
			var randomOffset = default(byte);

			before = () =>
			{
				xRegisterValue = this.Fixture.Create<byte>();
				randomOffset = this.Fixture.Create<byte>();
			};

			this.RunProgram(() => new byte[]
			{
				(byte)OpCode.LDA_Immediate, this.accumulatorValue,
				(byte)OpCode.LDX_Immediate, xRegisterValue,
				(byte)OpCode.ADC_ZeroPageX, randomOffset
			});

			this.DefineSpecs((b) => this.Memory.WriteZeroPage((byte)(xRegisterValue + randomOffset), b));
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
				(byte)OpCode.LDA_Immediate, this.accumulatorValue,
				(byte)OpCode.ADC_Absolute, randomOffset, randomPage
			});

			this.DefineSpecs((b) => this.Memory.Write(randomOffset, randomPage, b));
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
				(byte)OpCode.LDA_Immediate, this.accumulatorValue,
				(byte)OpCode.LDX_Immediate, xRegisterValue,
				(byte)OpCode.ADC_AbsoluteX, randomOffset, randomPage
			});

			this.DefineSpecs((b) => this.Memory.Write((byte)(xRegisterValue + randomOffset), randomPage, b));
		}

		public void When_use_absolute_y_addressing_mode()
		{
			var randomPage = default(byte);
			var randomOffset = default(byte);
			var yRegisterValue = default(byte);

			before = () =>
			{
				randomPage = this.Fixture.Create<byte>();
				randomOffset = this.Fixture.Create<byte>();
				yRegisterValue = this.Fixture.Create<byte>();
			};

			this.RunProgram(() => new byte[]
			{
				(byte)OpCode.LDA_Immediate, this.accumulatorValue,
				(byte)OpCode.LDY_Immediate, yRegisterValue,
				(byte)OpCode.ADC_AbsoluteY, randomOffset, randomPage
			});

			this.DefineSpecs((b) => this.Memory.Write((byte)(yRegisterValue + randomOffset), randomPage, b));
		}

		public void When_use_indexed_indirect_addressing_mode()
		{
			var xRegisterValue = default(byte);
			var randomOffset = default(byte);

			before = () =>
			{
				xRegisterValue = this.Fixture.Create<byte>();
				randomOffset = this.Fixture.Create<byte>();
			};

			this.RunProgram(() => new byte[]
			{
				(byte)OpCode.LDA_Immediate, this.accumulatorValue,
				(byte)OpCode.LDX_Immediate, xRegisterValue,
				(byte)OpCode.ADC_IndexedIndirect, randomOffset
			});

			this.DefineSpecs((b) => this.Memory.MockIndexedIndirectMemoryWrite(randomOffset, xRegisterValue, b));
		}

		public void When_use_indirect_indexed_addressing_mode()
		{
			var yRegisterValue = default(byte);
			var randomOffset = default(byte);

			before = () =>
			{
				yRegisterValue = this.Fixture.Create<byte>();
				randomOffset = this.Fixture.Create<byte>();
			};

			this.RunProgram(() => new byte[]
			{
				(byte)OpCode.LDA_Immediate, this.accumulatorValue,
				(byte)OpCode.LDY_Immediate, yRegisterValue,
				(byte)OpCode.ADC_IndirectIndexed, randomOffset
			});

			this.DefineSpecs((b) => this.Memory.MockIndirectIndexedMemoryWrite(randomOffset, yRegisterValue, b));
		}

		private void DefineSpecs(Action<byte> setByteToAdd)
		{
			before = () => 
			{
				this.accumulatorValue = 0x05;
				setByteToAdd(0x05);
			};

			it["should add the specified value to accumulator"] = () => { this.Processor.Accumulator.Value.should_be(0x0a); };

			this.CarryFlagShouldBeFalse();
			this.OverflowFlagShouldBeFalse();
			this.NegativeFlagShouldBeFalse();
			this.ZeroFlagShouldBeFalse();

			context["given that carry flag is set"] = () =>
			{
				before = () => { this.Processor.RunProgram(new byte[] { (byte)OpCode.SEC_Implied }); };
				it["should add it to the final result"] = () => { this.Processor.Accumulator.Value.should_be(0x0b); };
			};

			context["given that accumulator sign bit is set"] = () =>
			{
				before = () => { this.accumulatorValue = 0xff; };
				this.OverflowFlagShouldBeTrue();
				this.NegativeFlagShouldBeTrue();
			};

			context["given that result on accumulator is 0x00"] = () =>
			{
				before = () => 
				{
					this.accumulatorValue = 0x00;
					setByteToAdd?.Invoke(0x00);
				};

				this.ZeroFlagShouldBeTrue();
			};

			context["given that calculated result is negative"] = () =>
			{
				before = () => { setByteToAdd?.Invoke(0xff); };

				this.CarryFlagShouldBeTrue();
			};

			context["given that decimal flag is set"] = () =>
			{
				before = () => { this.Processor.RunProgram(new byte[] { (byte)OpCode.SED_Implied }); };

				context["and added byte is 0x95"] = () =>
				{
					before = () => { setByteToAdd?.Invoke(0x95); };

					this.CarryFlagShouldBeTrue();
				};

				context["and added byte is 0x02"] = () =>
				{
					before = () => { setByteToAdd?.Invoke(0x02); };

					this.CarryFlagShouldBeFalse();
				};
			};

			context["given that decimal flag is not set"] = () =>
			{
				context["and added byte is 0xff"] = () =>
				{
					before = () => { setByteToAdd?.Invoke(0xff); };

					this.CarryFlagShouldBeTrue();
				};

				context["and added byte is 0x00"] = () =>
				{
					before = () => { setByteToAdd?.Invoke(0x00); };

					this.CarryFlagShouldBeFalse();
				};
			};
		}
	}
}