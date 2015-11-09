using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_stx_operation : nspec
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
					(byte)OpCode.ImmediateLDX, fixture.Create<byte>(),
					(byte)OpCode.ZeroPageSTX, randomOffset
				});
			};

			it["should store the x register value at memory"] = () => { this.memory.Read(randomOffset, Memory.ZERO_PAGE).should_be(processor.X); };
		}

		public void When_use_zero_page_y_mode()
		{
			var randomOffset = default(byte);

			before = () => { randomOffset = fixture.Create<byte>(); };

			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte)OpCode.ImmediateLDY, fixture.Create<byte>(),
					(byte)OpCode.ImmediateLDX, fixture.Create<byte>(),
					(byte)OpCode.ZeroPageYSTX, randomOffset
				});
			};

			it["should store the x register value at memory"] = () =>
			{
				var offset = (byte)(randomOffset + processor.Y);
				this.memory.Read(offset, Memory.ZERO_PAGE).should_be(processor.X);
			};
		}

		public void When_use_absolute_addressing_mode()
		{
			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte)OpCode.ImmediateLDX, fixture.Create<byte>(),
					(byte)OpCode.AbsoluteSTX, 0x00, 0x20
				});
			};

			it["should store the x register value at memory"] = () =>
			{
				this.memory.Read(0x00, 0x20).should_be(processor.X);
			};
		}
	}
}