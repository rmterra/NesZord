using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesZord.Tests
{
	public class Describe_microprocessor_store_operations : nspec
	{
		private static readonly Fixture fixture = new Fixture();

		private Memory memory;

		private Microprocessor processor;

		public void before_each()
		{
			this.memory = new Memory();
			this.processor = new Microprocessor(this.memory);
		}

		public void When_store_the_value_at_y_register_into_memory_with_absolute_addressing_mode()
		{
			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte)OpCode.ImmediateLoadYRegister, fixture.Create<byte>(),
					(byte)OpCode.AbsoluteStoreYRegister, 0x00, 0x20
				});
			};

			it["should store the y register value at $0200"] = () =>
			{
				this.memory.Read(0x20, 0x00).should_be(processor.Y);
			};
		}

		public void When_store_the_value_at_accumulator_into_memory_with_absolute_addressing_mode()
		{
			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte)OpCode.ImmediateLoadAccumulator, fixture.Create<byte>(),
					(byte)OpCode.AbsoluteStoreAccumulator, 0x00, 0x20
				});
			};

			it["should store the accumulator value at $0200"] = () =>
			{
				this.memory.Read(0x20, 0x00).should_be(processor.Accumulator);
			};
		}

		public void When_store_the_value_at_accumulator_into_memory_with_absolute_y_addressing_mode()
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

			it["should store the accumulator value at $0200 + Y register as offset"] = () =>
			{
				this.memory.Read(0x20, processor.Y).should_be(processor.Accumulator);
			};
		}

		public void When_store_the_value_at_accumulator_into_memory_with_absolute_x_addressing_mode()
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

			it["should store the accumulator value at $0200 + X register as offset"] = () =>
			{
				this.memory.Read(0x20, processor.X).should_be(processor.Accumulator);
			};
		}

		public void When_store_the_value_at_x_register_into_memory_with_absolute_addressing_mode()
		{
			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte)OpCode.ImmediateLoadXRegister, fixture.Create<byte>(),
					(byte)OpCode.AbsoluteStoreXRegister, 0x00, 0x20
				});
			};

			it["should store the x register value at $0200"] = () =>
			{
				this.memory.Read(0x20, 0x00).should_be(processor.X);
			};
		}
	}
}