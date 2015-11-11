using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_sbc_operation : nspec
	{
		private static readonly Fixture fixture = new Fixture();

		private Microprocessor processor;

		private MemoryMock memory;

		public void before_each()
		{
			this.memory = new MemoryMock();
			this.processor = new Microprocessor(this.memory);
		}

		public void When_use_immediate_addressing_mode()
		{
			var byteToSubtract = default(byte);

			before = () => { byteToSubtract = fixture.Create<byte>(); };

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDA_Immediate, fixture.Create<byte>(),
					(byte)OpCode.TAX_Implied,
					(byte)OpCode.SBC_Immediate, byteToSubtract
				});
			};

			it["should subtract the specified value to accumulator"] = () =>
			{
				var resultWithCarry = processor.X - byteToSubtract;
				processor.Accumulator.should_be((byte)resultWithCarry & 0xff);
			};

			context["given that the subtracted byte is greater than Accumulator value"] = () =>
			{
				before = () => { byteToSubtract = 0xff; };
				it["should keep carry flag clear"] = () => { processor.Carry.should_be_false(); };
			};

			context["given that the subtracted byte is lower than Accumulator value"] = () =>
			{
				before = () => { byteToSubtract = 0x00; };
				it["should turn carry flag on"] = () => { processor.Carry.should_be_true(); };
			};
		}

		public void When_use_zero_page_addressing_mode()
		{
			var byteToSubtract = default(byte);
			var randomOffset = default(byte);

			before = () => 
			{
				byteToSubtract = fixture.Create<byte>();
				randomOffset = fixture.Create<byte>();
				this.memory.WriteZeroPage(randomOffset, byteToSubtract);
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDA_Immediate, fixture.Create<byte>(),
					(byte)OpCode.TAX_Implied,
					(byte)OpCode.SBC_ZeroPage, randomOffset
				});
			};

			it["should subtract the specified value to accumulator"] = () =>
			{
				var resultWithCarry = processor.X - byteToSubtract;
				processor.Accumulator.should_be((byte)resultWithCarry & 0xff);
			};

			context["given that the subtracted byte is greater than Accumulator value"] = () =>
			{
				before = () => { this.memory.WriteZeroPage(randomOffset, 0xff); };
				it["should keep carry flag clear"] = () => { processor.Carry.should_be_false(); };
			};

			context["given that the subtracted byte is lower than Accumulator value"] = () =>
			{
				before = () => { this.memory.WriteZeroPage(randomOffset, 0x00); };
				it["should turn carry flag on"] = () => { processor.Carry.should_be_true(); };
			};
		}

		public void When_use_zero_page_x_addressing_mode()
		{
			var byteToSubtract = default(byte);
			var randomOffset = default(byte);
			var xRegisterValue = default(byte);

			before = () =>
			{
				byteToSubtract = fixture.Create<byte>();
				randomOffset = fixture.Create<byte>();
				xRegisterValue = fixture.Create<byte>();
				this.memory.WriteZeroPage((byte)(xRegisterValue + randomOffset), byteToSubtract);
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDA_Immediate, fixture.Create<byte>(),
					(byte)OpCode.TAY_Implied,
					(byte)OpCode.LDX_Immediate, xRegisterValue,
					(byte)OpCode.SBC_ZeroPageX, randomOffset
				});
			};

			it["should subtract the specified value to accumulator"] = () =>
			{
				var resultWithCarry = processor.Y - byteToSubtract;
				processor.Accumulator.should_be((byte)resultWithCarry & 0xff);
			};

			context["given that the subtracted byte is greater than Accumulator value"] = () =>
			{
				before = () => { this.memory.WriteZeroPage((byte)(xRegisterValue + randomOffset), 0xff); };
				it["should keep carry flag clear"] = () => { processor.Carry.should_be_false(); };
			};

			context["given that the subtracted byte is lower than Accumulator value"] = () =>
			{
				before = () => { this.memory.WriteZeroPage((byte)(xRegisterValue + randomOffset), 0x00); };
				it["should turn carry flag on"] = () => { processor.Carry.should_be_true(); };
			};
		}

		public void When_use_absolute_addressing_mode()
		{
			var byteToSubtract = default(byte);
			var randomPage = default(byte);
			var randomOffset = default(byte);

			before = () =>
			{
				byteToSubtract = fixture.Create<byte>();
				randomPage = fixture.Create<byte>();
				randomOffset = fixture.Create<byte>();
				this.memory.Write(randomOffset, randomPage, byteToSubtract);
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDA_Immediate, fixture.Create<byte>(),
					(byte)OpCode.TAY_Implied,
					(byte)OpCode.SBC_Absolute, randomOffset, randomPage
				});
			};

			it["should subtract the specified value to accumulator"] = () =>
			{
				var resultWithCarry = processor.Y - byteToSubtract;
				processor.Accumulator.should_be((byte)resultWithCarry & 0xff);
			};

			context["given that the subtracted byte is greater than Accumulator value"] = () =>
			{
				before = () => { this.memory.Write(randomOffset, randomPage, 0xff); };
				it["should keep carry flag clear"] = () => { processor.Carry.should_be_false(); };
			};

			context["given that the subtracted byte is lower than Accumulator value"] = () =>
			{
				before = () => { this.memory.Write(randomOffset, randomPage, 0x00); };
				it["should turn carry flag on"] = () => { processor.Carry.should_be_true(); };
			};
		}

		public void When_use_absolute_x_addressing_mode()
		{
			var byteToSubtract = default(byte);
			var randomPage = default(byte);
			var randomOffset = default(byte);
			var xRegisterValue = default(byte);

			before = () =>
			{
				byteToSubtract = fixture.Create<byte>();
				randomPage = fixture.Create<byte>();
				randomOffset = fixture.Create<byte>();
				xRegisterValue = fixture.Create<byte>();
				this.memory.Write((byte)(xRegisterValue + randomOffset), randomPage, byteToSubtract);
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDA_Immediate, fixture.Create<byte>(),
					(byte)OpCode.TAY_Implied,
					(byte)OpCode.LDX_Immediate, xRegisterValue,
					(byte)OpCode.SBC_AbsoluteX, randomOffset, randomPage
				});
			};

			it["should subtract the specified value to accumulator"] = () =>
			{
				var resultWithCarry = processor.Y - byteToSubtract;
				processor.Accumulator.should_be((byte)resultWithCarry & 0xff);
			};

			context["given that the subtracted byte is greater than Accumulator value"] = () =>
			{
				before = () => { this.memory.Write((byte)(xRegisterValue + randomOffset), randomPage, 0xff); };
				it["should keep carry flag clear"] = () => { processor.Carry.should_be_false(); };
			};

			context["given that the subtracted byte is lower than Accumulator value"] = () =>
			{
				before = () => { this.memory.Write((byte)(xRegisterValue + randomOffset), randomPage, 0x00); };
				it["should turn carry flag on"] = () => { processor.Carry.should_be_true(); };
			};
		}

		public void When_use_absolute_y_addressing_mode()
		{
			var byteToSubtract = default(byte);
			var randomPage = default(byte);
			var randomOffset = default(byte);
			var yRegisterValue = default(byte);

			before = () =>
			{
				byteToSubtract = fixture.Create<byte>();
				randomPage = fixture.Create<byte>();
				randomOffset = fixture.Create<byte>();
				yRegisterValue = fixture.Create<byte>();
				this.memory.Write((byte)(yRegisterValue + randomOffset), randomPage, byteToSubtract);
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDA_Immediate, fixture.Create<byte>(),
					(byte)OpCode.TAX_Implied,
					(byte)OpCode.LDY_Immediate, yRegisterValue,
					(byte)OpCode.SBC_AbsoluteY, randomOffset, randomPage
				});
			};

			it["should subtract the specified value to accumulator"] = () =>
			{
				var resultWithCarry = processor.X - byteToSubtract;
				processor.Accumulator.should_be((byte)resultWithCarry & 0xff);
			};

			context["given that the subtracted byte is greater than Accumulator value"] = () =>
			{
				before = () => { this.memory.Write((byte)(yRegisterValue + randomOffset), randomPage, 0xff); };
				it["should keep carry flag clear"] = () => { processor.Carry.should_be_false(); };
			};

			context["given that the subtracted byte is lower than Accumulator value"] = () =>
			{
				before = () => { this.memory.Write((byte)(yRegisterValue + randomOffset), randomPage, 0x00); };
				it["should turn carry flag on"] = () => { processor.Carry.should_be_true(); };
			};
		}

		public void When_use_indexed_indirect_addressing_mode()
		{
			var byteToSubtract = default(byte);
			var randomOffset = default(byte);
			var xRegisterValue = default(byte);

			before = () =>
			{
				byteToSubtract = fixture.Create<byte>();
				randomOffset = fixture.Create<byte>();
				xRegisterValue = fixture.Create<byte>();
				this.memory.MockIndexedIndirectMemoryWrite(randomOffset, xRegisterValue, byteToSubtract);
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDA_Immediate, fixture.Create<byte>(),
					(byte)OpCode.TAY_Implied,
					(byte)OpCode.LDX_Immediate, xRegisterValue,
					(byte)OpCode.SBC_IndexedIndirect, randomOffset
				});
			};

			it["should subtract the specified value to accumulator"] = () =>
			{
				var resultWithCarry = processor.Y - byteToSubtract;
				processor.Accumulator.should_be((byte)resultWithCarry & 0xff);
			};

			context["given that the subtracted byte is greater than Accumulator value"] = () =>
			{
				before = () => { this.memory.MockIndexedIndirectMemoryWrite(randomOffset, xRegisterValue, 0xff); };
				it["should keep carry flag clear"] = () => { processor.Carry.should_be_false(); };
			};

			context["given that the subtracted byte is lower than Accumulator value"] = () =>
			{
				before = () => { this.memory.MockIndexedIndirectMemoryWrite(randomOffset, xRegisterValue, 0x00); };
				it["should turn carry flag on"] = () => { processor.Carry.should_be_true(); };
			};
		}

		public void When_use_indirect_indexed_addressing_mode()
		{
			var byteToSubtract = default(byte);
			var randomOffset = default(byte);
			var yRegisterValue = default(byte);

			before = () =>
			{
				byteToSubtract = fixture.Create<byte>();
				randomOffset = fixture.Create<byte>();
				yRegisterValue = fixture.Create<byte>();
				this.memory.MockIndirectIndexedMemoryWrite(randomOffset, yRegisterValue, byteToSubtract);
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDA_Immediate, fixture.Create<byte>(),
					(byte)OpCode.TAX_Implied,
					(byte)OpCode.LDY_Immediate, yRegisterValue,
					(byte)OpCode.SBC_IndirectIndexed, randomOffset
				});
			};

			it["should subtract the specified value to accumulator"] = () =>
			{
				var resultWithCarry = processor.X - byteToSubtract;
				processor.Accumulator.should_be((byte)resultWithCarry & 0xff);
			};

			context["given that the subtracted byte is greater than Accumulator value"] = () =>
			{
				before = () => { this.memory.MockIndirectIndexedMemoryWrite(randomOffset, yRegisterValue, 0xff); };
				it["should keep carry flag clear"] = () => { processor.Carry.should_be_false(); };
			};

			context["given that the subtracted byte is lower than Accumulator value"] = () =>
			{
				before = () => { this.memory.MockIndirectIndexedMemoryWrite(randomOffset, yRegisterValue, 0x00); };
				it["should turn carry flag on"] = () => { processor.Carry.should_be_true(); };
			};
		}
	}
}
