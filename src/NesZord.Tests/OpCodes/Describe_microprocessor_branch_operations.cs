using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_branch_operations : nspec
	{
		private static readonly Fixture fixture = new Fixture();

		private Memory memory;

		private Microprocessor processor;

		public void before_each()
		{
			this.memory = new Memory();
			this.processor = new Microprocessor(this.memory);
		}

		public void When_branch_if_not_equal()
		{
			context["given that executed program must branch while X register is not 0x03"] = () =>
			{
				act = () =>
				{
					this.processor.RunProgram(new byte[]
					{
						(byte) OpCode.ImmediateLDX, 0x08,
						(byte) OpCode.DEX,
						(byte) OpCode.AbsoluteSTX, 0x00, 0x02,
						(byte) OpCode.ImmediateCPX, 0x03,
						(byte) OpCode.BNE, 0xf8,
						(byte) OpCode.AbsoluteSTX, 0x01, 0x02,
						(byte) OpCode.BRK
					});
				};

				it["should X register value be equal 0x03"] = () => { this.processor.X.should_be(0x03); };
				it["should value at $0200 be equal 0x03"] = () => { this.memory.Read(0x00, 0x02).should_be(0x03); };
				it["should value at $0201 be equal 0x03"] = () => { this.memory.Read(0x01, 0x02).Is(0x03); };
				it["should Carry flag turn on"] = () => { this.processor.Carry.should_be_true(); };
				it["should Zero flag turn on"] = () => { this.processor.Zero.should_be_true(); };
			};
		}

		public void When_branch_if_equal()
		{
			context["given that executed program must branch while Y register is not 0x02"] = () =>
			{
				act = () =>
				{
					this.processor.RunProgram(new byte[]
					{
						(byte) OpCode.ImmediateLDY, 0x01,
						(byte) OpCode.INY,
						(byte) OpCode.ImmediateCPY, 0x02,
						(byte) OpCode.BEQ, 0xfb,
						(byte) OpCode.AbsoluteSTY, 0x00, 0x02,
						(byte) OpCode.BRK
					});
				};

				it["should Y register value be equal 0x03"] = () => { this.processor.Y.should_be(0x03); };
				it["should value at $0200 be equal 0x03"] = () => { this.memory.Read(0x00, 0x02).should_be(0x03); };
				it["should Carry flag turn on"] = () => { this.processor.Carry.should_be_true(); };
				it["should Zero flag turn on"] = () => { this.processor.Zero.should_be_false(); };
			};
		}

		public void When_branch_if_carry_is_clear()
		{
			context["given that executed program must branch while Carry is clear"] = () =>
			{
				act = () =>
				{
					this.processor.RunProgram(new byte[]
					{
						(byte) OpCode.ImmediateLDX, 0xf5,
						(byte) OpCode.TXA,
						(byte) OpCode.AbsoluteSTA, 0x00, 0x20,
						(byte) OpCode.ImmediateADC, 0x01,
						(byte) OpCode.BCC, 0xf9,
						(byte) OpCode.AbsoluteSTA, 0x00, 0x20,
						(byte) OpCode.BRK
					});
				};

				it["should X register value be equal 0xf5"] = () => { this.processor.X.should_be(0xf5); };
				it["should Accumulator register value be equal 0x00"] = () => { this.processor.Accumulator.should_be(0x00); };
				it["should value at $0200 be equal 0x00"] = () => { this.memory.Read(0x00, 0x02).should_be(0x00); };
				it["should Carry flag turn on"] = () => { this.processor.Carry.should_be_true(); };
			};
		}

		public void When_branch_if_carry_is_set()
		{
			context["given that executed program must branch while Carry is set"] = () =>
			{
				act = () =>
				{
					this.processor.RunProgram(new byte[]
					{
						(byte) OpCode.ImmediateLDX, 0x08,
						(byte) OpCode.DEX,
						(byte) OpCode.AbsoluteSTX, 0x00, 0x02,
						(byte) OpCode.ImmediateCPX, 0x03,
						(byte) OpCode.BCS, 0xf8,
						(byte) OpCode.AbsoluteSTX, 0x01, 0x02,
						(byte) OpCode.BRK
					});
				};

				it["should X register value be equal 0x02"] = () => { this.processor.X.should_be(0x02); };
				it["should value at $0200 be equal 0x02"] = () => { this.memory.Read(0x00, 0x02).should_be(0x02); };
				it["should value at $0201 be equal 0x02"] = () => { this.memory.Read(0x01, 0x02).Is(0x02); };
				it["should Carry flag turn on"] = () => { this.processor.Carry.should_be_false(); };
			};
		}
	}
}