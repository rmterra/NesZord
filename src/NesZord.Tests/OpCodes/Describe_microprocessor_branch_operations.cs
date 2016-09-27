using NesZord.Core;
using NSpec;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_branch_operations : Describe_microprocessor_operation
	{
		public void When_branch_if_not_equal()
		{
			this.RunProgram(() => new byte[]
			{
				(byte) OpCode.LDX_Immediate, 0x08,
				(byte) OpCode.DEX_Implied,
				(byte) OpCode.CPX_Immediate, 0x06,
				(byte) OpCode.BNE_Relative, 0x03,
				(byte) OpCode.LDX_Immediate, 0xff,
				(byte) OpCode.BRK_Implied
			});

			it["should X register value be equal 0x07"] = () => { this.Processor.X.Value.should_be(0x07); };
			this.CarryFlagShouldBeTrue();
			this.ZeroFlagShouldBeFalse();
		}

		public void When_branch_if_equal()
		{
			this.RunProgram(() => new byte[]
			{
				(byte) OpCode.LDY_Immediate, 0x08,
				(byte) OpCode.DEY_Implied,
				(byte) OpCode.CPY_Immediate, 0x07,
				(byte) OpCode.BEQ_Relative, 0x03,
				(byte) OpCode.LDY_Immediate, 0xff,
				(byte) OpCode.BRK_Implied
			});

			it["should Y register value be equal 0x07"] = () => { this.Processor.Y.Value.should_be(0x07); };
			this.CarryFlagShouldBeTrue();
			this.ZeroFlagShouldBeTrue();
		}

		public void When_branch_if_carry_is_clear()
		{
			this.RunProgram(() => new byte[]
			{
				(byte) OpCode.LDX_Immediate, 0x05,
				(byte) OpCode.CPX_Immediate, 0x0a,
				(byte) OpCode.BCC_Relative, 0x03,
				(byte) OpCode.LDX_Immediate, 0xff,
				(byte) OpCode.BRK_Implied
			});

			it["should X register value be equal 0x05"] = () => { this.Processor.X.Value.should_be(0x05); };
			this.CarryFlagShouldBeFalse();
			this.ZeroFlagShouldBeFalse();
		}

		public void When_branch_if_carry_is_set()
		{
			this.RunProgram(() => new byte[]
			{
				(byte) OpCode.LDX_Immediate, 0x0a,
				(byte) OpCode.CPX_Immediate, 0x05,
				(byte) OpCode.BCS_Relative, 0x03,
				(byte) OpCode.LDX_Immediate, 0xff,
				(byte) OpCode.BRK_Implied
			});

			it["should X register value be equal 0x0a"] = () => { this.Processor.X.Value.should_be(0x0a); };
			this.CarryFlagShouldBeTrue();
			this.ZeroFlagShouldBeFalse();
		}

		public void When_branch_if_negative_is_clear()
		{
			this.RunProgram(() => new byte[]
			{
				(byte) OpCode.LDX_Immediate, 0x80,
				(byte) OpCode.DEX_Implied,
				(byte) OpCode.BPL_Relative, 0x03,
				(byte) OpCode.LDX_Immediate, 0xff,
				(byte) OpCode.BRK_Implied
			});

			it["should X register value be equal 0x7f"] = () => { this.Processor.X.Value.should_be(0x7f); };
			this.NegativeFlagShouldBeFalse();
		}

		public void When_branch_if_negative_is_set()
		{
			this.RunProgram(() => new byte[]
			{
				(byte) OpCode.LDX_Immediate, 0x7f,
				(byte) OpCode.INX_Implied,
				(byte) OpCode.BMI_Relative, 0xfd,
				(byte) OpCode.LDX_Immediate, 0xff,
				(byte) OpCode.BRK_Implied
			});

			it["should X register value be equal 0x80"] = () => { this.Processor.X.Value.should_be(0x80); };
			this.NegativeFlagShouldBeTrue();
		}

		public void When_branch_if_overflow_is_clear()
		{
			this.RunProgram(() => new byte[]
			{
				(byte) OpCode.LDA_Immediate, 0xfd,
				(byte) OpCode.ADC_Immediate, 0x01,
				(byte) OpCode.BVC_Relative, 0x03,
				(byte) OpCode.LDA_Immediate, 0xff,
				(byte) OpCode.BRK_Implied
			});

			it["should accumulator value be equal 0xfe"] = () => { this.Processor.Accumulator.Value.should_be(0xfe); };
			this.OverflowFlagShouldBeFalse();
		}

		public void When_branch_if_overflow_is_set()
		{
			this.RunProgram(() => new byte[]
			{
				(byte) OpCode.LDA_Immediate, 0xff,
				(byte) OpCode.ADC_Immediate, 0x01,
				(byte) OpCode.BVS_Relative, 0x03,
				(byte) OpCode.LDA_Immediate, 0xff,
				(byte) OpCode.BRK_Implied
			});

			it["should accumulator value be equal 0x00"] = () => { this.Processor.Accumulator.Value.should_be(0x00); };
			this.OverflowFlagShouldBeTrue();
		}
	}
}