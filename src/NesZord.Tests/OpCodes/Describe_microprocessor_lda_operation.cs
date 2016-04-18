using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_lda_operation : nspec
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
			var expectedAccumulatorValue = default(byte);

			before = () => { expectedAccumulatorValue = fixture.Create<byte>(); };

			act = () =>
			{
				var operation = (byte)OpCode.LDA_Immediate;
				processor.RunProgram(new byte[] { operation, expectedAccumulatorValue });
			};

			it["should set accumulator with received value"] = () => { processor.Accumulator.Value.should_be(expectedAccumulatorValue); };
		}

		public void When_use_zero_page_addressing_mode()
		{
			var expectedAccumulatorValue = default(byte);
			var randomOffset = default(byte);

			before = () => 
			{
				expectedAccumulatorValue = fixture.Create<byte>();
				randomOffset = fixture.Create<byte>();

				this.memory.WriteZeroPage(randomOffset, expectedAccumulatorValue);
			};

			act = () =>
			{
				var operation = (byte)OpCode.LDA_ZeroPage;
				processor.RunProgram(new byte[] { operation, randomOffset });
			};

			it["should set accumulator with received value"] = () => { processor.Accumulator.Value.should_be(expectedAccumulatorValue); };
		}

		public void When_use_zero_x_page_addressing_mode()
		{
			var expectedAccumulatorValue = default(byte);
			var randomOffset = default(byte);
			var xRegisterValue = default(byte);

			before = () =>
			{
				expectedAccumulatorValue = fixture.Create<byte>();
				randomOffset = fixture.Create<byte>();
				xRegisterValue = fixture.Create<byte>();

				this.memory.WriteZeroPage((byte)(xRegisterValue + randomOffset), expectedAccumulatorValue);
			};

			act = () =>
			{
				processor.RunProgram(new byte[] 
				{
					(byte)OpCode.LDX_Immediate, xRegisterValue,
					(byte)OpCode.LDA_ZeroPageX, randomOffset
				});
			};

			it["should set accumulator with received value"] = () => { processor.Accumulator.Value.should_be(expectedAccumulatorValue); };
		}

		public void When_use_absolute_addressing_mode()
		{
			var expectedAccumulatorValue = default(byte);
			var randomPage = default(byte);
			var randomOffset = default(byte);

			before = () =>
			{
				expectedAccumulatorValue = fixture.Create<byte>();
				randomPage = fixture.Create<byte>();
				randomOffset = fixture.Create<byte>();

				this.memory.Write(randomOffset, randomPage, expectedAccumulatorValue);
			};

			act = () =>
			{
				var operation = (byte)OpCode.LDA_Absolute;
				processor.RunProgram(new byte[] { operation, randomOffset, randomPage });
			};

			it["should set accumulator with received value"] = () => { processor.Accumulator.Value.should_be(expectedAccumulatorValue); };
		}

		public void When_use_absolute_x_addressing_mode()
		{
			var expectedAccumulatorValue = default(byte);
			var randomPage = default(byte);
			var randomOffset = default(byte);
			var xRegisterValue = default(byte);

			before = () =>
			{
				expectedAccumulatorValue = fixture.Create<byte>();
				randomPage = fixture.Create<byte>();
				randomOffset = fixture.Create<byte>();
				xRegisterValue = fixture.Create<byte>();

				this.memory.Write((byte)(randomOffset + xRegisterValue), randomPage, expectedAccumulatorValue);
			};

			act = () =>
			{
				processor.RunProgram(new byte[] 
				{
					(byte)OpCode.LDX_Immediate, xRegisterValue,
					(byte)OpCode.LDA_AbsoluteX, randomOffset, randomPage
				});
			};

			it["should set accumulator with received value"] = () => { processor.Accumulator.Value.should_be(expectedAccumulatorValue); };
		}

		public void When_use_absolute_y_addressing_mode()
		{
			var expectedAccumulatorValue = default(byte);
			var randomPage = default(byte);
			var randomOffset = default(byte);
			var yRegisterValue = default(byte);

			before = () =>
			{
				expectedAccumulatorValue = fixture.Create<byte>();
				randomPage = fixture.Create<byte>();
				randomOffset = fixture.Create<byte>();
				yRegisterValue = fixture.Create<byte>();

				this.memory.Write((byte)(randomOffset + yRegisterValue), randomPage, expectedAccumulatorValue);
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDY_Immediate, yRegisterValue,
					(byte)OpCode.LDA_AbsoluteY, randomOffset, randomPage
				});
			};

			it["should set accumulator with received value"] = () => { processor.Accumulator.Value.should_be(expectedAccumulatorValue); };
		}

		public void When_use_indexed_indirect_addressing_mode()
		{
			var expectedAccumulatorValue = default(byte);
			var randomOffset = default(byte);
			var xRegisterValue = default(byte);

			before = () =>
			{
				expectedAccumulatorValue = fixture.Create<byte>();
				randomOffset = fixture.Create<byte>();
				xRegisterValue = fixture.Create<byte>();

				this.memory.MockIndexedIndirectMemoryWrite(randomOffset, xRegisterValue, expectedAccumulatorValue);
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDX_Immediate, xRegisterValue,
					(byte)OpCode.LDA_IndexedIndirect, randomOffset
				});
			};

			it["should set accumulator with received value"] = () => { processor.Accumulator.Value.should_be(expectedAccumulatorValue); };
		}

		public void When_use_indirect_indexed_addressing_mode()
		{
			var expectedAccumulatorValue = default(byte);
			var randomOffset = default(byte);
			var yRegisterValue = default(byte);

			before = () =>
			{
				expectedAccumulatorValue = fixture.Create<byte>();
				randomOffset = fixture.Create<byte>();
				yRegisterValue = fixture.Create<byte>();

				this.memory.MockIndirectIndexedMemoryWrite(randomOffset, yRegisterValue, expectedAccumulatorValue);
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDY_Immediate, yRegisterValue,
					(byte)OpCode.LDA_IndirectIndexed, randomOffset
				});
			};

			it["should set accumulator with received value"] = () => { processor.Accumulator.Value.should_be(expectedAccumulatorValue); };
		}
	}
}