using NesZord.Core;
using NesZord.Core.Extensions;
using NSpec;
using Ploeh.AutoFixture;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_lsr_operation : nspec
	{
		private static readonly Fixture fixture = new Fixture();

		private MemoryMock memory;

		private Microprocessor processor;

		public void before_each()
		{
			this.memory = new MemoryMock();
			this.processor = new Microprocessor(this.memory);
		}

		public void When_use_accumulator_addressing_mode()
		{
			var randomByte = default(byte);
			var shiftedValue = default(byte);

			before = () =>
			{
				randomByte = fixture.Create<byte>();
				shiftedValue = (byte)(randomByte >> 1);
			};

			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte) OpCode.LDA_Immediate, randomByte,
					(byte) OpCode.LSR_Accumulator
				});
			};

			it["should accumulator value be shifted to right"] = () => this.processor.Accumulator.should_be(shiftedValue);

			it["should set negative flag to false"] = () => { this.processor.Negative.should_be(false); };

			it["should set carry flag equal to first accumulator bit"] = () =>
			{
				this.processor.Carry.should_be(randomByte.GetBitAt(Microprocessor.FIRST_BIT_POSITION));
			};

			it["should set zero flag when shifted value is 0xff or 0x00"] = () =>
			{
				this.processor.Zero.should_be((shiftedValue & 0xff) == 0);
			};
		}

		public void When_zero_page_addressing_mode()
		{
			var randomOffset = default(byte);
			var randomByte = default(byte);
			var shiftedValue = default(byte);

			before = () =>
			{
				randomOffset = fixture.Create<byte>();
				randomByte = fixture.Create<byte>();
				shiftedValue = (byte)(randomByte >> 1);

				this.memory.WriteZeroPage(randomOffset, randomByte);
			};

			act = () =>
			{
				this.processor.RunProgram(new byte[] { (byte) OpCode.LSR_ZeroPage, randomOffset });
			};

			it["should memory value be shifted to right"] = () =>
			{
				this.memory.Read(randomOffset, Memory.ZERO_PAGE).should_be(shiftedValue);
			};

			it["should set negative flag to false"] = () => { this.processor.Negative.should_be(false); };

			it["should set carry flag equal to first memory value bit"] = () =>
			{
				this.processor.Carry.should_be(randomByte.GetBitAt(Microprocessor.FIRST_BIT_POSITION));
			};

			it["should set zero flag when shifted value is 0xff or 0x00"] = () =>
			{
				this.processor.Zero.should_be((shiftedValue & 0xff) == 0);
			};
		}

		public void When_zero_page_x_addressing_mode()
		{
			var randomOffset = default(byte);
			var xRegisterValue = default(byte);
			var randomByte = default(byte);
			var shiftedValue = default(byte);

			before = () =>
			{
				randomOffset = fixture.Create<byte>();
				xRegisterValue = fixture.Create<byte>();
				randomByte = fixture.Create<byte>();
				shiftedValue = (byte)(randomByte >> 1);

				this.memory.WriteZeroPage((byte)(xRegisterValue + randomOffset), randomByte);
			};

			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte) OpCode.LDX_Immediate, xRegisterValue,
					(byte) OpCode.LSR_ZeroPageX, randomOffset
				});
			};

			it["should memory value be shifted to right"] = () =>
			{
				this.memory.Read((byte)(xRegisterValue + randomOffset), Memory.ZERO_PAGE).should_be(shiftedValue);
			};

			it["should set negative flag to false"] = () =>
			{
				this.processor.Negative.should_be(false);
			};

			it["should set carry flag equal to first memory value bit"] = () =>
			{
				this.processor.Carry.should_be(randomByte.GetBitAt(Microprocessor.FIRST_BIT_POSITION));
			};

			it["should set zero flag when shifted value is 0xff or 0x00"] = () =>
			{
				this.processor.Zero.should_be((shiftedValue & 0xff) == 0);
			};
		}

		public void When_absolute_addressing_mode()
		{
			var randomOffset = default(byte);
			var randomPage = default(byte);
			var randomByte = default(byte);
			var shiftedValue = default(byte);

			before = () =>
			{
				randomOffset = fixture.Create<byte>();
				randomPage = fixture.Create<byte>();
				randomByte = fixture.Create<byte>();
				shiftedValue = (byte)(randomByte >> 1);

				this.memory.Write(randomOffset, randomPage, randomByte);
			};

			act = () =>
			{
				this.processor.RunProgram(new byte[] { (byte)OpCode.LSR_Absolute, randomOffset, randomPage });
			};

			it["should memory value be shifted to right"] = () =>
			{
				this.memory.Read(randomOffset, randomPage).should_be(shiftedValue);
			};

			it["should set negative flag to false"] = () =>
			{
				this.processor.Negative.should_be(false);
			};

			it["should set carry flag equal to first memory value bit"] = () =>
			{
				this.processor.Carry.should_be(randomByte.GetBitAt(Microprocessor.FIRST_BIT_POSITION));
			};

			it["should set zero flag when shifted value is 0xff or 0x00"] = () =>
			{
				this.processor.Zero.should_be((shiftedValue & 0xff) == 0);
			};
		}

		public void When_absolute_x_addressing_mode()
		{
			var randomOffset = default(byte);
			var randomPage = default(byte);
			var xRegisterValue = default(byte);
			var randomByte = default(byte);
			var shiftedValue = default(byte);

			before = () =>
			{
				randomOffset = fixture.Create<byte>();
				randomPage = fixture.Create<byte>();
				xRegisterValue = fixture.Create<byte>();
				randomByte = fixture.Create<byte>();
				shiftedValue = (byte)(randomByte >> 1);

				this.memory.Write((byte)(xRegisterValue + randomOffset), randomPage, randomByte);
			};

			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDX_Immediate, xRegisterValue,
					(byte)OpCode.LSR_AbsoluteX, randomOffset, randomPage
				});
			};

			it["should memory value be shifted to right"] = () =>
			{
				this.memory.Read((byte)(xRegisterValue + randomOffset), randomPage).should_be(shiftedValue);
			};

			it["should set negative flag to false"] = () =>
			{
				this.processor.Negative.should_be(false);
			};

			it["should set carry flag equal to first memory value bit"] = () =>
			{
				this.processor.Carry.should_be(randomByte.GetBitAt(Microprocessor.FIRST_BIT_POSITION));
			};

			it["should set zero flag when shifted value is 0xff or 0x00"] = () =>
			{
				this.processor.Zero.should_be((shiftedValue & 0xff) == 0);
			};
		}
	}
}
