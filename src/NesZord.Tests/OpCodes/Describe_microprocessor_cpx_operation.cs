using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_cpx_operation : nspec
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
			var xRegisterValue = default(byte);
			var byteToCompare = default(byte);

			before = () => { xRegisterValue = 0x05; };

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDX_Immediate, xRegisterValue,
					(byte)OpCode.CPX_Immediate, byteToCompare
				});
			};

			context["given that x register value is lower than compared byte"] = () =>
			{
				before = () => { byteToCompare = 0xff; };
				it["should not set on negative flag"] = () => { processor.Negative.should_be_false(); };
				it["should not set on carry flag"] = () => { processor.Carry.should_be_false(); };
				it["should not set on zero flag"] = () => { processor.Zero.should_be_false(); };
			};

			context["given that x register value is equal than compared byte"] = () =>
			{
				before = () => { byteToCompare = 0x05; };
				it["should not set on negative flag"] = () => { processor.Negative.should_be_false(); };
				it["should set carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should set zero flag"] = () => { processor.Zero.should_be_true(); };
			};

			context["given that x register value is greater than compared byte"] = () =>
			{
				before = () => { byteToCompare = 0x00; };
				it["should not set on negative flag"] = () => { processor.Negative.should_be_false(); };
				it["should set carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should not set on zero flag"] = () => { processor.Zero.should_be_false(); };
			};

			context["given compare result is greater than 0x80"] = () =>
			{
				before = () =>
				{
					xRegisterValue = 0xff;
					byteToCompare = 0x00;
				};

				it["should set negative flag"] = () => { processor.Negative.should_be_true(); };
				it["should set carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should not set on zero flag"] = () => { processor.Zero.should_be_false(); };
			};
		}

		public void When_use_zero_page_addressing_mode()
		{
			var randomOffset = default(byte);
			var xRegisterValue = default(byte);

			before = () => 
			{
				randomOffset = fixture.Create<byte>();
				xRegisterValue = 0x05;
            };

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDX_Immediate, xRegisterValue,
					(byte)OpCode.CPX_ZeroPage, randomOffset
				});
			};

			context["given that x register value is lower than compared byte"] = () =>
			{
				before = () => { this.memory.WriteZeroPage(randomOffset, 0xff); };
				it["should not set on negative flag"] = () => { processor.Negative.should_be_false(); };
				it["should not set on carry flag"] = () => { processor.Carry.should_be_false(); };
				it["should not set on zero flag"] = () => { processor.Zero.should_be_false(); };
			};

			context["given that x register value is equal than compared byte"] = () =>
			{
				before = () => { this.memory.WriteZeroPage(randomOffset, 0x05); };
				it["should not set on negative flag"] = () => { processor.Negative.should_be_false(); };
				it["should set carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should set zero flag"] = () => { processor.Zero.should_be_true(); };
			};

			context["given that x register value is greater than compared byte"] = () =>
			{
				before = () => { this.memory.WriteZeroPage(randomOffset, 0x00); };
				it["should not set on negative flag"] = () => { processor.Negative.should_be_false(); };
				it["should set carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should not set on zero flag"] = () => { processor.Zero.should_be_false(); };
			};

			context["given compare result is greater than 0x80"] = () =>
			{
				before = () => 
				{
					xRegisterValue = 0xff;
					this.memory.WriteZeroPage(randomOffset, 0x00);
				};

				it["should set negative flag"] = () => { processor.Negative.should_be_true(); };
				it["should set carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should not set on zero flag"] = () => { processor.Zero.should_be_false(); };
			};
		}

		public void When_use_absolute_addressing_mode()
		{
			var xRegisterValue = default(byte);
			var randomPage = default(byte);
			var randomOffset = default(byte);

			before = () =>
			{
				xRegisterValue = 0x05;

				randomPage = fixture.Create<byte>();
				randomOffset = fixture.Create<byte>();
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDX_Immediate, xRegisterValue,
					(byte)OpCode.CPX_Absolute, randomOffset, randomPage
				});
			};

			context["given that x register value is lower than compared byte"] = () =>
			{
				before = () => { this.memory.Write(randomOffset, randomPage, 0xff); };
				it["should not set on negative flag"] = () => { processor.Negative.should_be_false(); };
				it["should not set on carry flag"] = () => { processor.Carry.should_be_false(); };
				it["should not set on zero flag"] = () => { processor.Zero.should_be_false(); };
			};

			context["given that x register value is equal than compared byte"] = () =>
			{
				before = () => { this.memory.Write(randomOffset, randomPage, 0x05); };
				it["should not set on negative flag"] = () => { processor.Negative.should_be_false(); };
				it["should set carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should set zero flag"] = () => { processor.Zero.should_be_true(); };
			};

			context["given that x register value is greater than compared byte"] = () =>
			{
				before = () => { this.memory.Write(randomOffset, randomPage, 0x00); };
				it["should not set on negative flag"] = () => { processor.Negative.should_be_false(); };
				it["should set carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should not set on zero flag"] = () => { processor.Zero.should_be_false(); };
			};

			context["given compare result is greater than 0x80"] = () =>
			{
				before = () =>
				{
					xRegisterValue = 0xff;
					this.memory.WriteZeroPage(randomOffset, 0x00);
				};

				it["should set negative flag"] = () => { processor.Negative.should_be_true(); };
				it["should set carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should not set on zero flag"] = () => { processor.Zero.should_be_false(); };
			};
		}
	}
}
