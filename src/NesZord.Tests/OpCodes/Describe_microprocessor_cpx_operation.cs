using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;
using System;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_cpx_operation : Describe_microprocessor_operation
	{
		public void When_use_immediate_addressing_mode()
		{
			var xRegisterValue = default(byte);
			var byteToCompare = default(byte);

			this.RunProgram(() => new byte[]
			{
				(byte)OpCode.LDX_Immediate, xRegisterValue,
				(byte)OpCode.CPX_Immediate, byteToCompare
			});

			this.DefineSpec(
				(b) => xRegisterValue = b,
				(b) => byteToCompare = b);
		}

		public void When_use_zero_page_addressing_mode()
		{
			var xRegisterValue = default(byte);
			var randomOffset = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[]
			{
				(byte)OpCode.LDX_Immediate, xRegisterValue,
				(byte)OpCode.CPX_ZeroPage, randomOffset
			});

			this.DefineSpec(
				(b) => xRegisterValue = b,
				(b) => this.Memory.WriteZeroPage(randomOffset, b));
		}

		public void When_use_absolute_addressing_mode()
		{
			var xRegisterValue = default(byte);
			var randomPage = this.Fixture.Create<byte>();
			var randomOffset = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[]
			{
				(byte)OpCode.LDX_Immediate, xRegisterValue,
				(byte)OpCode.CPX_Absolute, randomOffset, randomPage
			});

			this.DefineSpec(
				(b) => xRegisterValue = b,
				(b) => this.Memory.Write(randomOffset, randomPage, b));
		}

		private void DefineSpec(Action<byte> setXRegisterValue, Action<byte> setByteToCompare)
		{
			before = () => { setXRegisterValue(0x05); };

			context["given that x register value is lower than compared byte"] = () =>
			{
				before = () => { setByteToCompare(0xff); };
				this.NegativeFlagShouldBeFalse();
				this.CarryFlagShouldBeFalse();
				this.ZeroFlagShouldBeFalse();
			};

			context["given that x register value is equal than compared byte"] = () =>
			{
				before = () => { setByteToCompare(0x05); };
				this.NegativeFlagShouldBeFalse();
				this.CarryFlagShouldBeTrue();
				this.ZeroFlagShouldBeTrue();
			};

			context["given that x register value is greater than compared byte"] = () =>
			{
				before = () => { setByteToCompare(0x00); };
				this.NegativeFlagShouldBeFalse();
				this.CarryFlagShouldBeTrue();
				this.ZeroFlagShouldBeFalse();
			};

			context["given compare result is greater than 0x80"] = () =>
			{
				before = () =>
				{
					setXRegisterValue(0xff);
					setByteToCompare(0x00);
				};

				this.NegativeFlagShouldBeTrue();
				this.CarryFlagShouldBeTrue();
				this.ZeroFlagShouldBeFalse();
			};
		}
	}
}