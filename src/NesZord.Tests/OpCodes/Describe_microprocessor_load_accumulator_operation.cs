using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_load_accumulator_operation : nspec
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
				var operation = (byte)OpCode.ImmediateLoadAccumulator;
				processor.RunProgram(new byte[] { operation, expectedAccumulatorValue });
			};

			it["should set accumulator with received value"] = () => { processor.Accumulator.should_be(expectedAccumulatorValue); };
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
				var operation = (byte)OpCode.ZeroPageLoadAccumulator;
				processor.RunProgram(new byte[] { operation, randomOffset });
			};

			it["should set accumulator with received value"] = () => { processor.Accumulator.should_be(expectedAccumulatorValue); };
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
					(byte)OpCode.ImmediateLoadXRegister, xRegisterValue,
					(byte)OpCode.ZeroPageXLoadAccumulator, randomOffset
				});
			};

			it["should set accumulator with received value"] = () => { processor.Accumulator.should_be(expectedAccumulatorValue); };
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

				this.memory.Write(randomPage, randomOffset, expectedAccumulatorValue);
			};

			act = () =>
			{
				var operation = (byte)OpCode.AbsoluteLoadAccumulator;
				processor.RunProgram(new byte[] { operation, randomOffset, randomPage });
			};

			it["should set accumulator with received value"] = () => { processor.Accumulator.should_be(expectedAccumulatorValue); };
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

				this.memory.Write(randomPage, (byte)(randomOffset + xRegisterValue), expectedAccumulatorValue);
			};

			act = () =>
			{
				processor.RunProgram(new byte[] 
				{
					(byte)OpCode.ImmediateLoadXRegister, xRegisterValue,
					(byte)OpCode.AbsoluteXLoadAccumulator, randomOffset, randomPage
				});
			};

			it["should set accumulator with received value"] = () => { processor.Accumulator.should_be(expectedAccumulatorValue); };
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

				this.memory.Write(randomPage, (byte)(randomOffset + yRegisterValue), expectedAccumulatorValue);
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.ImmediateLoadYRegister, yRegisterValue,
					(byte)OpCode.AbsoluteYLoadAccumulator, randomOffset, randomPage
				});
			};

			it["should set accumulator with received value"] = () => { processor.Accumulator.should_be(expectedAccumulatorValue); };
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
					(byte)OpCode.ImmediateLoadXRegister, xRegisterValue,
					(byte)OpCode.IndexedIndirectLoadAccumulator, randomOffset
				});
			};

			it["should set accumulator with received value"] = () => { processor.Accumulator.should_be(expectedAccumulatorValue); };
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
					(byte)OpCode.ImmediateLoadYRegister, yRegisterValue,
					(byte)OpCode.IndirectIndexedLoadAccumulator, randomOffset
				});
			};

			it["should set accumulator with received value"] = () => { processor.Accumulator.should_be(expectedAccumulatorValue); };
		}
	}
}