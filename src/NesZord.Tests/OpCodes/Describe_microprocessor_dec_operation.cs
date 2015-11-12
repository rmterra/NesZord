using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;
using System;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_dec_operation : nspec
	{
		private static readonly Fixture fixture = new Fixture();

		private MemoryMock memory;

		private Microprocessor processor;

		public void before_each()
		{
			this.memory = new MemoryMock();
			this.processor = new Microprocessor(this.memory);
		}

		public void When_use_zero_page_addressing_mode()
		{
			var expectedValue = default(byte);
			var randomOffset = default(byte);

			before = () => 
			{
				expectedValue = fixture.Create<byte>();
				randomOffset = fixture.Create<byte>();

				this.memory.WriteZeroPage(randomOffset, (byte)(expectedValue + 1));
			};

			act = () =>
			{
				processor.RunProgram(new byte[] { (byte)OpCode.DEC_ZeroPage, randomOffset });
			};

			it["should decrement 1 to memory value"] = () => 
			{
				this.memory.Read(randomOffset, Memory.ZERO_PAGE).should_be(expectedValue);
			};

			context["given that memory value is 0x01"] = () => 
			{
				before = () =>
				{
					expectedValue = 0x00;
					this.memory.WriteZeroPage(randomOffset, (byte)(expectedValue + 1));
				};

				it["should set zero flag"] = () => { processor.Zero.should_be_true(); };

				it["should not set negative flag"] = () => { processor.Negative.should_be_false(); };
			};

			context["given that final memory value sign bit is 1"] = () =>
			{
				before = () =>
				{
					expectedValue = (byte)new Random().Next(0x80, 0xff);
					this.memory.WriteZeroPage(randomOffset, (byte)(expectedValue + 1));
				};

				it["should not set zero flag"] = () => { processor.Zero.should_be_false(); };

				it["should set negative flag"] = () => { processor.Negative.should_be_true(); };
			};
		}

		public void When_use_zero_page_x_addressing_mode()
		{
			var expectedValue = default(byte);
			var randomOffset = default(byte);
			var xRegisterValue = default(byte);

			before = () =>
			{
				expectedValue = fixture.Create<byte>();
				randomOffset = fixture.Create<byte>();
				xRegisterValue = fixture.Create<byte>();

				this.memory.WriteZeroPage((byte)(xRegisterValue + randomOffset), (byte)(expectedValue + 1));
			};

			act = () =>
			{
				processor.RunProgram(new byte[] 
				{
					(byte)OpCode.LDX_Immediate, xRegisterValue,
					(byte)OpCode.DEC_ZeroPageX, randomOffset
				});
			};

			it["should decrement 1 to memory value"] = () =>
			{
				this.memory.Read((byte)(xRegisterValue + randomOffset), Memory.ZERO_PAGE).should_be(expectedValue);
			};

			context["given that memory value is 0x01"] = () =>
			{
				before = () =>
				{
					expectedValue = 0x00;
					this.memory.WriteZeroPage((byte)(xRegisterValue + randomOffset), (byte)(expectedValue + 1));
				};

				it["should set zero flag"] = () => { processor.Zero.should_be_true(); };

				it["should not set negative flag"] = () => { processor.Negative.should_be_false(); };
			};

			context["given that final memory value sign bit is 1"] = () =>
			{
				before = () =>
				{
					expectedValue = (byte)new Random().Next(0x80, 0xff);
					this.memory.WriteZeroPage((byte)(xRegisterValue + randomOffset), (byte)(expectedValue + 1));
				};

				it["should not set zero flag"] = () => { processor.Zero.should_be_false(); };

				it["should set negative flag"] = () => { processor.Negative.should_be_true(); };
			};
		}

		public void When_use_absolute_addressing_mode()
		{
			var expectedValue = default(byte);
			var randomOffset = default(byte);
			var randomPage = default(byte);

			before = () =>
			{
				expectedValue = fixture.Create<byte>();
				randomOffset = fixture.Create<byte>();
				randomPage = fixture.Create<byte>();

				this.memory.Write(randomOffset, randomPage, (byte)(expectedValue + 1));
			};

			act = () =>
			{
				processor.RunProgram(new byte[] { (byte)OpCode.DEC_Absolute, randomOffset, randomPage });
			};

			it["should decrement 1 to memory value"] = () =>
			{
				this.memory.Read(randomOffset, randomPage).should_be(expectedValue);
			};

			context["given that memory value is 0x01"] = () =>
			{
				before = () =>
				{
					expectedValue = 0x00;
					this.memory.Write(randomOffset, randomPage, (byte)(expectedValue + 1));
				};

				it["should set zero flag"] = () => { processor.Zero.should_be_true(); };

				it["should not set negative flag"] = () => { processor.Negative.should_be_false(); };
			};

			context["given that final memory value sign bit is 1"] = () =>
			{
				before = () =>
				{
					expectedValue = (byte)new Random().Next(0x80, 0xff);
					this.memory.Write(randomOffset, randomPage, (byte)(expectedValue + 1));
				};

				it["should not set zero flag"] = () => { processor.Zero.should_be_false(); };

				it["should set negative flag"] = () => { processor.Negative.should_be_true(); };
			};
		}

		public void When_use_absolute_x_addressing_mode()
		{
			var expectedValue = default(byte);
			var randomOffset = default(byte);
			var randomPage = default(byte);
			var xRegisterValue = default(byte);

			before = () =>
			{
				expectedValue = fixture.Create<byte>();
				randomOffset = fixture.Create<byte>();
				randomPage = fixture.Create<byte>();
				xRegisterValue = fixture.Create<byte>();

				this.memory.Write((byte)(xRegisterValue + randomOffset), randomPage, (byte)(expectedValue + 1));
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDX_Immediate, xRegisterValue,
					(byte)OpCode.DEC_AbsoluteX, randomOffset, randomPage
				});
			};

			it["should decrement 1 to memory value"] = () =>
			{
				this.memory.Read((byte)(xRegisterValue + randomOffset), randomPage).should_be(expectedValue);
			};

			context["given that memory value is 0x01"] = () =>
			{
				before = () =>
				{
					expectedValue = 0x00;
					this.memory.Write((byte)(xRegisterValue + randomOffset), randomPage, (byte)(expectedValue + 1));
				};

				it["should set zero flag"] = () => { processor.Zero.should_be_true(); };

				it["should not set negative flag"] = () => { processor.Negative.should_be_false(); };
			};

			context["given that final memory value sign bit is 1"] = () =>
			{
				before = () =>
				{
					expectedValue = (byte)new Random().Next(0x80, 0xff);
					this.memory.Write((byte)(xRegisterValue + randomOffset), randomPage, (byte)(expectedValue + 1));
				};

				it["should not set zero flag"] = () => { processor.Zero.should_be_false(); };

				it["should set negative flag"] = () => { processor.Negative.should_be_true(); };
			};
		}
	}
}
