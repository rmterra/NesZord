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
						(byte) OpCode.LDX_Immediate, 0x08,
						(byte) OpCode.DEX_Implied,
						(byte) OpCode.STX_Absolute, 0x00, 0x02,
						(byte) OpCode.CPX_Immediate, 0x03,
						(byte) OpCode.BNE_Relative, 0xf8,
						(byte) OpCode.STX_Absolute, 0x01, 0x02,
						(byte) OpCode.BRK_Implied
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
						(byte) OpCode.LDY_Immediate, 0x01,
						(byte) OpCode.INY_Implied,
						(byte) OpCode.CPY_Immediate, 0x02,
						(byte) OpCode.BEQ_Relative, 0xfb,
						(byte) OpCode.STY_Absolute, 0x00, 0x02,
						(byte) OpCode.BRK_Implied
					});
				};

				it["should Y register value be equal 0x03"] = () => { this.processor.Y.Value.should_be(0x03); };
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
						(byte) OpCode.LDX_Immediate, 0xf5,
						(byte) OpCode.TXA_Implied,
						(byte) OpCode.STA_Absolute, 0x00, 0x20,
						(byte) OpCode.ADC_Immediate, 0x01,
						(byte) OpCode.BCC_Relative, 0xf9,
						(byte) OpCode.STA_Absolute, 0x00, 0x20,
						(byte) OpCode.BRK_Implied
					});
				};

				it["should X register value be equal 0xf5"] = () => { this.processor.X.should_be(0xf5); };
				it["should Accumulator register value be equal 0x00"] = () => { this.processor.Accumulator.Value.should_be(0x00); };
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
						(byte) OpCode.LDX_Immediate, 0x08,
						(byte) OpCode.DEX_Implied,
						(byte) OpCode.STX_Absolute, 0x00, 0x02,
						(byte) OpCode.CPX_Immediate, 0x03,
						(byte) OpCode.BCS_Relative, 0xf8,
						(byte) OpCode.STX_Absolute, 0x01, 0x02,
						(byte) OpCode.BRK_Implied
					});
				};

				it["should X register value be equal 0x02"] = () => { this.processor.X.should_be(0x02); };
				it["should value at $0200 be equal 0x02"] = () => { this.memory.Read(0x00, 0x02).should_be(0x02); };
				it["should value at $0201 be equal 0x02"] = () => { this.memory.Read(0x01, 0x02).Is(0x02); };
				it["should Carry flag turn on"] = () => { this.processor.Carry.should_be_false(); };
			};
		}

		public void When_branch_if_negative_is_clear()
		{
			context["given that executed program must branch while Negative is clear"] = () =>
			{
				act = () =>
				{
					this.processor.RunProgram(new byte[]
					{
						(byte) OpCode.LDX_Immediate, 0x03,
						(byte) OpCode.DEX_Implied,
						(byte) OpCode.BPL_Relative, 0xfd,
						(byte) OpCode.BRK_Implied
					});
				};

				it["should X register value be equal 0xff"] = () => { this.processor.X.should_be(0xff); };
				it["should Negative flag turn on"] = () => { this.processor.Negative.should_be_true(); };
			};
		}

		public void When_branch_if_negative_is_set()
		{
			context["given that executed program must branch while Negative is set"] = () =>
			{
				act = () =>
				{
					this.processor.RunProgram(new byte[]
					{
						(byte) OpCode.LDX_Immediate, 0xfc,
						(byte) OpCode.INX_Implied,
						(byte) OpCode.BMI_Relative, 0xfd,
						(byte) OpCode.BRK_Implied
					});
				};

				it["should X register value be equal 0x00"] = () => { this.processor.X.should_be(0x00); };
				it["should Negative flag turn off"] = () => { this.processor.Negative.should_be_false(); };
				it["should Zero flag turn on"] = () => { this.processor.Zero.should_be_true(); };
			};
		}

		public void When_branch_if_overflow_is_clear()
		{
			context["given that executed program must branch while Overflow is clear"] = () =>
			{
				act = () =>
				{
					this.processor.RunProgram(new byte[]
					{
						(byte) OpCode.LDA_Immediate, 0xfd,
						(byte) OpCode.ADC_Immediate, 0x01,
						(byte) OpCode.BVC_Relative, 0xfc,
						(byte) OpCode.BRK_Implied
					});
				};

				it["should accumulator value be equal 0x00"] = () => { this.processor.Accumulator.Value.should_be(0x00); };
				it["should set Overflow flag"] = () => { this.processor.Overflow.should_be_true(); };
			};
		}

		public void When_branch_if_overflow_is_set()
		{
			context["given that executed program must branch while Overflow is set"] = () =>
			{
				act = () =>
				{
					this.processor.RunProgram(new byte[]
					{
						(byte) OpCode.LDA_Immediate, 0x7f,
						(byte) OpCode.ADC_Immediate, 0x01,
						(byte) OpCode.BVS_Relative, 0xfc,
						(byte) OpCode.BRK_Implied
					});
				};

				it["should accumulator value be equal 0x81"] = () => { this.processor.Accumulator.Value.should_be(0x81); };
				it["should not set Negative flag"] = () => { this.processor.Negative.should_be_true(); };
				it["should not set Overflow flag"] = () => { this.processor.Overflow.should_be_false(); };
			};
		}
	}
}