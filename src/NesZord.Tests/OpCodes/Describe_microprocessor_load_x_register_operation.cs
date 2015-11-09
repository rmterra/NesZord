using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_load_x_register_operation : nspec
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
			var expectedXRegisterValue = default(byte);

			before = () => { expectedXRegisterValue = fixture.Create<byte>(); };

			act = () =>
			{
				var operation = (byte)OpCode.ImmediateLoadXRegister;
				processor.RunProgram(new byte[] { operation, expectedXRegisterValue });
			};

			it["should set x with received value"] = () => { processor.X.should_be(expectedXRegisterValue); };
		}

		public void When_use_zero_page_addressing_mode()
		{
			var expectedXRegisterValue = default(byte);
			var randomOffset = default(byte);

			before = () => 
			{
				expectedXRegisterValue = fixture.Create<byte>();
				randomOffset = fixture.Create<byte>();

				this.memory.WriteZeroPage(randomOffset, expectedXRegisterValue);
			};

			act = () =>
			{
				var operation = (byte)OpCode.ZeroPageLoadXRegister;
				processor.RunProgram(new byte[] { operation, randomOffset });
			};

			it["should set x with received value"] = () => { processor.X.should_be(expectedXRegisterValue); };
		}

		public void When_use_zero_y_page_addressing_mode()
		{
			var expectedXRegisterValue = default(byte);
			var randomOffset = default(byte);
			var yRegisterValue = default(byte);

			before = () =>
			{
				expectedXRegisterValue = fixture.Create<byte>();
				randomOffset = fixture.Create<byte>();
				yRegisterValue = fixture.Create<byte>();

				this.memory.WriteZeroPage((byte)(yRegisterValue + randomOffset), expectedXRegisterValue);
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.ImmediateLoadYRegister, yRegisterValue,
					(byte)OpCode.ZeroPageYLoadXRegister, randomOffset
				});
			};

			it["should set x register with received value"] = () => { processor.X.should_be(expectedXRegisterValue); };
		}

		public void When_use_absolute_addressing_mode()
		{
			var expectedXRegisterValue = default(byte);
			var randomPage = default(byte);
			var randomOffset = default(byte);

			before = () =>
			{
				expectedXRegisterValue = fixture.Create<byte>();
				randomPage = fixture.Create<byte>();
				randomOffset = fixture.Create<byte>();

				this.memory.Write(randomOffset, randomPage, expectedXRegisterValue);
			};

			act = () =>
			{
				var operation = (byte)OpCode.AbsoluteLoadXRegister;
				processor.RunProgram(new byte[] { operation, randomOffset, randomPage });
			};

			it["should set x with received value"] = () => { processor.X.should_be(expectedXRegisterValue); };
		}

		public void When_use_absolute_y_addressing_mode()
		{
			var expectedXRegisterValue = default(byte);
			var randomPage = default(byte);
			var randomOffset = default(byte);
			var yRegisterValue = default(byte);

			before = () =>
			{
				expectedXRegisterValue = fixture.Create<byte>();
				randomPage = fixture.Create<byte>();
				randomOffset = fixture.Create<byte>();
				yRegisterValue = fixture.Create<byte>();

				this.memory.Write((byte)(randomOffset + yRegisterValue), randomPage, expectedXRegisterValue);
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.ImmediateLoadYRegister, yRegisterValue,
					(byte)OpCode.AbsoluteYLoadXRegister, randomOffset, randomPage
				});
			};

			it["should set x with received value"] = () => { processor.X.should_be(expectedXRegisterValue); };
		}
	}
}