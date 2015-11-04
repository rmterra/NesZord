using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesZord.Tests
{
	public class Describe_microprocessor_add_with_carry_operation : nspec
	{
		private static readonly Fixture fixture = new Fixture();

		private Microprocessor processor;

		private Memory memory;

		public void before_each()
		{
			this.memory = new Memory();
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
					(byte)OpCode.ImmediateLoadAccumulator, fixture.Create<byte>(),
					(byte)OpCode.TransferFromAccumulatorToX,
					(byte)OpCode.ImmediateAddWithCarry, byteToAdd
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
					(byte)OpCode.ImmediateLoadAccumulator, fixture.Create<byte>(),
					(byte)OpCode.TransferFromAccumulatorToX,
					(byte)OpCode.ZeroPageAddWithCarry, randomOffset
				});
			};

			it["should add the specified value to accumulator"] = () =>
			{
				var resultWithCarry = processor.X + byteToAdd;
				processor.Accumulator.should_be((byte)resultWithCarry & 0xff);
			};

			context["given a byte greater than #ff"] = () =>
			{
				before = () =>
				{
					byteToAdd = 0xff;
					this.memory.WriteZeroPage(randomOffset, byteToAdd);
				};
				it["should turn on carry flag"] = () => { processor.Carry.should_be(true); };
			};

			context["given a byte lower than #ff"] = () =>
			{
				before = () =>
				{
					byteToAdd = 0x00;
					this.memory.WriteZeroPage(randomOffset, byteToAdd);
				};
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
					(byte)OpCode.ImmediateLoadAccumulator, fixture.Create<byte>(),
					(byte)OpCode.TransferFromAccumulatorToY,
					(byte)OpCode.ImmediateLoadXRegister, xRegisterValue,
					(byte)OpCode.ZeroPageXAddWithCarry, randomOffset
				});
			};

			it["should add the specified value to accumulator"] = () =>
			{
				var resultWithCarry = processor.Y + byteToAdd;
				processor.Accumulator.should_be((byte)resultWithCarry & 0xff);
			};

			context["given a byte greater than #ff"] = () =>
			{
				before = () =>
				{
					byteToAdd = 0xff;
					this.memory.WriteZeroPage((byte)(xRegisterValue + randomOffset), byteToAdd);
				};
				it["should turn on carry flag"] = () => { processor.Carry.should_be(true); };
			};

			context["given a byte lower than #ff"] = () =>
			{
				before = () =>
				{
					byteToAdd = 0x00;
					this.memory.WriteZeroPage((byte)(xRegisterValue + randomOffset), byteToAdd);
				};
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
				this.memory.Write(randomPage, randomOffset, byteToAdd);
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.ImmediateLoadAccumulator, fixture.Create<byte>(),
					(byte)OpCode.TransferFromAccumulatorToY,
					(byte)OpCode.AbsoluteAddWithCarry, randomOffset, randomPage
				});
			};

			it["should add the specified value to accumulator"] = () =>
			{
				var resultWithCarry = processor.Y + byteToAdd;
				processor.Accumulator.should_be((byte)resultWithCarry & 0xff);
			};

			context["given a byte greater than #ff"] = () =>
			{
				before = () =>
				{
					byteToAdd = 0xff;
					this.memory.Write(randomPage, randomOffset, byteToAdd);
				};
				it["should turn on carry flag"] = () => { processor.Carry.should_be(true); };
			};

			context["given a byte lower than #ff"] = () =>
			{
				before = () =>
				{
					byteToAdd = 0x00;
					this.memory.Write(randomPage, randomOffset, byteToAdd);
				};
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
				this.memory.Write(randomPage, (byte)(xRegisterValue + randomOffset), byteToAdd);
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.ImmediateLoadAccumulator, fixture.Create<byte>(),
					(byte)OpCode.TransferFromAccumulatorToY,
					(byte)OpCode.ImmediateLoadXRegister, xRegisterValue,
					(byte)OpCode.AbsoluteXAddWithCarry, randomOffset, randomPage
				});
			};

			it["should add the specified value to accumulator"] = () =>
			{
				var resultWithCarry = processor.Y + byteToAdd;
				processor.Accumulator.should_be((byte)resultWithCarry & 0xff);
			};

			context["given a byte greater than #ff"] = () =>
			{
				before = () =>
				{
					byteToAdd = 0xff;
					this.memory.Write(randomPage, (byte)(xRegisterValue + randomOffset), byteToAdd);
				};
				it["should turn on carry flag"] = () => { processor.Carry.should_be(true); };
			};

			context["given a byte lower than #ff"] = () =>
			{
				before = () =>
				{
					byteToAdd = 0x00;
					this.memory.Write(randomPage, (byte)(xRegisterValue + randomOffset), byteToAdd);
				};
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
				this.memory.Write(randomPage, (byte)(yRegisterValue + randomOffset), byteToAdd);
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.ImmediateLoadAccumulator, fixture.Create<byte>(),
					(byte)OpCode.TransferFromAccumulatorToX,
					(byte)OpCode.ImmediateLoadYRegister, yRegisterValue,
					(byte)OpCode.AbsoluteYAddWithCarry, randomOffset, randomPage
				});
			};

			it["should add the specified value to accumulator"] = () =>
			{
				var resultWithCarry = processor.X + byteToAdd;
				processor.Accumulator.should_be((byte)resultWithCarry & 0xff);
			};

			context["given a byte greater than #ff"] = () =>
			{
				before = () =>
				{
					byteToAdd = 0xff;
					this.memory.Write(randomPage, (byte)(yRegisterValue + randomOffset), byteToAdd);
				};
				it["should turn on carry flag"] = () => { processor.Carry.should_be(true); };
			};

			context["given a byte lower than #ff"] = () =>
			{
				before = () =>
				{
					byteToAdd = 0x00;
					this.memory.Write(randomPage, (byte)(yRegisterValue + randomOffset), byteToAdd);
				};
				it["should not turn on carry flag"] = () => { processor.Carry.should_be_false(); };
			};
		}

		public void When_use_indexed_indirect_addressing_mode()
		{
			var byteToAdd = default(byte);
			var xRegisterValue = default(byte);
			var indirectPage = default(byte);
			var indirectOffset = default(byte);
			var randomOffset = default(byte);

			before = () =>
			{
				byteToAdd = fixture.Create<byte>();
				xRegisterValue = fixture.Create<byte>();
				indirectPage = fixture.Create<byte>();
				indirectOffset = fixture.Create<byte>();
				randomOffset = fixture.Create<byte>();

				var computedOffset = (byte)(xRegisterValue + randomOffset);
				this.memory.Write(Memory.ZERO_PAGE, computedOffset, indirectPage);
				this.memory.Write(Memory.ZERO_PAGE, (byte)(computedOffset + 1), indirectOffset);
				this.memory.Write(indirectPage, indirectOffset, byteToAdd);
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.ImmediateLoadAccumulator, fixture.Create<byte>(),
					(byte)OpCode.TransferFromAccumulatorToY,
					(byte)OpCode.ImmediateLoadXRegister, xRegisterValue,
					(byte)OpCode.IndexedIndirectAddWithCarry, randomOffset
				});
			};

			it["should add the specified value to accumulator"] = () =>
			{
				var resultWithCarry = processor.Y + byteToAdd;
				processor.Accumulator.should_be((byte)resultWithCarry & 0xff);
			};

			context["given a byte greater than #ff"] = () =>
			{
				before = () =>
				{
					byteToAdd = 0xff;

					this.memory.Write(Memory.ZERO_PAGE, randomOffset, indirectPage);
					this.memory.Write(Memory.ZERO_PAGE, (byte)(randomOffset + 1), indirectOffset);
					this.memory.Write(indirectPage, indirectOffset, byteToAdd);
				};
				it["should turn on carry flag"] = () => { processor.Carry.should_be(true); };
			};

			context["given a byte lower than #ff"] = () =>
			{
				before = () =>
				{
					byteToAdd = 0x00;

					this.memory.Write(Memory.ZERO_PAGE, randomOffset, indirectPage);
					this.memory.Write(Memory.ZERO_PAGE, (byte)(randomOffset + 1), indirectOffset);
					this.memory.Write(indirectPage, indirectOffset, byteToAdd);
				};
				it["should not turn on carry flag"] = () => { processor.Carry.should_be_false(); };
			};
		}

		public void When_use_indirect_indexed_addressing_mode()
		{
			var byteToAdd = default(byte);
			var yRegisterValue = default(byte);
			var randomOffset = default(byte);
			var indirectPage = default(byte);
			var indirectOffset = default(byte);

			before = () =>
			{
				byteToAdd = fixture.Create<byte>();
				yRegisterValue = fixture.Create<byte>();
				randomOffset = fixture.Create<byte>();
				indirectPage = fixture.Create<byte>();
				indirectOffset = fixture.Create<byte>();

				this.memory.Write(Memory.ZERO_PAGE, randomOffset, indirectPage);
				this.memory.Write(Memory.ZERO_PAGE, (byte)(randomOffset + 1), indirectOffset);
				this.memory.Write((byte)(indirectPage + yRegisterValue), indirectOffset, byteToAdd);
			};

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.ImmediateLoadAccumulator, fixture.Create<byte>(),
					(byte)OpCode.TransferFromAccumulatorToX,
					(byte)OpCode.ImmediateLoadYRegister, yRegisterValue,
					(byte)OpCode.IndirectIndexedAddWithCarry, randomOffset
				});
			};

			it["should add the specified value to accumulator"] = () =>
			{
				var resultWithCarry = processor.X + byteToAdd;
				processor.Accumulator.should_be((byte)resultWithCarry & 0xff);
			};

			context["given a byte greater than #ff"] = () =>
			{
				before = () =>
				{
					byteToAdd = 0xff;

					this.memory.Write(Memory.ZERO_PAGE, randomOffset, indirectPage);
					this.memory.Write(Memory.ZERO_PAGE, (byte)(randomOffset + 1), indirectOffset);
					this.memory.Write((byte)(indirectPage + yRegisterValue), indirectOffset, byteToAdd);
				};
				it["should turn on carry flag"] = () => { processor.Carry.should_be(true); };
			};

			context["given a byte lower than #ff"] = () =>
			{
				before = () =>
				{
					byteToAdd = 0x00;

					this.memory.Write(Memory.ZERO_PAGE, randomOffset, indirectPage);
					this.memory.Write(Memory.ZERO_PAGE, (byte)(randomOffset + 1), indirectOffset);
					this.memory.Write((byte)(indirectPage + yRegisterValue), indirectOffset, byteToAdd);
				};
				it["should not turn on carry flag"] = () => { processor.Carry.should_be_false(); };
			};
		}
	}
}