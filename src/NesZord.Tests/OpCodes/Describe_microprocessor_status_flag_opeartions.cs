using NesZord.Core;
using NSpec;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_status_flag_opeartions : nspec
	{
		private Microprocessor processor;

		public void before_each() { this.processor = new Microprocessor(new Memory()); }

		public void When_set_carry_flag()
		{
			act = () => 
			{
				this.processor.RunProgram(new byte[] { (byte) OpCode.SEC_Implied });
			};

			it["should turn on Carry flag"] = () => 
			{
				this.processor.Carry.should_be_true();
			};
		}

		public void When_clear_carry_flag()
		{
			act = () =>
			{
				this.processor.RunProgram(new byte[] 
				{
					(byte)OpCode.SEC_Implied,
					(byte)OpCode.CLC_Implied
				});
			};

			it["should turn off Carry flag"] = () =>
			{
				this.processor.Carry.should_be_false();
			};
		}

		public void When_set_decimal_flag()
		{
			act = () =>
			{
				this.processor.RunProgram(new byte[] { (byte)OpCode.SED_Implied });
			};

			it["should turn on Decimal flag"] = () =>
			{
				this.processor.Decimal.should_be_true();
			};
		}

		public void When_clear_decimal_flag()
		{
			act = () =>
			{
				this.processor.RunProgram(new byte[] 
				{
					(byte)OpCode.SED_Implied,
					(byte)OpCode.CLD_Implied
				});
			};

			it["should turn off Decimal flag"] = () =>
			{
				this.processor.Decimal.should_be_false();
			};
		}

		public void When_set_interrupt_flag()
		{
			act = () =>
			{
				this.processor.RunProgram(new byte[] { (byte)OpCode.SEI_Implied });
			};

			it["should turn on Interrupt flag"] = () =>
			{
				this.processor.Interrupt.should_be_true();
			};
		}

		public void When_clear_interrupt_flag()
		{
			act = () =>
			{
				this.processor.RunProgram(new byte[] 
				{
					(byte)OpCode.SEI_Implied,
					(byte)OpCode.CLI_Implied
				});
			};

			it["should turn off Interrupt flag"] = () =>
			{
				this.processor.Interrupt.should_be_false();
			};
		}

		public void When_clear_overflow_flag()
		{
			act = () =>
			{
				this.processor.RunProgram(new byte[] { (byte)OpCode.CLV_Implied });
			};

			it["should turn off Overflow flag"] = () =>
			{
				this.processor.Overflow.should_be_false();
			};
		}
	}
}
