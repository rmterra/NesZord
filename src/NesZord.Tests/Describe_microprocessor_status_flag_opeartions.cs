using NesZord.Core;
using NSpec;

namespace NesZord.Tests
{
	public class Describe_microprocessor_status_flag_opeartions : nspec
	{
		private Microprocessor processor;

		public void before_each() { this.processor = new Microprocessor(new Memory()); }

		public void When_set_carry_flag()
		{
			act = () => 
			{
				this.processor.RunProgram(new byte[] { (byte) OpCode.SetCarryFlag });
			};

			it["should turn on Carry flag"] = () => 
			{
				this.processor.Carry.should_be_true();
			};
		}
	}
}
