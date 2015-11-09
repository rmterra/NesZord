using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_sta_operation : nspec
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
					(byte)OpCode.ImmediateLDA, fixture.Create<byte>(),
					(byte)OpCode.ZeroPageSTA, randomOffset
				});
			};

			it["should store the accumulator value at memory"] = () => { this.memory.Read(randomOffset, Memory.ZERO_PAGE).should_be(processor.Accumulator); };
		}

		public void When_use_zero_page_x_mode()
		{
			var randomOffset = default(byte);

			before = () => { randomOffset = fixture.Create<byte>(); };

			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte)OpCode.ImmediateLDX, fixture.Create<byte>(),
					(byte)OpCode.ImmediateLDA, fixture.Create<byte>(),
					(byte)OpCode.ZeroPageXSTA, randomOffset
				});
			};

			it["should store the accumulator value at memory"] = () =>
			{
				var offset = (byte)(randomOffset + processor.X);
				this.memory.Read(offset, Memory.ZERO_PAGE).should_be(processor.Accumulator);
			};
		}

		public void When_use_absolute_addressing_mode()
		{
			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte)OpCode.ImmediateLDA, fixture.Create<byte>(),
					(byte)OpCode.AbsoluteSTA, 0x00, 0x20
				});
			};

			it["should store the accumulator value at memory"] = () =>
			{
				this.memory.Read(0x00, 0x20).should_be(processor.Accumulator);
			};
		}

		public void When_use_absolute_y_addressing_mode()
		{
			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte)OpCode.ImmediateLDY, fixture.Create<byte>(),
					(byte)OpCode.ImmediateLDA, fixture.Create<byte>(),
					(byte)OpCode.AbsoluteYSTA, 0x00, 0x20
				});
			};

			it["should store the accumulator value at memory"] = () =>
			{
				this.memory.Read(processor.Y, 0x20).should_be(processor.Accumulator);
			};
		}

		public void When_use_absolute_x_addressing_mode()
		{
			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte)OpCode.ImmediateLDX, fixture.Create<byte>(),
					(byte)OpCode.ImmediateLDA, fixture.Create<byte>(),
					(byte)OpCode.AbsoluteXSTA, 0x00, 0x20
				});
			};

			it["should store the accumulator value at memory"] = () =>
			{
				this.memory.Read(processor.X, 0x20).should_be(processor.Accumulator);
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
					(byte)OpCode.ImmediateLDX, xRegisterValue,
					(byte)OpCode.ImmediateLDA, fixture.Create<byte>(),
					(byte)OpCode.IndexedIndirectSTA, randomOffset
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
					(byte)OpCode.ImmediateLDY, yRegisterValue,
					(byte)OpCode.ImmediateLDA, fixture.Create<byte>(),
					(byte)OpCode.IndirectIndexedSTA, randomOffset
				});
			};

			it["should store the accumulator value at memory"] = () =>
			{
				this.memory.Read(indirectLocation).should_be(processor.Accumulator);
			};
		}
	}
}