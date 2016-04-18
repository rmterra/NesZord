using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;
using System;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_adc_operation : nspec
	{
		private static readonly Fixture fixture = new Fixture();

		private byte accumulatorValue;

		private byte byteToAdd;

		private Microprocessor processor;

		private MemoryMock memory;

		public void before_each()
		{
			this.accumulatorValue = 0x05;
			this.byteToAdd = 0x05;
			this.memory = new MemoryMock();
			this.processor = new Microprocessor(this.memory);
		}

		public void When_use_immediate_addressing_mode()
		{
			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDA_Immediate, accumulatorValue,
					(byte)OpCode.ADC_Immediate, byteToAdd
				});
			};

			this.DefineSpecs(b => this.byteToAdd = b);
		}

		public void When_use_zero_page_addressing_mode()
		{
			var randomOffset = default(byte);

			before = () => this.memory.WriteZeroPage(randomOffset, byteToAdd);

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDA_Immediate, this.accumulatorValue,
					(byte)OpCode.ADC_ZeroPage, randomOffset
				});
			};

			this.DefineSpecs(b => this.memory.WriteZeroPage(randomOffset, b));
		}

		public void When_use_zero_page_x_addressing_mode()
		{
			var xRegisterValue = default(byte);
			var randomOffset = default(byte);

			before = () =>
			{
				xRegisterValue = fixture.Create<byte>();
				randomOffset = fixture.Create<byte>();
				this.memory.WriteZeroPage((byte)(xRegisterValue + randomOffset), byteToAdd);
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDA_Immediate, this.accumulatorValue,
					(byte)OpCode.LDX_Immediate, xRegisterValue,
					(byte)OpCode.ADC_ZeroPageX, randomOffset
				});
			};

			this.DefineSpecs((b) => this.memory.WriteZeroPage((byte)(xRegisterValue + randomOffset), b));
		}

		public void When_use_absolute_addressing_mode()
		{
			var randomPage = default(byte);
			var randomOffset = default(byte);

			before = () =>
			{
				randomPage = fixture.Create<byte>();
				randomOffset = fixture.Create<byte>();
				this.memory.Write(randomOffset, randomPage, byteToAdd);
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDA_Immediate, this.accumulatorValue,
					(byte)OpCode.ADC_Absolute, randomOffset, randomPage
				});
			};

			this.DefineSpecs((b) => this.memory.Write(randomOffset, randomPage, b));
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
				this.memory.Write((byte)(xRegisterValue + randomOffset), randomPage, byteToAdd);
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDA_Immediate, this.accumulatorValue,
					(byte)OpCode.LDX_Immediate, xRegisterValue,
					(byte)OpCode.ADC_AbsoluteX, randomOffset, randomPage
				});
			};

			this.DefineSpecs((b) => this.memory.Write((byte)(xRegisterValue + randomOffset), randomPage, b));
		}

		public void When_use_absolute_y_addressing_mode()
		{
			var randomPage = default(byte);
			var randomOffset = default(byte);
			var yRegisterValue = default(byte);

			before = () =>
			{
				randomPage = fixture.Create<byte>();
				randomOffset = fixture.Create<byte>();
				yRegisterValue = fixture.Create<byte>();

				this.memory.Write((byte)(yRegisterValue + randomOffset), randomPage, byteToAdd);
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDA_Immediate, this.accumulatorValue,
					(byte)OpCode.LDY_Immediate, yRegisterValue,
					(byte)OpCode.ADC_AbsoluteY, randomOffset, randomPage
				});
			};

			this.DefineSpecs((b) => this.memory.Write((byte)(yRegisterValue + randomOffset), randomPage, b));
		}

		public void When_use_indexed_indirect_addressing_mode()
		{
			var xRegisterValue = default(byte);
			var randomOffset = default(byte);

			before = () =>
			{
				xRegisterValue = fixture.Create<byte>();
				randomOffset = fixture.Create<byte>();

				this.memory.MockIndexedIndirectMemoryWrite(randomOffset, xRegisterValue, byteToAdd);
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDA_Immediate, this.accumulatorValue,
					(byte)OpCode.LDX_Immediate, xRegisterValue,
					(byte)OpCode.ADC_IndexedIndirect, randomOffset
				});
			};

			this.DefineSpecs((b) => this.memory.MockIndexedIndirectMemoryWrite(randomOffset, xRegisterValue, b));
		}

		public void When_use_indirect_indexed_addressing_mode()
		{
			var yRegisterValue = default(byte);
			var randomOffset = default(byte);

			before = () =>
			{
				yRegisterValue = fixture.Create<byte>();
				randomOffset = fixture.Create<byte>();

				this.memory.MockIndirectIndexedMemoryWrite(randomOffset, yRegisterValue, byteToAdd);
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDA_Immediate, this.accumulatorValue,
					(byte)OpCode.LDY_Immediate, yRegisterValue,
					(byte)OpCode.ADC_IndirectIndexed, randomOffset
				});
			};

			this.DefineSpecs((b) => this.memory.MockIndirectIndexedMemoryWrite(randomOffset, yRegisterValue, b));
		}

		private void DefineSpecs(Action<byte> setByteToAdd)
		{
			it["should add the specified value to accumulator"] = () => { processor.Accumulator.Value.should_be(0x0a); };
			it["should keep initial value on carry flag"] = () => { processor.Carry.should_be_false(); };
			it["should keep initial value on overflow flag"] = () => { processor.Overflow.should_be_false(); };
			it["should keep initial value on negative flag"] = () => { processor.Negative.should_be_false(); };
			it["should keep initial value on zero flag"] = () => { processor.Zero.should_be_false(); };

			context["given that carry flag is set"] = () =>
			{
				before = () => { this.processor.RunProgram(new byte[] { (byte)OpCode.SEC_Implied }); };
				it["should add it to the final result"] = () => { this.processor.Accumulator.Value.should_be(0x0b); };
			};

			context["given that accumulator sign bit is set"] = () =>
			{
				before = () => { this.accumulatorValue = 0xff; };
				it["should set overflow flag"] = () => { this.processor.Overflow.should_be_true(); };
				it["should set negative flag"] = () => { this.processor.Negative.should_be_true(); };
			};

			context["given that result on accumulator is 0x00"] = () =>
			{
				before = () => 
				{
					this.accumulatorValue = 0x00;
					setByteToAdd?.Invoke(0x00);
				};

				it["should set zero flag"] = () => { this.processor.Zero.should_be_true(); };
			};

			context["given that calculated result is negative"] = () =>
			{
				before = () => { setByteToAdd?.Invoke(0xff); };
				it["should set carry flag"] = () => { this.processor.Carry.should_be_true(); };
			};

			context["given that decimal flag is set"] = () =>
			{
				before = () => { this.processor.RunProgram(new byte[] { (byte)OpCode.SED_Implied }); };

				context["and added byte is 0x95"] = () =>
				{
					before = () => { setByteToAdd?.Invoke(0x95); };
					it["should set carry flag"] = () => { processor.Carry.should_be_true(); };
				};

				context["and added byte is 0x02"] = () =>
				{
					before = () => { setByteToAdd?.Invoke(0x02); };
					it["should not set carry flag"] = () => { processor.Carry.should_be_false(); };
				};
			};

			context["given that decimal flag is not set"] = () =>
			{
				context["and added byte is 0xff"] = () =>
				{
					before = () => { setByteToAdd?.Invoke(0xff); };
					it["should set carry flag"] = () => { processor.Carry.should_be_true(); };
				};

				context["and added byte is 0x00"] = () =>
				{
					before = () => { setByteToAdd?.Invoke(0x00); };
					it["should not set carry flag"] = () => { processor.Carry.should_be_false(); };
				};
			};
		}
	}
}