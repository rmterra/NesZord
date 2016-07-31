using NesZord.Core;
using NSpec;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_branch_operations : Describe_microprocessor_operation
	{
		public void When_branch_if_not_equal()
		{
			context["given that executed program must branch while X register is not 0x03"] = () =>
			{
				this.RunProgram(() => new byte[]
				{
					(byte) OpCode.LDX_Immediate, 0x08,
					(byte) OpCode.DEX_Implied,
					(byte) OpCode.STX_Absolute, 0x00, 0x02,
					(byte) OpCode.CPX_Immediate, 0x03,
					(byte) OpCode.BNE_Relative, 0xf8,
					(byte) OpCode.STX_Absolute, 0x01, 0x02,
					(byte) OpCode.BRK_Implied
				});

				it["should X register value be equal 0x03"] = () => { this.Processor.X.Value.should_be(0x03); };
				it["should value at $0200 be equal 0x03"] = () => { this.Memory.Read(0x00, 0x02).should_be(0x03); };
				it["should value at $0201 be equal 0x03"] = () => { this.Memory.Read(0x01, 0x02).Is(0x03); };
				this.CarryFlagShouldBeTrue();
				this.ZeroFlagShouldBeTrue();
			};
		}

		public void When_branch_if_equal()
		{
			context["given that executed program must branch while Y register is not 0x02"] = () =>
			{
				this.RunProgram(() => new byte[]
				{
					(byte) OpCode.LDY_Immediate, 0x01,
					(byte) OpCode.INY_Implied,
					(byte) OpCode.CPY_Immediate, 0x02,
					(byte) OpCode.BEQ_Relative, 0xfb,
					(byte) OpCode.STY_Absolute, 0x00, 0x02,
					(byte) OpCode.BRK_Implied
				});

				it["should Y register value be equal 0x03"] = () => { this.Processor.Y.Value.should_be(0x03); };
				it["should value at $0200 be equal 0x03"] = () => { this.Memory.Read(0x00, 0x02).should_be(0x03); };
				this.CarryFlagShouldBeTrue();
				this.ZeroFlagShouldBeFalse();
			};
		}

		public void When_branch_if_carry_is_clear()
		{
			context["given that executed program must branch while Carry is clear"] = () =>
			{
				this.RunProgram(() => new byte[]
				{
					(byte) OpCode.LDX_Immediate, 0xf5,
					(byte) OpCode.TXA_Implied,
					(byte) OpCode.STA_Absolute, 0x00, 0x20,
					(byte) OpCode.ADC_Immediate, 0x01,
					(byte) OpCode.BCC_Relative, 0xf9,
					(byte) OpCode.STA_Absolute, 0x00, 0x20,
					(byte) OpCode.BRK_Implied
				});

				it["should X register value be equal 0xf5"] = () => { this.Processor.X.Value.should_be(0xf5); };
				it["should Accumulator register value be equal 0x00"] = () => { this.Processor.Accumulator.Value.should_be(0x00); };
				it["should value at $0200 be equal 0x00"] = () => { this.Memory.Read(0x00, 0x02).should_be(0x00); };
				this.CarryFlagShouldBeTrue();
			};
		}

		public void When_branch_if_carry_is_set()
		{
			context["given that executed program must branch while Carry is set"] = () =>
			{
				this.RunProgram(() => new byte[]
				{
					(byte) OpCode.LDX_Immediate, 0x08,
					(byte) OpCode.DEX_Implied,
					(byte) OpCode.STX_Absolute, 0x00, 0x02,
					(byte) OpCode.CPX_Immediate, 0x03,
					(byte) OpCode.BCS_Relative, 0xf8,
					(byte) OpCode.STX_Absolute, 0x01, 0x02,
					(byte) OpCode.BRK_Implied
				});

				it["should X register value be equal 0x02"] = () => { this.Processor.X.Value.should_be(0x02); };
				it["should value at $0200 be equal 0x02"] = () => { this.Memory.Read(0x00, 0x02).should_be(0x02); };
				it["should value at $0201 be equal 0x02"] = () => { this.Memory.Read(0x01, 0x02).Is(0x02); };
				this.CarryFlagShouldBeFalse();
			};
		}

		public void When_branch_if_negative_is_clear()
		{
			context["given that executed program must branch while Negative is clear"] = () =>
			{
				this.RunProgram(() => new byte[]
				{
					(byte) OpCode.LDX_Immediate, 0x03,
					(byte) OpCode.DEX_Implied,
					(byte) OpCode.BPL_Relative, 0xfd,
					(byte) OpCode.BRK_Implied
				});

				it["should X register value be equal 0xff"] = () => { this.Processor.X.Value.should_be(0xff); };
				this.NegativeFlagShouldBeTrue();
			};
		}

		public void When_branch_if_negative_is_set()
		{
			context["given that executed program must branch while Negative is set"] = () =>
			{
				this.RunProgram(() => new byte[]
				{
					(byte) OpCode.LDX_Immediate, 0xfc,
					(byte) OpCode.INX_Implied,
					(byte) OpCode.BMI_Relative, 0xfd,
					(byte) OpCode.BRK_Implied
				});

				it["should X register value be equal 0x00"] = () => { this.Processor.X.Value.should_be(0x00); };
				this.NegativeFlagShouldBeFalse();
				this.ZeroFlagShouldBeTrue();
			};
		}

		public void When_branch_if_overflow_is_clear()
		{
			context["given that executed program must branch while Overflow is clear"] = () =>
			{
				this.RunProgram(() => new byte[]
				{
					(byte) OpCode.LDA_Immediate, 0xfd,
					(byte) OpCode.ADC_Immediate, 0x01,
					(byte) OpCode.BVC_Relative, 0xfc,
					(byte) OpCode.BRK_Implied
				});

				it["should accumulator value be equal 0x00"] = () => { this.Processor.Accumulator.Value.should_be(0x00); };
				this.OverflowFlagShouldBeTrue();
			};
		}

		public void When_branch_if_overflow_is_set()
		{
			context["given that executed program must branch while Overflow is set"] = () =>
			{
				this.RunProgram(() => new byte[]
				{
					(byte) OpCode.LDA_Immediate, 0x7f,
					(byte) OpCode.ADC_Immediate, 0x01,
					(byte) OpCode.BVS_Relative, 0xfc,
					(byte) OpCode.BRK_Implied
				});

				it["should accumulator value be equal 0x81"] = () => { this.Processor.Accumulator.Value.should_be(0x81); };
				this.NegativeFlagShouldBeTrue();
				this.OverflowFlagShouldBeFalse();
			};
		}
	}
}