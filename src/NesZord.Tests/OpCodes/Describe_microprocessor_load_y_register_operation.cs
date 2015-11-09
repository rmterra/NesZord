using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_load_y_register_operation : nspec
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
			var expectedYRegisterValue = default(byte);

			before = () => { expectedYRegisterValue = fixture.Create<byte>(); };

			act = () =>
			{
				var operation = (byte)OpCode.ImmediateLoadYRegister;
				processor.RunProgram(new byte[] { operation, expectedYRegisterValue });
			};

			it["should set y with received value"] = () => { processor.Y.should_be(expectedYRegisterValue); };
		}

		public void When_use_zero_page_addressing_mode()
		{
			var expectedYRegisterValue = default(byte);
			var randomOffset = default(byte);

			before = () => 
			{
				expectedYRegisterValue = fixture.Create<byte>();
				randomOffset = fixture.Create<byte>();

				this.memory.WriteZeroPage(randomOffset, expectedYRegisterValue);
			};

			act = () =>
			{
				var operation = (byte)OpCode.ZeroPageLoadYRegister;
				processor.RunProgram(new byte[] { operation, randomOffset });
			};

			it["should set y with received value"] = () => { processor.Y.should_be(expectedYRegisterValue); };
		}

		public void When_use_zero_x_page_addressing_mode()
		{
			var expectedYRegisterValue = default(byte);
			var randomOffset = default(byte);
			var xRegisterValue = default(byte);

			before = () =>
			{
				expectedYRegisterValue = fixture.Create<byte>();
				randomOffset = fixture.Create<byte>();
				xRegisterValue = fixture.Create<byte>();

				this.memory.WriteZeroPage((byte)(xRegisterValue + randomOffset), expectedYRegisterValue);
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.ImmediateLoadXRegister, xRegisterValue,
					(byte)OpCode.ZeroPageXLoadYRegister, randomOffset
				});
			};

			it["should set y register with received value"] = () => { processor.Y.should_be(expectedYRegisterValue); };
		}

		public void When_use_absolute_addressing_mode()
		{
			var expectedYRegisterValue = default(byte);
			var randomPage = default(byte);
			var randomOffset = default(byte);

			before = () =>
			{
				expectedYRegisterValue = fixture.Create<byte>();
				randomPage = fixture.Create<byte>();
				randomOffset = fixture.Create<byte>();

				this.memory.Write(randomOffset, randomPage, expectedYRegisterValue);
			};

			act = () =>
			{
				var operation = (byte)OpCode.AbsoluteLoadYRegister;
				processor.RunProgram(new byte[] { operation, randomOffset, randomPage });
			};

			it["should set y with received value"] = () => { processor.Y.should_be(expectedYRegisterValue); };
		}

		public void When_use_absolute_x_addressing_mode()
		{
			var expectedYRegisterValue = default(byte);
			var randomPage = default(byte);
			var randomOffset = default(byte);
			var xRegisterValue = default(byte);

			before = () =>
			{
				expectedYRegisterValue = fixture.Create<byte>();
				randomPage = fixture.Create<byte>();
				randomOffset = fixture.Create<byte>();
				xRegisterValue = fixture.Create<byte>();

				this.memory.Write((byte)(randomOffset + xRegisterValue), randomPage, expectedYRegisterValue);
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.ImmediateLoadXRegister, xRegisterValue,
					(byte)OpCode.AbsoluteXLoadYRegister, randomOffset, randomPage
				});
			};

			it["should set y with received value"] = () => { processor.Y.should_be(expectedYRegisterValue); };
		}
	}
}