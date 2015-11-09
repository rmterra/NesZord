using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_compare_x_register_with_memory_operation : nspec
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
			var byteToCompare = default(byte);

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.ImmediateLoadXRegister, 0x05,
					(byte)OpCode.ImmediateCompareXRegister, byteToCompare
				});
			};

			context["given that x register value is lower than compared byte"] = () =>
			{
				before = () => { byteToCompare = 0xff; };
				it["should not turn on carry flag"] = () => { processor.Carry.should_be_false(); };
				it["should not turn on zero flag"] = () => { processor.Zero.should_be_false(); };
			};

			context["given that x register value is equal than compared byte"] = () =>
			{
				before = () => { byteToCompare = 0x05; };
				it["should turn on carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should turn on zero flag"] = () => { processor.Zero.should_be_true(); };
			};

			context["given that x register value is grater than compared byte"] = () =>
			{
				before = () => { byteToCompare = 0x00; };
				it["should turn on carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should not turn on zero flag"] = () => { processor.Zero.should_be_false(); };
			};
		}

		public void When_use_zero_page_addressing_mode()
		{
			var randomOffset = default(byte);

			before = () => { randomOffset = fixture.Create<byte>(); };

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.ImmediateLoadXRegister, 0x05,
					(byte)OpCode.ZeroPageCompareXRegister, randomOffset
				});
			};

			context["given that x register value is lower than compared byte"] = () =>
			{
				before = () => { this.memory.WriteZeroPage(randomOffset, 0xff); };
				it["should not turn on carry flag"] = () => { processor.Carry.should_be_false(); };
				it["should not turn on zero flag"] = () => { processor.Zero.should_be_false(); };
			};

			context["given that x register value is equal than compared byte"] = () =>
			{
				before = () => { this.memory.WriteZeroPage(randomOffset, 0x05); };
				it["should turn on carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should turn on zero flag"] = () => { processor.Zero.should_be_true(); };
			};

			context["given that x register value is grater than compared byte"] = () =>
			{
				before = () => { this.memory.WriteZeroPage(randomOffset, 0x00); };
				it["should turn on carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should not turn on zero flag"] = () => { processor.Zero.should_be_false(); };
			};
		}

		public void When_use_absolute_addressing_mode()
		{
			var randomPage = default(byte);
			var randomOffset = default(byte);

			before = () =>
			{
				randomPage = fixture.Create<byte>();
				randomOffset = fixture.Create<byte>();
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.ImmediateLoadXRegister, 0x05,
					(byte)OpCode.AbsoluteCompareXRegister, randomOffset, randomPage
				});
			};

			context["given that x register value is lower than compared byte"] = () =>
			{
				before = () => { this.memory.Write(randomOffset, randomPage, 0xff); };
				it["should not turn on carry flag"] = () => { processor.Carry.should_be_false(); };
				it["should not turn on zero flag"] = () => { processor.Zero.should_be_false(); };
			};

			context["given that x register value is equal than compared byte"] = () =>
			{
				before = () => { this.memory.Write(randomOffset, randomPage, 0x05); };
				it["should turn on carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should turn on zero flag"] = () => { processor.Zero.should_be_true(); };
			};

			context["given that x register value is grater than compared byte"] = () =>
			{
				before = () => { this.memory.Write(randomOffset, randomPage, 0x00); };
				it["should turn on carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should not turn on zero flag"] = () => { processor.Zero.should_be_false(); };
			};
		}
	}
}
