using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_sty_operation : nspec
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
					(byte)OpCode.LDY_Immediate, fixture.Create<byte>(),
					(byte)OpCode.STY_ZeroPage, randomOffset
				});
			};

			it["should store the y register value at memory"] = () => { this.memory.Read(randomOffset, Memory.ZERO_PAGE).should_be(processor.Y.Value); };
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
					(byte)OpCode.LDY_Immediate, fixture.Create<byte>(),
					(byte)OpCode.STY_ZeroPageX, randomOffset
				});
			};

			it["should store the y register value at memory"] = () =>
			{
				var offset = (byte)(randomOffset + processor.X.Value);
				this.memory.Read(offset, Memory.ZERO_PAGE).should_be(processor.Y.Value);
			};
		}

		public void When_use_absolute_addressing_mode()
		{
			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDY_Immediate, fixture.Create<byte>(),
					(byte)OpCode.STY_Absolute, 0x00, 0x20
				});
			};

			it["should store the y register value at memory"] = () =>
			{
				this.memory.Read(0x00, 0x20).should_be(processor.Y.Value);
			};
		}
	}
}