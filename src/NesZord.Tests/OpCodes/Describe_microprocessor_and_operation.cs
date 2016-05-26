using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_and_operation : nspec
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
					(byte) OpCode.LDA_Absolute, fixture.Create<byte>(),
					(byte) OpCode.TAX_Implied,
					(byte) OpCode.AND_Immediate, byteToCompare
				});
			};

			this.DefineSpecs(byteToCompare);
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
					(byte) OpCode.LDA_Absolute, fixture.Create<byte>(),
					(byte) OpCode.TAX_Implied,
					(byte) OpCode.AND_ZeroPage, randomOffset
				});
			};

			this.DefineSpecs(byteToCompare);
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
					(byte) OpCode.LDA_Absolute, fixture.Create<byte>(),
					(byte) OpCode.TAY_Implied,
					(byte) OpCode.LDX_Immediate, xRegisterValue,
					(byte) OpCode.AND_ZeroPageX, randomOffset
				});
			};

			this.DefineSpecs(byteToCompare);
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
				this.memory.Write(randomOffset, randomPage, byteToCompare);
			};

			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte) OpCode.LDA_Absolute, fixture.Create<byte>(),
					(byte) OpCode.TAX_Implied,
					(byte) OpCode.AND_Absolute, randomOffset, randomPage
				});
			};

			this.DefineSpecs(byteToCompare);
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
				this.memory.Write((byte)(xRegisterValue + randomOffset), randomPage, byteToCompare);
			};

			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte) OpCode.LDA_Absolute, fixture.Create<byte>(),
					(byte) OpCode.TAY_Implied,
					(byte) OpCode.LDX_Immediate, xRegisterValue,
					(byte) OpCode.AND_AbsoluteX, randomOffset, randomPage
				});
			};

			this.DefineSpecs(byteToCompare);
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
				this.memory.Write((byte)(yRegisterValue + randomOffset), randomPage, byteToCompare);
			};

			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte) OpCode.LDA_Absolute, fixture.Create<byte>(),
					(byte) OpCode.TAX_Implied,
					(byte) OpCode.LDY_Immediate, yRegisterValue,
					(byte) OpCode.AND_AbsoluteY, randomOffset, randomPage
				});
			};

			this.DefineSpecs(byteToCompare);
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
					(byte) OpCode.LDA_Absolute, fixture.Create<byte>(),
					(byte) OpCode.TAY_Implied,
					(byte) OpCode.LDX_Immediate, xRegisterValue,
					(byte) OpCode.AND_IndexedIndirect, randomOffset
				});
			};

			this.DefineSpecs(byteToCompare);
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
					(byte) OpCode.LDA_Absolute, fixture.Create<byte>(),
					(byte) OpCode.TAX_Implied,
					(byte) OpCode.LDY_Immediate, yRegisterValue,
					(byte) OpCode.AND_IndirectIndexed, randomOffset
				});
			};

			this.DefineSpecs(byteToCompare);
		}

		private void DefineSpecs(byte byteToCompare)
		{
			it["should set bitwise 'and' result on accumulator"] = () =>
			{
				this.processor.Accumulator.Value.should_be(this.processor.X.And(byteToCompare));
			};

			it["should set negative flag equal to seventh accumulator bit"] = () =>
			{
				this.processor.Negative.should_be(this.processor.Accumulator.IsSignBitSet);
			};

			it["should set zero flag when accumulator is 0"] = () =>
			{
				this.processor.Zero.should_be(this.processor.Accumulator.IsValueEqualZero);
			};
		}
	}
}