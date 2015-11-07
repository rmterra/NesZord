using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_store_x_register_into_memory_operation : nspec
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
					(byte)OpCode.ImmediateLoadXRegister, fixture.Create<byte>(),
					(byte)OpCode.ZeroPageStoreXRegister, randomOffset
				});
			};

			it["should store the x register value at memory"] = () => { this.memory.Read(0x00, randomOffset).should_be(processor.X); };
		}

		public void When_use_zero_page_y_mode()
		{
			var randomOffset = default(byte);

			before = () => { randomOffset = fixture.Create<byte>(); };

			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte)OpCode.ImmediateLoadYRegister, fixture.Create<byte>(),
					(byte)OpCode.ImmediateLoadXRegister, fixture.Create<byte>(),
					(byte)OpCode.ZeroPageYStoreXRegister, randomOffset
				});
			};

			it["should store the x register value at memory"] = () =>
			{
				var offset = (byte)(randomOffset + processor.Y);
				this.memory.Read(0x00, offset).should_be(processor.X);
			};
		}

		public void When_use_absolute_addressing_mode()
		{
			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte)OpCode.ImmediateLoadXRegister, fixture.Create<byte>(),
					(byte)OpCode.AbsoluteStoreXRegister, 0x00, 0x20
				});
			};

			it["should store the x register value at memory"] = () =>
			{
				this.memory.Read(0x20, 0x00).should_be(processor.X);
			};
		}
	}
}