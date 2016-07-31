using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;
using System;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_cpy_operation : Describe_microprocessor_operation
	{
		public void When_use_immediate_addressing_mode()
		{
			var byteToCompare = default(byte);

			this.RunProgram(() => new byte[]
			{
				(byte)OpCode.LDY_Immediate, 0x05,
				(byte)OpCode.CPY_Immediate, byteToCompare
			});

			this.DefineSpecs((b) => byteToCompare = b);
		}

		public void When_use_zero_page_addressing_mode()
		{
			var randomOffset = default(byte);

			before = () => { randomOffset = this.Fixture.Create<byte>(); };

			this.RunProgram(() => new byte[]
			{
				(byte)OpCode.LDY_Immediate, 0x05,
				(byte)OpCode.CPY_ZeroPage, randomOffset
			});

			this.DefineSpecs((b) => this.Memory.WriteZeroPage(randomOffset, b));
		}

		public void When_use_absolute_addressing_mode()
		{
			var randomPage = default(byte);
			var randomOffset = default(byte);

			before = () =>
			{
				randomPage = this.Fixture.Create<byte>();
				randomOffset = this.Fixture.Create<byte>();
			};

			this.RunProgram(() => new byte[]
			{
				(byte)OpCode.LDY_Immediate, 0x05,
				(byte)OpCode.CPY_Absolute, randomOffset, randomPage
			});

			this.DefineSpecs((b) => this.Memory.Write(randomOffset, randomPage, b));
		}

		private void DefineSpecs(Action<byte> setByteToCompare)
		{
			context["given that y register value is lower than compared byte"] = () =>
			{
				before = () => { setByteToCompare(0xff); };
				this.NegativeFlagShouldBeFalse();
				this.CarryFlagShouldBeFalse();
				this.ZeroFlagShouldBeFalse();
			};

			context["given that y register value is equal than compared byte"] = () =>
			{
				before = () => { setByteToCompare(0x05); };
				this.NegativeFlagShouldBeFalse();
				this.CarryFlagShouldBeTrue();
				this.ZeroFlagShouldBeTrue();
			};

			context["given that y register value is grater than compared byte"] = () =>
			{
				before = () => { setByteToCompare(0x00); };
				this.NegativeFlagShouldBeFalse();
				this.CarryFlagShouldBeTrue();
				this.ZeroFlagShouldBeFalse();
			};
		}
	}
}