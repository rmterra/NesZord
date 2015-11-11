using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_adc_operation : nspec
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
			var byteToAdd = default(byte);

			before = () => { byteToAdd = fixture.Create<byte>(); };

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDA_Immediate, fixture.Create<byte>(),
					(byte)OpCode.TAX_Implied,
					(byte)OpCode.ADC_Immediate, byteToAdd
				});
			};

			it["should add the specified value to accumulator"] = () =>
			{
				var resultWithCarry = processor.X + byteToAdd;
				processor.Accumulator.should_be((byte)resultWithCarry & 0xff);
			};

			context["given a byte greater than #ff"] = () =>
			{
				before = () => { byteToAdd = 0xff; };
				it["should turn on carry flag"] = () => { processor.Carry.should_be(true); };
			};

			context["given a byte lower than #ff"] = () =>
			{
				before = () => { byteToAdd = 0x00; };
				it["should not turn on carry flag"] = () => { processor.Carry.should_be_false(); };
			};
		}

		public void When_use_zero_page_addressing_mode()
		{
			var byteToAdd = default(byte);
			var randomOffset = default(byte);

			before = () =>
			{
				byteToAdd = fixture.Create<byte>();
				randomOffset = fixture.Create<byte>();
				this.memory.WriteZeroPage(randomOffset, byteToAdd);
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDA_Immediate, fixture.Create<byte>(),
					(byte)OpCode.TAX_Implied,
					(byte)OpCode.ADC_ZeroPage, randomOffset
				});
			};

			it["should add the specified value to accumulator"] = () =>
			{
				var resultWithCarry = processor.X + byteToAdd;
				processor.Accumulator.should_be((byte)resultWithCarry & 0xff);
			};

			context["given a byte greater than #ff"] = () =>
			{
				before = () => { this.memory.WriteZeroPage(randomOffset, 0xff); };
				it["should turn on carry flag"] = () => { processor.Carry.should_be(true); };
			};

			context["given a byte lower than #ff"] = () =>
			{
				before = () => { this.memory.WriteZeroPage(randomOffset, 0x00); };
				it["should not turn on carry flag"] = () => { processor.Carry.should_be_false(); };
			};
		}

		public void When_use_zero_page_x_addressing_mode()
		{
			var byteToAdd = default(byte);
			var xRegisterValue = default(byte);
			var randomOffset = default(byte);

			before = () =>
			{
				byteToAdd = fixture.Create<byte>();
				xRegisterValue = fixture.Create<byte>();
				randomOffset = fixture.Create<byte>();
				this.memory.WriteZeroPage((byte)(xRegisterValue + randomOffset), byteToAdd);
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDA_Immediate, fixture.Create<byte>(),
					(byte)OpCode.TAY_Implied,
					(byte)OpCode.LDX_Immediate, xRegisterValue,
					(byte)OpCode.ADC_ZeroPageX, randomOffset
				});
			};

			it["should add the specified value to accumulator"] = () =>
			{
				var resultWithCarry = processor.Y + byteToAdd;
				processor.Accumulator.should_be((byte)resultWithCarry & 0xff);
			};

			context["given a byte greater than #ff"] = () =>
			{
				before = () => { this.memory.WriteZeroPage((byte)(xRegisterValue + randomOffset), 0xff); };
				it["should turn on carry flag"] = () => { processor.Carry.should_be(true); };
			};

			context["given a byte lower than #ff"] = () =>
			{
				before = () => { this.memory.WriteZeroPage((byte)(xRegisterValue + randomOffset), 0x00); };
				it["should not turn on carry flag"] = () => { processor.Carry.should_be_false(); };
			};
		}

		public void When_use_absolute_addressing_mode()
		{
			var byteToAdd = default(byte);
			var randomPage = default(byte);
			var randomOffset = default(byte);

			before = () =>
			{
				byteToAdd = fixture.Create<byte>();
				randomPage = fixture.Create<byte>();
				randomOffset = fixture.Create<byte>();
				this.memory.Write(randomOffset, randomPage, byteToAdd);
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDA_Immediate, fixture.Create<byte>(),
					(byte)OpCode.TAY_Implied,
					(byte)OpCode.ADC_Absolute, randomOffset, randomPage
				});
			};

			it["should add the specified value to accumulator"] = () =>
			{
				var resultWithCarry = processor.Y + byteToAdd;
				processor.Accumulator.should_be((byte)resultWithCarry & 0xff);
			};

			context["given a byte greater than #ff"] = () =>
			{
				before = () => { this.memory.Write(randomOffset, randomPage, 0xff); };
				it["should turn on carry flag"] = () => { processor.Carry.should_be(true); };
			};

			context["given a byte lower than #ff"] = () =>
			{
				before = () => { this.memory.Write(randomOffset, randomPage, 0x00); };
				it["should not turn on carry flag"] = () => { processor.Carry.should_be_false(); };
			};
		}

		public void When_use_absolute_x_addressing_mode()
		{
			var byteToAdd = default(byte);
			var randomPage = default(byte);
			var randomOffset = default(byte);
			var xRegisterValue = default(byte);

			before = () =>
			{
				byteToAdd = fixture.Create<byte>();
				randomPage = fixture.Create<byte>();
				randomOffset = fixture.Create<byte>();
				xRegisterValue = fixture.Create<byte>();
				this.memory.Write((byte)(xRegisterValue + randomOffset), randomPage, byteToAdd);
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDA_Immediate, fixture.Create<byte>(),
					(byte)OpCode.TAY_Implied,
					(byte)OpCode.LDX_Immediate, xRegisterValue,
					(byte)OpCode.ADC_AbsoluteX, randomOffset, randomPage
				});
			};

			it["should add the specified value to accumulator"] = () =>
			{
				var resultWithCarry = processor.Y + byteToAdd;
				processor.Accumulator.should_be((byte)resultWithCarry & 0xff);
			};

			context["given a byte greater than #ff"] = () =>
			{
				before = () => { this.memory.Write((byte)(xRegisterValue + randomOffset), randomPage, 0xff); };
				it["should turn on carry flag"] = () => { processor.Carry.should_be(true); };
			};

			context["given a byte lower than #ff"] = () =>
			{
				before = () => { this.memory.Write((byte)(xRegisterValue + randomOffset), randomPage, 0x00); };
				it["should not turn on carry flag"] = () => { processor.Carry.should_be_false(); };
			};
		}

		public void When_use_absolute_y_addressing_mode()
		{
			var byteToAdd = default(byte);
			var randomPage = default(byte);
			var randomOffset = default(byte);
			var yRegisterValue = default(byte);

			before = () =>
			{
				byteToAdd = fixture.Create<byte>();
				randomPage = fixture.Create<byte>();
				randomOffset = fixture.Create<byte>();
				yRegisterValue = fixture.Create<byte>();

				this.memory.Write((byte)(yRegisterValue + randomOffset), randomPage, byteToAdd);
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDA_Immediate, fixture.Create<byte>(),
					(byte)OpCode.TAX_Implied,
					(byte)OpCode.LDY_Immediate, yRegisterValue,
					(byte)OpCode.ADC_AbsoluteY, randomOffset, randomPage
				});
			};

			it["should add the specified value to accumulator"] = () =>
			{
				var resultWithCarry = processor.X + byteToAdd;
				processor.Accumulator.should_be((byte)resultWithCarry & 0xff);
			};

			context["given a byte greater than #ff"] = () =>
			{
				before = () => { this.memory.Write((byte)(yRegisterValue + randomOffset), randomPage, 0xff); };
				it["should turn on carry flag"] = () => { processor.Carry.should_be(true); };
			};

			context["given a byte lower than #ff"] = () =>
			{
				before = () => { this.memory.Write((byte)(yRegisterValue + randomOffset), randomPage, 0x00); };
				it["should not turn on carry flag"] = () => { processor.Carry.should_be_false(); };
			};
		}

		public void When_use_indexed_indirect_addressing_mode()
		{
			var byteToAdd = default(byte);
			var xRegisterValue = default(byte);
			var randomOffset = default(byte);

			before = () =>
			{
				byteToAdd = fixture.Create<byte>();
				xRegisterValue = fixture.Create<byte>();
				randomOffset = fixture.Create<byte>();

				this.memory.MockIndexedIndirectMemoryWrite(randomOffset, xRegisterValue, byteToAdd);
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDA_Immediate, fixture.Create<byte>(),
					(byte)OpCode.TAY_Implied,
					(byte)OpCode.LDX_Immediate, xRegisterValue,
					(byte)OpCode.ADC_IndexedIndirect, randomOffset
				});
			};

			it["should add the specified value to accumulator"] = () =>
			{
				var resultWithCarry = processor.Y + byteToAdd;
				processor.Accumulator.should_be((byte)resultWithCarry & 0xff);
			};

			context["given a byte greater than #ff"] = () =>
			{
				before = () => { this.memory.MockIndexedIndirectMemoryWrite(randomOffset, xRegisterValue, 0xff); };
				it["should turn on carry flag"] = () => { processor.Carry.should_be(true); };
			};

			context["given a byte lower than #ff"] = () =>
			{
				before = () => { this.memory.MockIndexedIndirectMemoryWrite(randomOffset, xRegisterValue, 0x00); };
				it["should not turn on carry flag"] = () => { processor.Carry.should_be_false(); };
			};
		}

		public void When_use_indirect_indexed_addressing_mode()
		{
			var byteToAdd = default(byte);
			var yRegisterValue = default(byte);
			var randomOffset = default(byte);

			before = () =>
			{
				byteToAdd = fixture.Create<byte>();
				yRegisterValue = fixture.Create<byte>();
				randomOffset = fixture.Create<byte>();

				this.memory.MockIndirectIndexedMemoryWrite(randomOffset, yRegisterValue, byteToAdd);
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDA_Immediate, fixture.Create<byte>(),
					(byte)OpCode.TAX_Implied,
					(byte)OpCode.LDY_Immediate, yRegisterValue,
					(byte)OpCode.ADC_IndirectIndexed, randomOffset
				});
			};

			it["should add the specified value to accumulator"] = () =>
			{
				var resultWithCarry = processor.X + byteToAdd;
				processor.Accumulator.should_be((byte)resultWithCarry & 0xff);
			};

			context["given a byte greater than #ff"] = () =>
			{
				before = () => { this.memory.MockIndirectIndexedMemoryWrite(randomOffset, yRegisterValue, 0xff); };
				it["should turn on carry flag"] = () => { processor.Carry.should_be(true); };
			};

			context["given a byte lower than #ff"] = () =>
			{
				before = () => { this.memory.MockIndirectIndexedMemoryWrite(randomOffset, yRegisterValue, 0x00); };
				it["should not turn on carry flag"] = () => { processor.Carry.should_be_false(); };
			};
		}
	}
}