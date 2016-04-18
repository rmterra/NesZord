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
					(byte)OpCode.LDA_Immediate, fixture.Create<byte>(),
					(byte)OpCode.STA_ZeroPage, randomOffset
				});
			};

			it["should store the accumulator value at memory"] = () => { this.memory.Read(randomOffset, Memory.ZERO_PAGE).should_be(processor.Accumulator.Value); };
		}

		public void When_use_zero_page_x_mode()
		{
			var randomOffset = default(byte);

			before = () => { randomOffset = fixture.Create<byte>(); };

			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDX_Immediate, fixture.Create<byte>(),
					(byte)OpCode.LDA_Immediate, fixture.Create<byte>(),
					(byte)OpCode.STA_ZeroPageX, randomOffset
				});
			};

			it["should store the accumulator value at memory"] = () =>
			{
				var offset = (byte)(randomOffset + processor.X);
				this.memory.Read(offset, Memory.ZERO_PAGE).should_be(processor.Accumulator.Value);
			};
		}

		public void When_use_absolute_addressing_mode()
		{
			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDA_Immediate, fixture.Create<byte>(),
					(byte)OpCode.STA_Absolute, 0x00, 0x20
				});
			};

			it["should store the accumulator value at memory"] = () =>
			{
				this.memory.Read(0x00, 0x20).should_be(processor.Accumulator.Value);
			};
		}

		public void When_use_absolute_y_addressing_mode()
		{
			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDY_Immediate, fixture.Create<byte>(),
					(byte)OpCode.LDA_Immediate, fixture.Create<byte>(),
					(byte)OpCode.STA_AbsoluteY, 0x00, 0x20
				});
			};

			it["should store the accumulator value at memory"] = () =>
			{
				this.memory.Read(processor.Y.Value, 0x20).should_be(processor.Accumulator.Value);
			};
		}

		public void When_use_absolute_x_addressing_mode()
		{
			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDX_Immediate, fixture.Create<byte>(),
					(byte)OpCode.LDA_Immediate, fixture.Create<byte>(),
					(byte)OpCode.STA_AbsoluteX, 0x00, 0x20
				});
			};

			it["should store the accumulator value at memory"] = () =>
			{
				this.memory.Read(processor.X, 0x20).should_be(processor.Accumulator.Value);
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
					(byte)OpCode.LDX_Immediate, xRegisterValue,
					(byte)OpCode.LDA_Immediate, fixture.Create<byte>(),
					(byte)OpCode.STA_IndexedIndirect, randomOffset
				});
			};

			it["should store the accumulator value at memory"] = () =>
			{
				this.memory.Read(indirectLocation).should_be(processor.Accumulator.Value);
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
					(byte)OpCode.LDY_Immediate, yRegisterValue,
					(byte)OpCode.LDA_Immediate, fixture.Create<byte>(),
					(byte)OpCode.STA_IndirectIndexed, randomOffset
				});
			};

			it["should store the accumulator value at memory"] = () =>
			{
				this.memory.Read(indirectLocation).should_be(processor.Accumulator.Value);
			};
		}
	}
}