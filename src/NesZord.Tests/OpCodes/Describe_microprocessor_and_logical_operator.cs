﻿using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;
using NesZord.Core.Extensions;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_and_logical_operator : nspec
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
			var byteToCompare = default(byte);

			before = () => { byteToCompare = fixture.Create<byte>(); };

			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte) OpCode.AbsoluteLoadAccumulator, fixture.Create<byte>(),
					(byte) OpCode.TransferFromAccumulatorToX,
					(byte) OpCode.ImmediateBitwiseAnd, byteToCompare
				});
			};

			it["should set bitwise 'and' result on accumulator"] = () =>
			{
				this.processor.Accumulator.should_be(this.processor.X & byteToCompare);
			};

			it["should set negative flag equal to seventh accumulator bit"] = () =>
			{
				this.processor.Negative.should_be(this.processor.Accumulator.GetBitAt(Microprocessor.NEGATIVE_FLAG_BYTE_POSITION));
			};

			it["should set zero flag when accumulator is 0"] = () =>
			{
				this.processor.Zero.should_be(this.processor.Accumulator == 0);
			};
		}

		public void When_use_zero_page_addressing_mode()
		{
			var randomOffset = default(byte);
			var byteToCompare = default(byte);

			before = () => 
			{
				randomOffset = fixture.Create<byte>();
				byteToCompare = fixture.Create<byte>();
				this.memory.WriteZeroPage(randomOffset, byteToCompare);
			};

			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte) OpCode.AbsoluteLoadAccumulator, fixture.Create<byte>(),
					(byte) OpCode.TransferFromAccumulatorToX,
					(byte) OpCode.ZeroPageBitwiseAnd, randomOffset
				});
			};

			it["should set bitwise 'and' result on accumulator"] = () =>
			{
				this.processor.Accumulator.should_be(this.processor.X & byteToCompare);
			};

			it["should set negative flag equal to seventh accumulator bit"] = () =>
			{
				this.processor.Negative.should_be(this.processor.Accumulator.GetBitAt(Microprocessor.NEGATIVE_FLAG_BYTE_POSITION));
			};

			it["should set zero flag when accumulator is 0"] = () =>
			{
				this.processor.Zero.should_be(this.processor.Accumulator == 0);
			};
		}

		public void When_use_zero_page_x_addressing_mode()
		{
			var randomOffset = default(byte);
			var byteToCompare = default(byte);
			var xRegisterValue = default(byte);

			before = () =>
			{
				randomOffset = fixture.Create<byte>();
				byteToCompare = fixture.Create<byte>();
				xRegisterValue = fixture.Create<byte>();
				this.memory.WriteZeroPage((byte)(xRegisterValue + randomOffset), byteToCompare);
			};

			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte) OpCode.AbsoluteLoadAccumulator, fixture.Create<byte>(),
					(byte) OpCode.TransferFromAccumulatorToY,
					(byte) OpCode.ImmediateLoadXRegister, xRegisterValue,
					(byte) OpCode.ZeroPageXBitwiseAnd, randomOffset
				});
			};

			it["should set bitwise 'and' result on accumulator"] = () =>
			{
				this.processor.Accumulator.should_be(this.processor.Y & byteToCompare);
			};

			it["should set negative flag equal to seventh accumulator bit"] = () =>
			{
				this.processor.Negative.should_be(this.processor.Accumulator.GetBitAt(Microprocessor.NEGATIVE_FLAG_BYTE_POSITION));
			};

			it["should set zero flag when accumulator is 0"] = () =>
			{
				this.processor.Zero.should_be(this.processor.Accumulator == 0);
			};
		}

		public void When_use_absolute_addressing_mode()
		{
			var randomOffset = default(byte);
			var randomPage = default(byte);
			var byteToCompare = default(byte);

			before = () =>
			{
				randomOffset = fixture.Create<byte>();
				byteToCompare = fixture.Create<byte>();
				randomPage = fixture.Create<byte>();
				this.memory.Write(randomPage, randomOffset, byteToCompare);
			};

			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte) OpCode.AbsoluteLoadAccumulator, fixture.Create<byte>(),
					(byte) OpCode.TransferFromAccumulatorToX,
					(byte) OpCode.AbsoluteBitwiseAnd, randomOffset, randomPage
				});
			};

			it["should set bitwise 'and' result on accumulator"] = () =>
			{
				this.processor.Accumulator.should_be(this.processor.X & byteToCompare);
			};

			it["should set negative flag equal to seventh accumulator bit"] = () =>
			{
				this.processor.Negative.should_be(this.processor.Accumulator.GetBitAt(Microprocessor.NEGATIVE_FLAG_BYTE_POSITION));
			};

			it["should set zero flag when accumulator is 0"] = () =>
			{
				this.processor.Zero.should_be(this.processor.Accumulator == 0);
			};
		}

		public void When_use_absolute_x_addressing_mode()
		{
			var randomOffset = default(byte);
			var randomPage = default(byte);
			var byteToCompare = default(byte);
			var xRegisterValue = default(byte);

			before = () =>
			{
				randomOffset = fixture.Create<byte>();
				byteToCompare = fixture.Create<byte>();
				randomPage = fixture.Create<byte>();
				xRegisterValue = fixture.Create<byte>();
				this.memory.Write(randomPage, (byte)(xRegisterValue + randomOffset), byteToCompare);
			};

			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte) OpCode.AbsoluteLoadAccumulator, fixture.Create<byte>(),
					(byte) OpCode.TransferFromAccumulatorToY,
					(byte) OpCode.ImmediateLoadXRegister, xRegisterValue,
					(byte) OpCode.AbsoluteXBitwiseAnd, randomOffset, randomPage
				});
			};

			it["should set bitwise 'and' result on accumulator"] = () =>
			{
				this.processor.Accumulator.should_be(this.processor.Y & byteToCompare);
			};

			it["should set negative flag equal to seventh accumulator bit"] = () =>
			{
				this.processor.Negative.should_be(this.processor.Accumulator.GetBitAt(Microprocessor.NEGATIVE_FLAG_BYTE_POSITION));
			};

			it["should set zero flag when accumulator is 0"] = () =>
			{
				this.processor.Zero.should_be(this.processor.Accumulator == 0);
			};
		}

		public void When_use_absolute_y_addressing_mode()
		{
			var randomOffset = default(byte);
			var randomPage = default(byte);
			var byteToCompare = default(byte);
			var yRegisterValue = default(byte);

			before = () =>
			{
				randomOffset = fixture.Create<byte>();
				byteToCompare = fixture.Create<byte>();
				randomPage = fixture.Create<byte>();
				yRegisterValue = fixture.Create<byte>();
				this.memory.Write(randomPage, (byte)(yRegisterValue + randomOffset), byteToCompare);
			};

			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte) OpCode.AbsoluteLoadAccumulator, fixture.Create<byte>(),
					(byte) OpCode.TransferFromAccumulatorToX,
					(byte) OpCode.ImmediateLoadYRegister, yRegisterValue,
					(byte) OpCode.AbsoluteXBitwiseAnd, randomOffset, randomPage
				});
			};

			it["should set bitwise 'and' result on accumulator"] = () =>
			{
				this.processor.Accumulator.should_be(this.processor.X & byteToCompare);
			};

			it["should set negative flag equal to seventh accumulator bit"] = () =>
			{
				this.processor.Negative.should_be(this.processor.Accumulator.GetBitAt(Microprocessor.NEGATIVE_FLAG_BYTE_POSITION));
			};

			it["should set zero flag when accumulator is 0"] = () =>
			{
				this.processor.Zero.should_be(this.processor.Accumulator == 0);
			};
		}

		public void When_use_indexed_indirect_addressing_mode()
		{
			var randomOffset = default(byte);
			var byteToCompare = default(byte);
			var xRegisterValue = default(byte);

			before = () =>
			{
				randomOffset = fixture.Create<byte>();
				byteToCompare = fixture.Create<byte>();
				xRegisterValue = fixture.Create<byte>();
				this.memory.MockIndexedIndirectMemoryWrite(randomOffset, xRegisterValue, byteToCompare);
			};

			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte) OpCode.AbsoluteLoadAccumulator, fixture.Create<byte>(),
					(byte) OpCode.TransferFromAccumulatorToY,
					(byte) OpCode.ImmediateLoadXRegister, xRegisterValue,
					(byte) OpCode.IndexedIndirectBitwiseAnd, randomOffset
				});
			};

			it["should set bitwise 'and' result on accumulator"] = () =>
			{
				this.processor.Accumulator.should_be(this.processor.Y & byteToCompare);
			};

			it["should set negative flag equal to seventh accumulator bit"] = () =>
			{
				this.processor.Negative.should_be(this.processor.Accumulator.GetBitAt(Microprocessor.NEGATIVE_FLAG_BYTE_POSITION));
			};

			it["should set zero flag when accumulator is 0"] = () =>
			{
				this.processor.Zero.should_be(this.processor.Accumulator == 0);
			};
		}

		public void When_use_indirect_indexed_addressing_mode()
		{
			var randomOffset = default(byte);
			var byteToCompare = default(byte);
			var yRegisterValue = default(byte);

			before = () =>
			{
				randomOffset = fixture.Create<byte>();
				byteToCompare = fixture.Create<byte>();
				yRegisterValue = fixture.Create<byte>();
				this.memory.MockIndirectIndexedMemoryWrite(randomOffset, yRegisterValue, byteToCompare);
			};

			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte) OpCode.AbsoluteLoadAccumulator, fixture.Create<byte>(),
					(byte) OpCode.TransferFromAccumulatorToX,
					(byte) OpCode.ImmediateLoadYRegister, yRegisterValue,
					(byte) OpCode.IndirectIndexedBitwiseAnd, randomOffset
				});
			};

			it["should set bitwise 'and' result on accumulator"] = () =>
			{
				this.processor.Accumulator.should_be(this.processor.X & byteToCompare);
			};

			it["should set negative flag equal to seventh accumulator bit"] = () =>
			{
				this.processor.Negative.should_be(this.processor.Accumulator.GetBitAt(Microprocessor.NEGATIVE_FLAG_BYTE_POSITION));
			};

			it["should set zero flag when accumulator is 0"] = () =>
			{
				this.processor.Zero.should_be(this.processor.Accumulator == 0);
			};
		}
	}
}
