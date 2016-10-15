using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;
using System;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_inc_operation : Describe_microprocessor_operation
	{
		public void When_use_zero_page_addressing_mode()
		{
			var randomOffset = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[] { (byte)OpCode.INC_ZeroPage, randomOffset });

			this.DefoneSpecs(() => new MemoryLocation(randomOffset, Core.Memory.ZERO_PAGE));
		}

		public void When_use_zero_page_x_addressing_mode()
		{
			var randomOffset = this.Fixture.Create<byte>();
			var xRegisterValue = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[]
			{
				(byte)OpCode.LDX_Immediate, xRegisterValue,
				(byte)OpCode.INC_ZeroPageX, randomOffset
			});

			this.DefoneSpecs(() => new MemoryLocation((byte)(randomOffset + xRegisterValue), Core.Memory.ZERO_PAGE));
		}

		public void When_use_absolute_addressing_mode()
		{
			var randomOffset = this.Fixture.Create<byte>();
			var randomPage = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[] { (byte)OpCode.INC_Absolute, randomOffset, randomPage });

			this.DefoneSpecs(() => new MemoryLocation(randomOffset, randomPage));
		}

		public void When_use_absolute_x_addressing_mode()
		{
			var randomOffset = this.Fixture.Create<byte>();
			var randomPage = this.Fixture.Create<byte>();
			var xRegisterValue = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[]
			{
				(byte)OpCode.LDX_Immediate, xRegisterValue,
				(byte)OpCode.INC_AbsoluteX, randomOffset, randomPage
			});

			this.DefoneSpecs(() => new MemoryLocation(randomOffset, randomPage).Sum(xRegisterValue));
		}

		private void DefoneSpecs(Func<MemoryLocation> getLocation)
		{
			before = () => this.Memory.Write(getLocation?.Invoke(), 0x05);

			it["should increment 1 to memory value"] = () => this.Memory.Read(getLocation?.Invoke()).should_be(0x06);

			this.ZeroFlagShouldBeFalse();
			this.NegativeFlagShouldBeFalse();

			context["given that memory value is 0xff"] = () =>
			{
				before = () => this.Memory.Write(getLocation?.Invoke(), 0xff);

				this.ZeroFlagShouldBeTrue();
				this.NegativeFlagShouldBeFalse();
			};

			context["given that final memory value sign bit is 1"] = () =>
			{
				before = () => this.Memory.Write(getLocation?.Invoke(), 0x7f);

				this.ZeroFlagShouldBeFalse();
				this.NegativeFlagShouldBeTrue();
			};
		}
	}
}