using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;
using System;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_asl_operation : Describe_microprocessor_operation
	{
		private byte byteToShift;

		public void When_use_accumulator_addressing_mode()
		{
			this.RunProgram(() => new byte[] { (byte) OpCode.ASL_Accumulator });

			this.DefineSpecs(
				() => this.Processor.Accumulator.Value,
				(b) => this.Processor.Accumulator.Value = b);
		}

		public void When_zero_page_addressing_mode()
		{
			var randomOffset = default(byte);

			before = () => randomOffset = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[] { (byte) OpCode.ASL_ZeroPageX, randomOffset });

			this.DefineSpecs(
				() => this.Memory.Read(randomOffset, Core.Memory.ZERO_PAGE), 
				(b) => this.Memory.WriteZeroPage(randomOffset, b));
		}

		public void When_zero_page_x_addressing_mode()
		{
			var randomOffset = default(byte);
			var xRegisterValue = default(byte);

			before = () =>
			{
				randomOffset = this.Fixture.Create<byte>();
				xRegisterValue = this.Fixture.Create<byte>();
			};

			this.RunProgram(() => new byte[]
			{
				(byte) OpCode.LDX_Immediate, xRegisterValue,
				(byte) OpCode.ASL_ZeroPageX, randomOffset
			});

			this.DefineSpecs(
				() => this.Memory.Read((byte)(xRegisterValue + randomOffset), Core.Memory.ZERO_PAGE),
				(b) => this.Memory.WriteZeroPage((byte)(xRegisterValue + randomOffset), b));
		}

		public void When_absolute_addressing_mode()
		{
			var randomOffset = default(byte);
			var randomPage = default(byte);

			before = () =>
			{
				randomOffset = this.Fixture.Create<byte>();
				randomPage = this.Fixture.Create<byte>();
			};

			this.RunProgram(() => new byte[] { (byte)OpCode.ASL_Absolute, randomOffset, randomPage });

			this.DefineSpecs(
				() => this.Memory.Read(randomOffset, randomPage),
				(b) => this.Memory.Write(randomOffset, randomPage, b));
		}

		public void When_absolute_x_addressing_mode()
		{
			var randomOffset = default(byte);
			var randomPage = default(byte);
			var xRegisterValue = default(byte);

			before = () =>
			{
				randomOffset = this.Fixture.Create<byte>();
				randomPage = this.Fixture.Create<byte>();
				xRegisterValue = this.Fixture.Create<byte>();
			};

			this.RunProgram(() => new byte[]
			{
				(byte)OpCode.LDX_Immediate, xRegisterValue,
				(byte)OpCode.ASL_AbsoluteX, randomOffset, randomPage
			});

			this.DefineSpecs(
				() => this.Memory.Read((byte)(xRegisterValue + randomOffset), randomPage),
				(b) => this.Memory.Write((byte)(xRegisterValue + randomOffset), randomPage, b));
		}

		private void DefineSpecs(Func<byte> getByteToTest, Action<byte> setByteToShift = null)
		{
			before = () =>
			{
				this.byteToShift = 0x05;
				setByteToShift?.Invoke(this.byteToShift);
			};

			it["should byte to test be shifted to left"] = () =>
			{
				var byteToTest = getByteToTest?.Invoke();
				byteToTest.should_be(0x0a);
			};

			this.CarryFlagShouldBeFalse();
			this.NegativeFlagShouldBeFalse();
			this.ZeroFlagShouldBeFalse();

			context["given that byte to shift has the sign bit set"] = () =>
			{
				before = () => setByteToShift?.Invoke(0x80);
				this.CarryFlagShouldBeTrue();
			};

			context["given that shift result has the sign bit set"] = () =>
			{
				before = () => setByteToShift?.Invoke(0x40);
				this.NegativeFlagShouldBeTrue();
			};

			context["given that shift result is 0x00"] = () =>
			{
				before = () => setByteToShift?.Invoke(0x80);
				this.ZeroFlagShouldBeTrue();
			};
		}
	}
}