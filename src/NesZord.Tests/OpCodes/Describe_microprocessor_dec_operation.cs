using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;
using System;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_dec_operation : Describe_microprocessor_operation
	{
		public void When_use_zero_page_addressing_mode()
		{
			var randomOffset = default(byte);

			before = () => randomOffset = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[] { (byte)OpCode.DEC_ZeroPage, randomOffset });

			this.DefineSpecs(
				(b) => this.Memory.WriteZeroPage(randomOffset, b),
				() => this.Memory.Read(randomOffset, Core.Memory.ZERO_PAGE));
		}

		public void When_use_zero_page_x_addressing_mode()
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
				(byte)OpCode.LDX_Immediate, xRegisterValue,
				(byte)OpCode.DEC_ZeroPageX, randomOffset
			});

			this.DefineSpecs(
				(b) => this.Memory.WriteZeroPage((byte)(xRegisterValue + randomOffset), b),
				() => this.Memory.Read((byte)(xRegisterValue + randomOffset), Core.Memory.ZERO_PAGE));
		}

		public void When_use_absolute_addressing_mode()
		{
			var randomOffset = default(byte);
			var randomPage = default(byte);

			before = () =>
			{
				randomOffset = this.Fixture.Create<byte>();
				randomPage = this.Fixture.Create<byte>();
			};

			this.RunProgram(() => new byte[] { (byte)OpCode.DEC_Absolute, randomOffset, randomPage });

			this.DefineSpecs(
				(b) => this.Memory.Write(randomOffset, randomPage, b),
				() => this.Memory.Read(randomOffset, randomPage));
		}

		public void When_use_absolute_x_addressing_mode()
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
				(byte)OpCode.DEC_AbsoluteX, randomOffset, randomPage
			});

			this.DefineSpecs(
				(b) => this.Memory.Write((byte)(xRegisterValue + randomOffset), randomPage, b),
				() => this.Memory.Read((byte)(xRegisterValue + randomOffset), randomPage));
		}

		private void DefineSpecs(Action<byte> setByteToDecrement, Func<byte> getDecrementedByte)
		{
			before = () => setByteToDecrement(0x05);

			it["should decrement 1 to memory value"] = () => getDecrementedByte().should_be(0x04);

			context["given that memory value is 0x01"] = () =>
			{
				before = () => setByteToDecrement(0x01);

				this.ZeroFlagShouldBeTrue();
				this.NegativeFlagShouldBeFalse();
			};

			context["given that sign bit of memory value is set"] = () =>
			{
				before = () => setByteToDecrement(0x81);

				this.ZeroFlagShouldBeFalse();
				this.NegativeFlagShouldBeTrue();
			};
		}
	}
}