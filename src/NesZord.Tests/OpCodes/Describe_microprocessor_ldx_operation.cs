using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;
using System;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_ldx_operation : Describe_microprocessor_operation
	{
		public void When_use_immediate_addressing_mode()
		{
			var xRegisterValue = default(byte);

			this.RunProgram(() => new byte[] { (byte)OpCode.LDX_Immediate, xRegisterValue });

			this.DefineSpecs((b) => xRegisterValue = b);
		}

		public void When_use_zero_page_addressing_mode()
		{
			var randomOffset = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[] { (byte)OpCode.LDX_ZeroPage, randomOffset });

			this.DefineSpecs((b) => this.Memory.WriteZeroPage(randomOffset, b));
		}

		public void When_use_zero_y_page_addressing_mode()
		{
			var randomOffset = this.Fixture.Create<byte>();
			var yRegisterValue = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[]
			{
				(byte)OpCode.LDY_Immediate, yRegisterValue,
				(byte)OpCode.LDX_ZeroPageY, randomOffset
			});

			this.DefineSpecs((b) => this.Memory.WriteZeroPage((byte)(yRegisterValue + randomOffset), b));
		}

		public void When_use_absolute_addressing_mode()
		{
			var randomPage = this.Fixture.Create<byte>();
			var randomOffset = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[] { (byte)OpCode.LDX_Absolute, randomOffset, randomPage });

			this.DefineSpecs((b) => this.Memory.Write(randomOffset, randomPage, b));
		}

		public void When_use_absolute_y_addressing_mode()
		{
			var randomPage = this.Fixture.Create<byte>();
			var randomOffset = this.Fixture.Create<byte>();
			var yRegisterValue = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[]
			{
				(byte)OpCode.LDY_Immediate, yRegisterValue,
				(byte)OpCode.LDX_AbsoluteY, randomOffset, randomPage
			});

			this.DefineSpecs((b) => this.Memory.Write(new MemoryLocation(randomOffset, randomPage).Sum(yRegisterValue), b));
		}

		private void DefineSpecs(Action<byte> setValue)
		{
			before = () => setValue?.Invoke(0x05);

			it["should set x register with received value"] = () => this.Processor.X.Value.should_be(0x05);

			context["given that new x register value is 0x00"] = () =>
			{
				before = () => setValue?.Invoke(0x00);

				this.ZeroFlagShouldBeTrue();
				this.NegativeFlagShouldBeFalse();
			};

			context["given that new x register has sign bit set"] = () =>
			{
				before = () => setValue?.Invoke(0x80);

				this.ZeroFlagShouldBeFalse();
				this.NegativeFlagShouldBeTrue();
			};
		}
	}
}