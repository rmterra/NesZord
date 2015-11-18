using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;
using System;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_sbc_operation : nspec
	{
		private static readonly Fixture fixture = new Fixture();

		private byte accumulatorValue;

		private byte byteToSubtract;

		private Microprocessor processor;

		private MemoryMock memory;

		public void before_each()
		{
			this.accumulatorValue = 0x0b;
			this.byteToSubtract = 0x05;
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
					(byte)OpCode.SBC_Immediate, byteToSubtract
				});
			};

			this.DefineSpecs((b) => this.byteToSubtract = b);
		}

		public void When_use_zero_page_addressing_mode()
		{
			var randomOffset = default(byte);

			before = () => 
			{
				randomOffset = fixture.Create<byte>();
				this.memory.WriteZeroPage(randomOffset, byteToSubtract);
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDA_Immediate, this.accumulatorValue,
					(byte)OpCode.SBC_ZeroPage, randomOffset
				});
			};

			this.DefineSpecs((b) => this.memory.WriteZeroPage(randomOffset, b));
		}

		public void When_use_zero_page_x_addressing_mode()
		{
			var randomOffset = default(byte);
			var xRegisterValue = default(byte);

			before = () =>
			{
				randomOffset = fixture.Create<byte>();
				xRegisterValue = fixture.Create<byte>();
				this.memory.WriteZeroPage((byte)(xRegisterValue + randomOffset), byteToSubtract);
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDA_Immediate, this.accumulatorValue,
					(byte)OpCode.LDX_Immediate, xRegisterValue,
					(byte)OpCode.SBC_ZeroPageX, randomOffset
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
				this.memory.Write(randomOffset, randomPage, byteToSubtract);
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDA_Immediate, this.accumulatorValue,
					(byte)OpCode.SBC_Absolute, randomOffset, randomPage
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
				this.memory.Write((byte)(xRegisterValue + randomOffset), randomPage, byteToSubtract);
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDA_Immediate, this.accumulatorValue,
					(byte)OpCode.LDX_Immediate, xRegisterValue,
					(byte)OpCode.SBC_AbsoluteX, randomOffset, randomPage
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
				this.memory.Write((byte)(yRegisterValue + randomOffset), randomPage, byteToSubtract);
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDA_Immediate, this.accumulatorValue,
					(byte)OpCode.LDY_Immediate, yRegisterValue,
					(byte)OpCode.SBC_AbsoluteY, randomOffset, randomPage
				});
			};

			this.DefineSpecs((b) => this.memory.Write((byte)(yRegisterValue + randomOffset), randomPage, b));
		}

		public void When_use_indexed_indirect_addressing_mode()
		{
			var randomOffset = default(byte);
			var xRegisterValue = default(byte);

			before = () =>
			{
				randomOffset = fixture.Create<byte>();
				xRegisterValue = fixture.Create<byte>();
				this.memory.MockIndexedIndirectMemoryWrite(randomOffset, xRegisterValue, byteToSubtract);
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDA_Immediate, this.accumulatorValue,
					(byte)OpCode.LDX_Immediate, xRegisterValue,
					(byte)OpCode.SBC_IndexedIndirect, randomOffset
				});
			};

			this.DefineSpecs((b) => this.memory.MockIndexedIndirectMemoryWrite(randomOffset, xRegisterValue, b));
		}

		public void When_use_indirect_indexed_addressing_mode()
		{
			var randomOffset = default(byte);
			var yRegisterValue = default(byte);

			before = () =>
			{
				randomOffset = fixture.Create<byte>();
				yRegisterValue = fixture.Create<byte>();
				this.memory.MockIndirectIndexedMemoryWrite(randomOffset, yRegisterValue, byteToSubtract);
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDA_Immediate, this.accumulatorValue,
					(byte)OpCode.LDY_Immediate, yRegisterValue,
					(byte)OpCode.SBC_IndirectIndexed, randomOffset
				});
			};

			this.DefineSpecs((b) => this.memory.MockIndirectIndexedMemoryWrite(randomOffset, yRegisterValue, b));
		}

		private void DefineSpecs(Action<byte> setByteToSubtract)
		{
			it["should subtract the specified value to accumulator"] = () => processor.Accumulator.should_be(0x05);
			it["should set carry flag"] = () => { processor.Carry.should_be_true(); };
			it["should keep initial value on overflow flag"] = () => { processor.Overflow.should_be_false(); };
			it["should keep initial value on negative flag"] = () => { processor.Negative.should_be_false(); };
			it["should keep initial value on zero flag"] = () => { processor.Zero.should_be_false(); };

			context["given that carry flag is set"] = () =>
			{
				before = () => { this.processor.RunProgram(new byte[] { (byte)OpCode.SEC_Implied }); };
				it["should not use be it to calculate the final result"] = () => { this.processor.Accumulator.should_be(0x06); };
			};

			context["given that accumulator sign bit is set"] = () =>
			{
				before = () => { this.accumulatorValue = 0xff; };
				it["should set overflow flag"] = () => { this.processor.Overflow.should_be_true(); };
				it["should set negative flag"] = () => { this.processor.Negative.should_be_true(); };
			};

			context["given that calculated result is 0x00"] = () =>
			{
				before = () =>
				{
					this.accumulatorValue = 0x01;
					setByteToSubtract?.Invoke(0x00);
				};

				it["should set zero flag"] = () => { this.processor.Zero.should_be_true(); };
			};

			context["given that decimal flag is set"] = () =>
			{
				before = () => { this.processor.RunProgram(new byte[] { (byte)OpCode.SED_Implied }); };

				context["and calculated result is lower than 0"] = () =>
				{
					before = () => 
					{
						this.accumulatorValue = 0x02;
						setByteToSubtract?.Invoke(0x95);
					};
					it["should set overflow flag"] = () => { processor.Overflow.should_be_true(); };
				};

				context["and calculated result is greater than 0"] = () =>
				{
					before = () => accumulatorValue = 0x09;
					it["should not set overflow flag"] = () => { processor.Overflow.should_be_false(); };
				};
			};

			context["given that decimal flag is not set"] = () =>
			{
				context["and calculated result is greater than 127"] = () =>
				{
					before = () => accumulatorValue = 0xff;
					it["should set overflow flag"] = () => { processor.Overflow.should_be_true(); };
				};

				context["and calculated result is lower than -128"] = () =>
				{
					before = () => setByteToSubtract?.Invoke(0xff);
					it["should set overflow flag"] = () => { processor.Overflow.should_be_true(); };
				};

				context["and calculated result is between -128 and 127"] = () =>
				{
					it["should not set overflow flag"] = () => { processor.Overflow.should_be_false(); };
				};
			};
		}
	}
}
