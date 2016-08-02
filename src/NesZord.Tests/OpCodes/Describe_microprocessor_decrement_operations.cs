using NesZord.Core;
using NesZord.Core.Extensions;
using NSpec;
using Ploeh.AutoFixture;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_decrement_operations : Describe_microprocessor_operation
	{
		public void When_decrement_the_value_of_x_register()
		{
			var xRegisterValue = default(byte);

			before = () => xRegisterValue = 0x05;

			this.RunProgram(() => new byte[]
			{
				(byte)OpCode.LDX_Immediate, xRegisterValue,
				(byte)OpCode.DEX_Implied
			});

			it["should decrement 1 to x register"] = () => { this.Processor.X.Value.should_be(0x04); };

			this.ZeroFlagShouldBeFalse();
			this.NegativeFlagShouldBeFalse();

			context["given that x register value is decremented to 0x00"] = () => 
			{
				before = () => xRegisterValue = 0x01;
				this.ZeroFlagShouldBeTrue();
			};

			context["given that x register value is decremented to a value thar sign bit is set"] = () =>
			{
				before = () => xRegisterValue = 0x81;
				this.NegativeFlagShouldBeTrue();
			};
		}

		public void When_decrement_the_value_of_y_register()
		{
			var yRegisterValue = default(byte);

			before = () => yRegisterValue = 0x05;

			this.RunProgram(() => new byte[]
			{
				(byte)OpCode.LDY_Immediate, yRegisterValue,
				(byte)OpCode.DEY_Implied
			});

			it["should decrement 1 to y register"] = () => { this.Processor.Y.Value.should_be(0x04); };

			this.ZeroFlagShouldBeFalse();
			this.NegativeFlagShouldBeFalse();

			context["given that y register value is decremented to 0x00"] = () =>
			{
				before = () => yRegisterValue = 0x01;
				this.ZeroFlagShouldBeTrue();
			};

			context["given that y register value is decremented to a value thar sign bit is set"] = () =>
			{
				before = () => yRegisterValue = 0x81;
				this.NegativeFlagShouldBeTrue();
			};
		}
	}
}