using NesZord.Core;
using Ploeh.AutoFixture;
using System;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_cmp_operation : Describe_microprocessor_operation
	{
		public void When_use_immediate_addressing_mode()
		{
			var accumulatorValue = default(byte);
			var byteToCompare = default(byte);

			this.RunProgram(() => new byte[]
			{
				(byte)OpCode.LDA_Immediate, accumulatorValue,
				(byte)OpCode.CMP_Immediate, byteToCompare
			});

			this.DefineSpecs(
				(b) => accumulatorValue = b,
				(b) => byteToCompare = b);
		}

		public void When_use_zero_page_addressing_mode()
		{
			var randomOffset = default(byte);
			var accumulatorValue = default(byte);

			before = () => randomOffset = this.Fixture.Create<byte>();

			this.RunProgram(() => new byte[]
			{
				(byte)OpCode.LDA_Immediate, accumulatorValue,
				(byte)OpCode.CMP_ZeroPage, randomOffset
			});

			this.DefineSpecs(
				(b) => accumulatorValue = b,
				(b) => this.Memory.WriteZeroPage(randomOffset, b));
		}

		public void When_use_zero_page_x_addressing_mode()
		{
			var randomOffset = default(byte);
			var accumulatorValue = default(byte);
			var xRegisterValue = default(byte);

			before = () =>
			{
				randomOffset = this.Fixture.Create<byte>();
				xRegisterValue = this.Fixture.Create<byte>();
			};

			this.RunProgram(() => new byte[]
			{
				(byte)OpCode.LDX_Immediate, xRegisterValue,
				(byte)OpCode.LDA_Immediate, accumulatorValue,
				(byte)OpCode.CMP_ZeroPageX, randomOffset
			});

			this.DefineSpecs(
				(b) => accumulatorValue = b,
				(b) => this.Memory.WriteZeroPage((byte)(xRegisterValue + randomOffset), b));
		}

		public void When_use_absolute_addressing_mode()
		{
			var accumulatorValue = default(byte);
			var randomPage = default(byte);
			var randomOffset = default(byte);

			before = () =>
			{
				randomPage = this.Fixture.Create<byte>();
				randomOffset = this.Fixture.Create<byte>();
			};

			this.RunProgram(() => new byte[]
			{
				(byte)OpCode.LDA_Immediate, accumulatorValue,
				(byte)OpCode.CMP_Absolute, randomOffset, randomPage
			});

			this.DefineSpecs(
				(b) => accumulatorValue = b,
				(b) => this.Memory.Write(randomOffset, randomPage, b));
		}

		public void When_use_absolute_x_addressing_mode()
		{
			var accumulatorValue = default(byte);
			var xRegisterValue = default(byte);
			var randomPage = default(byte);
			var randomOffset = default(byte);

			before = () =>
			{
				randomPage = this.Fixture.Create<byte>();
				randomOffset = this.Fixture.Create<byte>();
				xRegisterValue = this.Fixture.Create<byte>();
			};

			this.RunProgram(() => new byte[]
			{
				(byte)OpCode.LDX_Immediate, xRegisterValue,
				(byte)OpCode.LDA_Immediate, accumulatorValue,
				(byte)OpCode.CMP_AbsoluteX, randomOffset, randomPage
			});

			this.DefineSpecs(
				(b) => accumulatorValue = b,
				(b) => this.Memory.Write((byte)(xRegisterValue + randomOffset), randomPage, b));
		}

		public void When_use_absolute_y_addressing_mode()
		{
			var accumulatorValue = default(byte);
			var yRegisterValue = default(byte);
			var randomPage = default(byte);
			var randomOffset = default(byte);

			before = () =>
			{
				randomPage = this.Fixture.Create<byte>();
				randomOffset = this.Fixture.Create<byte>();
				yRegisterValue = this.Fixture.Create<byte>();
			};

			this.RunProgram(() => new byte[]
			{
				(byte)OpCode.LDY_Immediate, yRegisterValue,
				(byte)OpCode.LDA_Immediate, accumulatorValue,
				(byte)OpCode.CMP_AbsoluteY, randomOffset, randomPage
			});

			this.DefineSpecs(
				(b) => accumulatorValue = b,
				(b) => this.Memory.Write((byte)(yRegisterValue + randomOffset), randomPage, b));
		}

		public void When_use_indexed_indirect_addressing_mode()
		{
			var accumulatorValue = default(byte);
			var xRegisterValue = default(byte);
			var randomOffset = default(byte);

			before = () =>
			{
				randomOffset = this.Fixture.Create<byte>();
				xRegisterValue = this.Fixture.Create<byte>();
			};

			this.RunProgram(() => new byte[]
			{
				(byte)OpCode.LDX_Immediate, xRegisterValue,
				(byte)OpCode.LDA_Immediate, accumulatorValue,
				(byte)OpCode.CMP_IndexedIndirect, randomOffset
			});

			this.DefineSpecs(
				(b) => accumulatorValue = b,
				(b) => this.Memory.MockIndexedIndirectMemoryWrite(randomOffset, xRegisterValue, b));
		}

		public void When_use_indirect_indexed_addressing_mode()
		{
			var accumulatorValue = default(byte);
			var yRegisterValue = default(byte);
			var randomOffset = default(byte);

			before = () =>
			{
				randomOffset = this.Fixture.Create<byte>();
				yRegisterValue = this.Fixture.Create<byte>();
			};

			this.RunProgram(() => new byte[]
			{
				(byte)OpCode.LDY_Immediate, yRegisterValue,
				(byte)OpCode.LDA_Immediate, accumulatorValue,
				(byte)OpCode.CMP_IndirectIndexed, randomOffset
			});

			this.DefineSpecs(
				(b) => accumulatorValue = b,
				(b) => this.Memory.MockIndirectIndexedMemoryWrite(randomOffset, yRegisterValue, b));
		}

		private void DefineSpecs(Action<byte> setAccumulatorValue, Action<byte> setByteToCompare)
		{
			before = () => { setAccumulatorValue(0x05); };

			context["given that accumulator value is lower than compared byte"] = () =>
			{
				before = () => { setByteToCompare(0xff); };
				this.NegativeFlagShouldBeFalse();
				this.CarryFlagShouldBeFalse();
				this.ZeroFlagShouldBeFalse();
			};

			context["given that accumulator value is equal than compared byte"] = () =>
			{
				before = () => { setByteToCompare(0x05); };
				this.NegativeFlagShouldBeFalse();
				this.CarryFlagShouldBeTrue();
				this.ZeroFlagShouldBeTrue();
			};

			context["given that accumulator value is greater than compared byte"] = () =>
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
					setAccumulatorValue(0xff);
					setByteToCompare(0x00);
				};

				this.NegativeFlagShouldBeTrue();
				this.CarryFlagShouldBeTrue();
				this.ZeroFlagShouldBeFalse();
			};
		}
	}
}