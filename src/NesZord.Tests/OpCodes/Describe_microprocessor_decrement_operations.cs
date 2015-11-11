using NesZord.Core;
using NesZord.Core.Extensions;
using NSpec;
using Ploeh.AutoFixture;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_decrement_operations : nspec
	{
		private static readonly Fixture fixture = new Fixture();

		private Microprocessor processor;

		public void before_each() { this.processor = new Microprocessor(new MemoryMock()); }

		public void When_decrement_the_value_of_x_register()
		{
			var expectedXRegisterValue = default(byte);

			before = () => { expectedXRegisterValue = fixture.Create<byte>(); };

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDX_Immediate, (byte)(expectedXRegisterValue + 1),
					(byte)OpCode.DEX_Implied
				});
			};

			it["should decrement 1 to x register"] = () => { processor.X.should_be(expectedXRegisterValue); };

			it["should set zero flag when x register value os 0x00"] = () =>
			{
				processor.Zero.should_be(this.processor.X == 0);
			};

			it["should set negative flag with last x register bit value"] = () => 
			{
				processor.Negative.should_be(this.processor.X.GetBitAt(Microprocessor.SIGN_BIT_INDEX));
			};
		}

		public void When_decrement_the_value_of_y_register()
		{
			var expectedYRegisterValue = default(byte);

			before = () => { expectedYRegisterValue = fixture.Create<byte>(); };

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDY_Immediate, (byte)(expectedYRegisterValue + 1),
					(byte)OpCode.DEY_Implied
				});
			};

			it["should decrement 1 to y register"] = () => { processor.Y.should_be(expectedYRegisterValue); };

			it["should set zero flag when y register value os 0x00"] = () =>
			{
				processor.Zero.should_be(this.processor.Y == 0);
			};

			it["should set negative flag with last y register bit value"] = () =>
			{
				processor.Negative.should_be(this.processor.Y.GetBitAt(Microprocessor.SIGN_BIT_INDEX));
			};
		}
	}
}