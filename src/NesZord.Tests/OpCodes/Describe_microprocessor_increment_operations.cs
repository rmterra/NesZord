using NesZord.Core;
using NesZord.Core.Extensions;
using NSpec;
using Ploeh.AutoFixture;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_increment_operations : nspec
	{
		private static readonly Fixture fixture = new Fixture();

		private Microprocessor processor;

		public void before_each() { this.processor = new Microprocessor(new Memory()); }

		public void When_increment_the_value_of_y_register()
		{
			act = () => { this.processor.RunProgram(new byte[] { (byte)OpCode.INY_Implied }); };

			it["should increment 1 to y register"] = () => { processor.Y.should_be(0x01); };

			it["should set zero flag when y register value os 0x00"] = () =>
			{
				processor.Zero.should_be(this.processor.Y == 0);
			};

			it["should set negative flag with last y register bit value"] = () =>
			{
				processor.Negative.should_be(this.processor.Y.GetBitAt(Microprocessor.SIGN_BIT_INDEX));
			};

		}

		public void When_increment_the_value_of_x_register()
		{
			act = () => { this.processor.RunProgram(new byte[] { (byte)OpCode.INX_Implied }); };

			it["should increment 1 to x register"] = () => { processor.X.should_be(0x01); };

			it["should set zero flag when x register value os 0x00"] = () =>
			{
				processor.Zero.should_be(this.processor.X == 0);
			};

			it["should set negative flag with last x register bit value"] = () =>
			{
				processor.Negative.should_be(this.processor.X.GetBitAt(Microprocessor.SIGN_BIT_INDEX));
			};

		}
	}
}