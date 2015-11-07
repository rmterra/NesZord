using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_store_accumulator_into_memory_operation : nspec
	{
		private static readonly Fixture fixture = new Fixture();

		private MemoryMock memory;

		private Microprocessor processor;

		public void before_each()
		{
			this.memory = new MemoryMock();
			this.processor = new Microprocessor(this.memory);
		}

		public void When_use_zero_page_mode()
		{
			var randomOffset = default(byte);

			before = () => { randomOffset = fixture.Create<byte>(); };

			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte)OpCode.ImmediateLoadAccumulator, fixture.Create<byte>(),
					(byte)OpCode.ZeroPageStoreAccumulator, randomOffset
				});
			};

			it["should store the accumulator value at memory"] = () => { this.memory.Read(0x00, randomOffset).should_be(processor.Accumulator); };
		}

		public void When_use_zero_page_x_mode()
		{
			var randomOffset = default(byte);

			before = () => { randomOffset = fixture.Create<byte>(); };

			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte)OpCode.ImmediateLoadXRegister, fixture.Create<byte>(),
					(byte)OpCode.ImmediateLoadAccumulator, fixture.Create<byte>(),
					(byte)OpCode.ZeroPageXStoreAccumulator, randomOffset
				});
			};

			it["should store the accumulator value at memory"] = () =>
			{
				var offset = (byte)(randomOffset + processor.X);
				this.memory.Read(0x00, offset).should_be(processor.Accumulator);
			};
		}

		public void When_use_absolute_addressing_mode()
		{
			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte)OpCode.ImmediateLoadAccumulator, fixture.Create<byte>(),
					(byte)OpCode.AbsoluteStoreAccumulator, 0x00, 0x20
				});
			};

			it["should store the accumulator value at memory"] = () =>
			{
				this.memory.Read(0x20, 0x00).should_be(processor.Accumulator);
			};
		}

		public void When_use_absolute_y_addressing_mode()
		{
			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte)OpCode.ImmediateLoadYRegister, fixture.Create<byte>(),
					(byte)OpCode.ImmediateLoadAccumulator, fixture.Create<byte>(),
					(byte)OpCode.AbsoluteYStoreAccumulator, 0x00, 0x20
				});
			};

			it["should store the accumulator value at memory"] = () =>
			{
				this.memory.Read(0x20, processor.Y).should_be(processor.Accumulator);
			};
		}

		public void When_use_absolute_x_addressing_mode()
		{
			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte)OpCode.ImmediateLoadXRegister, fixture.Create<byte>(),
					(byte)OpCode.ImmediateLoadAccumulator, fixture.Create<byte>(),
					(byte)OpCode.AbsoluteXStoreAccumulator, 0x00, 0x20
				});
			};

			it["should store the accumulator value at memory"] = () =>
			{
				this.memory.Read(0x20, processor.X).should_be(processor.Accumulator);
			};
		}

		public void When_use_indexed_indirect_mode()
		{
			var randomOffset = default(byte);
			var xRegisterValue = default(byte);
			var indirectLocation = default(MemoryLocation);

			before = () =>
			{
				randomOffset = fixture.Create<byte>();
				xRegisterValue = fixture.Create<byte>();
				indirectLocation = this.memory.GetIndexedIndirectLocation(randomOffset, xRegisterValue);
			};

			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte)OpCode.ImmediateLoadXRegister, xRegisterValue,
					(byte)OpCode.ImmediateLoadAccumulator, fixture.Create<byte>(),
					(byte)OpCode.IndexedIndirectStoreAccumulator, randomOffset
				});
			};

			it["should store the accumulator value at memory"] = () =>
			{
				this.memory.Read(indirectLocation).should_be(processor.Accumulator);
			};
		}

		public void When_use_indirect_indexed_mode()
		{
			var randomOffset = default(byte);
			var yRegisterValue = default(byte);
			var indirectLocation = default(MemoryLocation);

			before = () =>
			{
				randomOffset = fixture.Create<byte>();
				yRegisterValue = fixture.Create<byte>();
				indirectLocation = this.memory.GetIndirectIndexedLocation(randomOffset, yRegisterValue);
			};

			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte)OpCode.ImmediateLoadYRegister, yRegisterValue,
					(byte)OpCode.ImmediateLoadAccumulator, fixture.Create<byte>(),
					(byte)OpCode.IndirectIndexedStoreAccumulator, randomOffset
				});
			};

			it["should store the accumulator value at memory"] = () =>
			{
				this.memory.Read(indirectLocation).should_be(processor.Accumulator);
			};
		}
	}
}