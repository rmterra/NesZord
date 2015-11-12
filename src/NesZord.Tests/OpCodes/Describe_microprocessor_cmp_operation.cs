using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_cmp_operation : nspec
	{
		private static readonly Fixture fixture = new Fixture();

		private MemoryMock memory;

		private Microprocessor processor;

		public void before_each()
		{
			this.memory = new MemoryMock();
			this.processor = new Microprocessor(this.memory);
		}

		public void When_use_immediate_addressing_mode()
		{
			var accumulatorValue = default(byte);
			var byteToCompare = default(byte);

			before = () => { accumulatorValue = 0x05; };

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDA_Immediate, accumulatorValue,
					(byte)OpCode.CMP_Immediate, byteToCompare
				});
			};

			context["given that accumulator value is lower than compared byte"] = () =>
			{
				before = () => { byteToCompare = 0xff; };
				it["should not set on negative flag"] = () => { processor.Negative.should_be_false(); };
				it["should not set on carry flag"] = () => { processor.Carry.should_be_false(); };
				it["should not set on zero flag"] = () => { processor.Zero.should_be_false(); };
			};

			context["given that accumulator value is equal than compared byte"] = () =>
			{
				before = () => { byteToCompare = 0x05; };
				it["should not set on negative flag"] = () => { processor.Negative.should_be_false(); };
				it["should set carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should set zero flag"] = () => { processor.Zero.should_be_true(); };
			};

			context["given that accumulator value is greater than compared byte"] = () =>
			{
				before = () => { byteToCompare = 0x00; };
				it["should not set on negative flag"] = () => { processor.Negative.should_be_false(); };
				it["should set carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should not set on zero flag"] = () => { processor.Zero.should_be_false(); };
			};

			context["given compare result is greater than 0x80"] = () =>
			{
				before = () =>
				{
					accumulatorValue = 0xff;
					byteToCompare = 0x00;
				};

				it["should set negative flag"] = () => { processor.Negative.should_be_true(); };
				it["should set carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should not set on zero flag"] = () => { processor.Zero.should_be_false(); };
			};
		}

		public void When_use_zero_page_addressing_mode()
		{
			var randomOffset = default(byte);
			var accumulatorValue = default(byte);

			before = () => 
			{
				randomOffset = fixture.Create<byte>();
				accumulatorValue = 0x05;
            };

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDA_Immediate, accumulatorValue,
					(byte)OpCode.CMP_ZeroPage, randomOffset
				});
			};

			context["given that accumulator value is lower than compared byte"] = () =>
			{
				before = () => { this.memory.WriteZeroPage(randomOffset, 0xff); };
				it["should not set on negative flag"] = () => { processor.Negative.should_be_false(); };
				it["should not set on carry flag"] = () => { processor.Carry.should_be_false(); };
				it["should not set on zero flag"] = () => { processor.Zero.should_be_false(); };
			};

			context["given that accumulator value is equal than compared byte"] = () =>
			{
				before = () => { this.memory.WriteZeroPage(randomOffset, 0x05); };
				it["should not set on negative flag"] = () => { processor.Negative.should_be_false(); };
				it["should set carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should set zero flag"] = () => { processor.Zero.should_be_true(); };
			};

			context["given that accumulator value is greater than compared byte"] = () =>
			{
				before = () => { this.memory.WriteZeroPage(randomOffset, 0x00); };
				it["should not set on negative flag"] = () => { processor.Negative.should_be_false(); };
				it["should set carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should not set on zero flag"] = () => { processor.Zero.should_be_false(); };
			};

			context["given compare result is greater than 0x80"] = () =>
			{
				before = () => 
				{
					accumulatorValue = 0xff;
					this.memory.WriteZeroPage(randomOffset, 0x00);
				};

				it["should set negative flag"] = () => { processor.Negative.should_be_true(); };
				it["should set carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should not set on zero flag"] = () => { processor.Zero.should_be_false(); };
			};
		}

		public void When_use_zero_page_x_addressing_mode()
		{
			var randomOffset = default(byte);
			var accumulatorValue = default(byte);
			var xRegisterValue = default(byte);

			before = () =>
			{
				randomOffset = fixture.Create<byte>();
				xRegisterValue = fixture.Create<byte>();
				accumulatorValue = 0x05;
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDX_Immediate, xRegisterValue,
					(byte)OpCode.LDA_Immediate, accumulatorValue,
					(byte)OpCode.CMP_ZeroPageX, randomOffset
				});
			};

			context["given that accumulator value is lower than compared byte"] = () =>
			{
				before = () => { this.memory.WriteZeroPage((byte)(xRegisterValue + randomOffset), 0xff); };
				it["should not set on negative flag"] = () => { processor.Negative.should_be_false(); };
				it["should not set on carry flag"] = () => { processor.Carry.should_be_false(); };
				it["should not set on zero flag"] = () => { processor.Zero.should_be_false(); };
			};

			context["given that accumulator value is equal than compared byte"] = () =>
			{
				before = () => { this.memory.WriteZeroPage((byte)(xRegisterValue + randomOffset), 0x05); };
				it["should not set on negative flag"] = () => { processor.Negative.should_be_false(); };
				it["should set carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should set zero flag"] = () => { processor.Zero.should_be_true(); };
			};

			context["given that accumulator value is greater than compared byte"] = () =>
			{
				before = () => { this.memory.WriteZeroPage((byte)(xRegisterValue + randomOffset), 0x00); };
				it["should not set on negative flag"] = () => { processor.Negative.should_be_false(); };
				it["should set carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should not set on zero flag"] = () => { processor.Zero.should_be_false(); };
			};

			context["given compare result is greater than 0x80"] = () =>
			{
				before = () =>
				{
					accumulatorValue = 0xff;
					this.memory.WriteZeroPage((byte)(xRegisterValue + randomOffset), 0x00);
				};

				it["should set negative flag"] = () => { processor.Negative.should_be_true(); };
				it["should set carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should not set on zero flag"] = () => { processor.Zero.should_be_false(); };
			};
		}

		public void When_use_absolute_addressing_mode()
		{
			var accumulatorValue = default(byte);
			var randomPage = default(byte);
			var randomOffset = default(byte);

			before = () =>
			{
				accumulatorValue = 0x05;

				randomPage = fixture.Create<byte>();
				randomOffset = fixture.Create<byte>();
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDA_Immediate, accumulatorValue,
					(byte)OpCode.CMP_Absolute, randomOffset, randomPage
				});
			};

			context["given that accumulator value is lower than compared byte"] = () =>
			{
				before = () => { this.memory.Write(randomOffset, randomPage, 0xff); };
				it["should not set on negative flag"] = () => { processor.Negative.should_be_false(); };
				it["should not set on carry flag"] = () => { processor.Carry.should_be_false(); };
				it["should not set on zero flag"] = () => { processor.Zero.should_be_false(); };
			};

			context["given that accumulator value is equal than compared byte"] = () =>
			{
				before = () => { this.memory.Write(randomOffset, randomPage, 0x05); };
				it["should not set on negative flag"] = () => { processor.Negative.should_be_false(); };
				it["should set carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should set zero flag"] = () => { processor.Zero.should_be_true(); };
			};

			context["given that accumulator value is greater than compared byte"] = () =>
			{
				before = () => { this.memory.Write(randomOffset, randomPage, 0x00); };
				it["should not set on negative flag"] = () => { processor.Negative.should_be_false(); };
				it["should set carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should not set on zero flag"] = () => { processor.Zero.should_be_false(); };
			};

			context["given compare result is greater than 0x80"] = () =>
			{
				before = () =>
				{
					accumulatorValue = 0xff;
					this.memory.WriteZeroPage(randomOffset, 0x00);
				};

				it["should set negative flag"] = () => { processor.Negative.should_be_true(); };
				it["should set carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should not set on zero flag"] = () => { processor.Zero.should_be_false(); };
			};
		}

		public void When_use_absolute_x_addressing_mode()
		{
			var accumulatorValue = default(byte);
			var xRegisterValue = default(byte);
			var randomPage = default(byte);
			var randomOffset = default(byte);

			before = () =>
			{
				accumulatorValue = 0x05;

				randomPage = fixture.Create<byte>();
				randomOffset = fixture.Create<byte>();
				xRegisterValue = fixture.Create<byte>();
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDX_Immediate, xRegisterValue,
					(byte)OpCode.LDA_Immediate, accumulatorValue,
					(byte)OpCode.CMP_AbsoluteX, randomOffset, randomPage
				});
			};

			context["given that accumulator value is lower than compared byte"] = () =>
			{
				before = () => { this.memory.Write((byte)(xRegisterValue + randomOffset), randomPage, 0xff); };
				it["should not set on negative flag"] = () => { processor.Negative.should_be_false(); };
				it["should not set on carry flag"] = () => { processor.Carry.should_be_false(); };
				it["should not set on zero flag"] = () => { processor.Zero.should_be_false(); };
			};

			context["given that accumulator value is equal than compared byte"] = () =>
			{
				before = () => { this.memory.Write((byte)(xRegisterValue + randomOffset), randomPage, 0x05); };
				it["should not set on negative flag"] = () => { processor.Negative.should_be_false(); };
				it["should set carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should set zero flag"] = () => { processor.Zero.should_be_true(); };
			};

			context["given that accumulator value is greater than compared byte"] = () =>
			{
				before = () => { this.memory.Write((byte)(xRegisterValue + randomOffset), randomPage, 0x00); };
				it["should not set on negative flag"] = () => { processor.Negative.should_be_false(); };
				it["should set carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should not set on zero flag"] = () => { processor.Zero.should_be_false(); };
			};

			context["given compare result is greater than 0x80"] = () =>
			{
				before = () =>
				{
					accumulatorValue = 0xff;
					this.memory.WriteZeroPage((byte)(xRegisterValue + randomOffset), 0x00);
				};

				it["should set negative flag"] = () => { processor.Negative.should_be_true(); };
				it["should set carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should not set on zero flag"] = () => { processor.Zero.should_be_false(); };
			};
		}

		public void When_use_absolute_y_addressing_mode()
		{
			var accumulatorValue = default(byte);
			var yRegisterValue = default(byte);
			var randomPage = default(byte);
			var randomOffset = default(byte);

			before = () =>
			{
				accumulatorValue = 0x05;

				randomPage = fixture.Create<byte>();
				randomOffset = fixture.Create<byte>();
				yRegisterValue = fixture.Create<byte>();
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDY_Immediate, yRegisterValue,
					(byte)OpCode.LDA_Immediate, accumulatorValue,
					(byte)OpCode.CMP_AbsoluteY, randomOffset, randomPage
				});
			};

			context["given that accumulator value is lower than compared byte"] = () =>
			{
				before = () => { this.memory.Write((byte)(yRegisterValue + randomOffset), randomPage, 0xff); };
				it["should not set on negative flag"] = () => { processor.Negative.should_be_false(); };
				it["should not set on carry flag"] = () => { processor.Carry.should_be_false(); };
				it["should not set on zero flag"] = () => { processor.Zero.should_be_false(); };
			};

			context["given that accumulator value is equal than compared byte"] = () =>
			{
				before = () => { this.memory.Write((byte)(yRegisterValue + randomOffset), randomPage, 0x05); };
				it["should not set on negative flag"] = () => { processor.Negative.should_be_false(); };
				it["should set carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should set zero flag"] = () => { processor.Zero.should_be_true(); };
			};

			context["given that accumulator value is greater than compared byte"] = () =>
			{
				before = () => { this.memory.Write((byte)(yRegisterValue + randomOffset), randomPage, 0x00); };
				it["should not set on negative flag"] = () => { processor.Negative.should_be_false(); };
				it["should set carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should not set on zero flag"] = () => { processor.Zero.should_be_false(); };
			};

			context["given compare result is greater than 0x80"] = () =>
			{
				before = () =>
				{
					accumulatorValue = 0xff;
					this.memory.WriteZeroPage((byte)(yRegisterValue + randomOffset), 0x00);
				};

				it["should set negative flag"] = () => { processor.Negative.should_be_true(); };
				it["should set carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should not set on zero flag"] = () => { processor.Zero.should_be_false(); };
			};
		}

		public void When_use_indexed_indirect_addressing_mode()
		{
			var accumulatorValue = default(byte);
			var xRegisterValue = default(byte);
			var randomOffset = default(byte);

			before = () =>
			{
				accumulatorValue = 0x05;

				randomOffset = fixture.Create<byte>();
				xRegisterValue = fixture.Create<byte>();
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDX_Immediate, xRegisterValue,
					(byte)OpCode.LDA_Immediate, accumulatorValue,
					(byte)OpCode.CMP_IndexedIndirect, randomOffset
				});
			};

			context["given that accumulator value is lower than compared byte"] = () =>
			{
				before = () => { this.memory.MockIndexedIndirectMemoryWrite(randomOffset, xRegisterValue, 0xff); };
				it["should not set on negative flag"] = () => { processor.Negative.should_be_false(); };
				it["should not set on carry flag"] = () => { processor.Carry.should_be_false(); };
				it["should not set on zero flag"] = () => { processor.Zero.should_be_false(); };
			};

			context["given that accumulator value is equal than compared byte"] = () =>
			{
				before = () => { this.memory.MockIndexedIndirectMemoryWrite(randomOffset, xRegisterValue, 0x05); };
				it["should not set on negative flag"] = () => { processor.Negative.should_be_false(); };
				it["should set carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should set zero flag"] = () => { processor.Zero.should_be_true(); };
			};

			context["given that accumulator value is greater than compared byte"] = () =>
			{
				before = () => { this.memory.MockIndexedIndirectMemoryWrite(randomOffset, xRegisterValue, 0x00); };
				it["should not set on negative flag"] = () => { processor.Negative.should_be_false(); };
				it["should set carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should not set on zero flag"] = () => { processor.Zero.should_be_false(); };
			};

			context["given compare result is greater than 0x80"] = () =>
			{
				before = () =>
				{
					accumulatorValue = 0xff;
					this.memory.MockIndexedIndirectMemoryWrite(randomOffset, xRegisterValue, 0x00);
				};

				it["should set negative flag"] = () => { processor.Negative.should_be_true(); };
				it["should set carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should not set on zero flag"] = () => { processor.Zero.should_be_false(); };
			};
		}

		public void When_use_indirect_indexed_addressing_mode()
		{
			var accumulatorValue = default(byte);
			var yRegisterValue = default(byte);
			var randomOffset = default(byte);

			before = () =>
			{
				accumulatorValue = 0x05;

				randomOffset = fixture.Create<byte>();
				yRegisterValue = fixture.Create<byte>();
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDY_Immediate, yRegisterValue,
					(byte)OpCode.LDA_Immediate, accumulatorValue,
					(byte)OpCode.CMP_IndirectIndexed, randomOffset
				});
			};

			context["given that accumulator value is lower than compared byte"] = () =>
			{
				before = () => { this.memory.MockIndirectIndexedMemoryWrite(randomOffset, yRegisterValue, 0xff); };
				it["should not set on negative flag"] = () => { processor.Negative.should_be_false(); };
				it["should not set on carry flag"] = () => { processor.Carry.should_be_false(); };
				it["should not set on zero flag"] = () => { processor.Zero.should_be_false(); };
			};

			context["given that accumulator value is equal than compared byte"] = () =>
			{
				before = () => { this.memory.MockIndirectIndexedMemoryWrite(randomOffset, yRegisterValue, 0x05); };
				it["should not set on negative flag"] = () => { processor.Negative.should_be_false(); };
				it["should set carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should set zero flag"] = () => { processor.Zero.should_be_true(); };
			};

			context["given that accumulator value is greater than compared byte"] = () =>
			{
				before = () => { this.memory.MockIndirectIndexedMemoryWrite(randomOffset, yRegisterValue, 0x00); };
				it["should not set on negative flag"] = () => { processor.Negative.should_be_false(); };
				it["should set carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should not set on zero flag"] = () => { processor.Zero.should_be_false(); };
			};

			context["given compare result is greater than 0x80"] = () =>
			{
				before = () =>
				{
					accumulatorValue = 0xff;
					this.memory.MockIndirectIndexedMemoryWrite(randomOffset, yRegisterValue, 0x00);
				};

				it["should set negative flag"] = () => { processor.Negative.should_be_true(); };
				it["should set carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should not set on zero flag"] = () => { processor.Zero.should_be_false(); };
			};
		}
	}
}
