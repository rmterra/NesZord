using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;
using System;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_asl_operation : nspec
	{
		private static readonly Fixture fixture = new Fixture();

		private byte byteToShift;

		private MemoryMock memory;

		private Microprocessor processor;

		public void before_each()
		{
			this.memory = new MemoryMock();
			this.processor = new Microprocessor(this.memory);
			this.byteToShift = 0x05;
		}

		public void When_use_accumulator_addressing_mode()
		{
			act = () =>
			{
				this.processor.RunProgram(new byte[] { (byte) OpCode.ASL_Accumulator });
			};

			this.DefineSpecs(
				() => this.processor.Accumulator.Value,
				(b) => this.processor.Accumulator.Value = b);
		}

		public void When_zero_page_addressing_mode()
		{
			var randomOffset = default(byte);

			before = () => randomOffset = fixture.Create<byte>();

			act = () =>
			{
				this.processor.RunProgram(new byte[] { (byte) OpCode.ASL_ZeroPageX, randomOffset });
			};

			this.DefineSpecs(
				() => this.memory.Read(randomOffset, Memory.ZERO_PAGE), 
				(b) => this.memory.WriteZeroPage(randomOffset, b));
		}

		public void When_zero_page_x_addressing_mode()
		{
			var randomOffset = default(byte);
			var xRegisterValue = default(byte);

			before = () =>
			{
				randomOffset = fixture.Create<byte>();
				xRegisterValue = fixture.Create<byte>();
			};

			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte) OpCode.LDX_Immediate, xRegisterValue,
					(byte) OpCode.ASL_ZeroPageX, randomOffset
				});
			};

			this.DefineSpecs(
				() => this.memory.Read((byte)(xRegisterValue + randomOffset), Memory.ZERO_PAGE),
				(b) => this.memory.WriteZeroPage((byte)(xRegisterValue + randomOffset), b));
		}

		public void When_absolute_addressing_mode()
		{
			var randomOffset = default(byte);
			var randomPage = default(byte);

			before = () =>
			{
				randomOffset = fixture.Create<byte>();
				randomPage = fixture.Create<byte>();
			};

			act = () =>
			{
				this.processor.RunProgram(new byte[] { (byte)OpCode.ASL_Absolute, randomOffset, randomPage });
			};

			this.DefineSpecs(
				() => this.memory.Read(randomOffset, randomPage),
				(b) => this.memory.Write(randomOffset, randomPage, b));
		}

		public void When_absolute_x_addressing_mode()
		{
			var randomOffset = default(byte);
			var randomPage = default(byte);
			var xRegisterValue = default(byte);

			before = () =>
			{
				randomOffset = fixture.Create<byte>();
				randomPage = fixture.Create<byte>();
				xRegisterValue = fixture.Create<byte>();
			};

			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDX_Immediate, xRegisterValue,
					(byte)OpCode.ASL_AbsoluteX, randomOffset, randomPage
				});
			};

			this.DefineSpecs(
				() => this.memory.Read((byte)(xRegisterValue + randomOffset), randomPage),
				(b) => this.memory.Write((byte)(xRegisterValue + randomOffset), randomPage, b));
		}

		private void DefineSpecs(Func<byte> getByteToTest, Action<byte> setByteToShift = null)
		{
			before = () => setByteToShift?.Invoke(this.byteToShift);

			it["should byte to test be shifted to left"] = () =>
			{
				var byteToTest = getByteToTest?.Invoke();
				byteToTest.should_be(0x0a);
			};

			it["should not set carry flag"] = () => this.processor.Carry.should_be_false();
			it["should not set negative flag"] = () => this.processor.Negative.should_be_false();
			it["should not set zero flag"] = () => this.processor.Zero.should_be_false();

			context["given that byte to shift has the sign bit set"] = () =>
			{
				before = () => setByteToShift?.Invoke(0x80);
				it["should set carry flag"] = () => this.processor.Carry.should_be_true();
			};

			context["given that shift result has the sign bit set"] = () =>
			{
				before = () => setByteToShift?.Invoke(0x40);
				it["should set negative flag"] = () => this.processor.Negative.should_be_true();
			};

			context["given that shift result is 0x00"] = () =>
			{
				before = () => setByteToShift?.Invoke(0x80);
				it["should set zero flag"] = () => this.processor.Zero.should_be_true();
			};
		}
	}
}