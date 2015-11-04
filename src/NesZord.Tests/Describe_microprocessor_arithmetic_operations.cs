using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;

namespace NesZord.Tests
{
	public class Describe_microprocessor_arithmetic_operations : nspec
	{
		private static readonly Fixture fixture = new Fixture();

		private Microprocessor processor;

		public void before_each() { this.processor = new Microprocessor(new Memory()); }

		public void When_subtract_with_carry_with_immediate_addressing_mode()
		{
			var byteToSubtract = default(byte);

			before = () => { byteToSubtract = fixture.Create<byte>(); };

			act = () =>
			{
                processor.RunProgram(new byte[]
				{
					(byte)OpCode.ImmediateLoadAccumulator, fixture.Create<byte>(),
					(byte)OpCode.TransferFromAccumulatorToX,
					(byte)OpCode.ImmediateSubtractWithCarry, byteToSubtract
				});
			};

			it["should subtract the specified value to accumulator"] = () =>
			{
				var resultWithCarry = processor.X - byteToSubtract;
				processor.Accumulator.should_be((byte)resultWithCarry & 0xff);
			};

			context["given that the subtracted byte is greater than Accumulator value"] = () =>
			{
				before = () => { byteToSubtract = 0xff; };
				it["should keep carry flag clear"] = () => { processor.Carry.should_be_false(); };
			};

			context["given that the subtracted byte is lower than Accumulator value"] = () =>
			{
				before = () => { byteToSubtract = 0x00; };
				it["should turn carry flag on"] = () => { processor.Carry.should_be_true(); };
			};
		}

		public void When_compare_y_register_with_memory_with_immediate_addressing_mode()
		{
			var byteToCompare = default(byte);

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.ImmediateLoadYRegister, 0x05,
					(byte)OpCode.ImmediateCompareYRegister, byteToCompare
				});
			};

			context["given that y register value is lower than compared byte"] = () =>
			{
				before = () => { byteToCompare = 0xff; };
				it["should not turn on carry flag"] = () => { processor.Carry.should_be_false(); };
				it["should not turn on zero flag"] = () => { processor.Zero.should_be_false(); };
			};

			context["given that y register value is equal than compared byte"] = () =>
			{
				before = () => { byteToCompare = 0x05; };
				it["should turn on carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should turn on zero flag"] = () => { processor.Zero.should_be_true(); };
			};

			context["given that y register value is grater than compared byte"] = () =>
			{
				before = () => { byteToCompare = 0x00; };
				it["should turn on carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should not turn on zero flag"] = () => { processor.Zero.should_be_false(); };
			};
		}

		public void When_compare_x_register_with_memory_with_immediate_addressing_mode()
		{
			var byteToCompare = default(byte);

			act = () =>
			{
				processor.RunProgram(new byte[]
				{
					(byte)OpCode.ImmediateLoadXRegister, 0x05,
					(byte)OpCode.ImmediateCompareXRegister, byteToCompare
				});
			};

			context["given that x register value is lower than compared byte"] = () =>
			{
				before = () => { byteToCompare = 0xff; };
				it["should not turn on carry flag"] = () => { processor.Carry.should_be_false(); };
				it["should not turn on zero flag"] = () => { processor.Zero.should_be_false(); };
			};

			context["given that x register value is equal than compared byte"] = () =>
			{
				before = () => { byteToCompare = 0x05; };
				it["should turn on carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should turn on zero flag"] = () => { processor.Zero.should_be_true(); };
			};

			context["given that x register value is grater than compared byte"] = () =>
			{
				before = () => { byteToCompare = 0x00; };
				it["should turn on carry flag"] = () => { processor.Carry.should_be_true(); };
				it["should not turn on zero flag"] = () => { processor.Zero.should_be_false(); };
			};
		}
	}
}