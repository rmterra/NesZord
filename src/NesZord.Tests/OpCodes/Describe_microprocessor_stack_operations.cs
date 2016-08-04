using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_stack_operations : nspec
	{
		private static readonly Fixture fixture = new Fixture();

		private MemoryMock memory;

		private Microprocessor processor;

		public void before_each()
		{
			this.memory = new MemoryMock();
			this.processor = new Microprocessor(this.memory);
		}

		public void When_push_accumulator_to_stack()
		{
			act = () => 
			{
				this.processor.RunProgram(new byte[] 
				{
					(byte) OpCode.LDA_Immediate, fixture.Create<byte>(),
					(byte) OpCode.PHA_Implied
				});
			};

			it["should value at first stack position be equal to accumulator"] = () =>
			{
				this.memory.Read(Memory.INITIAL_STACK_OFFSET, Memory.STACK_PAGE).should_be(this.processor.Accumulator.Value);
			};

			it["should actual stack pointer value be 0xfe"] = () =>
			{
				this.processor.StackPointer.CurrentOffset.should_be(0xfe);
			};
		}

		public void When_push_processor_status_to_stack()
		{
			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte) OpCode.SEC_Implied,
					(byte) OpCode.SED_Implied,
					(byte) OpCode.PHP_Implied
				});
			};

			it["should value at first stack position be equal to 0x09"] = () =>
			{
				this.memory.Read(Memory.INITIAL_STACK_OFFSET, Memory.STACK_PAGE).should_be(0x09);
			};

			it["should actual stack pointer value be 0xfe"] = () =>
			{
				this.processor.StackPointer.CurrentOffset.should_be(0xfe);
			};
		}

		public void When_pull_from_stack_to_accumulator()
		{
			var accumulatorValue = default(byte);

			before = () => accumulatorValue = 0x05;

			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte) OpCode.LDA_Immediate, accumulatorValue,
					(byte) OpCode.PHA_Implied,
					(byte) OpCode.LDA_Immediate, 0xff,
					(byte) OpCode.PLA_Implied,
				});
			};

			it["should accumulator value be 0x05"] = () => this.processor.Accumulator.Value.should_be(0x05);
			it["should keep initial value on negative flag"] = () => { processor.Negative.should_be_false(); };
			it["should keep initial value on zero flag"] = () => { processor.Zero.should_be_false(); };

			it["should value at first stack position be equal to accumulator"] = () =>
			{
				this.memory.Read(Memory.INITIAL_STACK_OFFSET, Memory.STACK_PAGE).should_be(this.processor.Accumulator.Value);
			};

			it["should actual stack pointer value be 0xff"] = () =>
			{
				this.processor.StackPointer.CurrentOffset.should_be(0xff);
			};

			context["given that pulled value is 0x00"] = () =>
			{
				before = () => accumulatorValue = 0x00;
				it["should set zero flag"] = () => { processor.Zero.should_be_true(); };
			};

			context["given that pulled value has sign bit set"] = () =>
			{
				before = () => accumulatorValue = 0x80;
				it["should set negative flag"] = () => { processor.Negative.should_be_true(); };
			};
		}

		public void When_pull_from_stack_to_processor_flags()
		{
			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte) OpCode.SEC_Implied,
					(byte) OpCode.SED_Implied,
					(byte) OpCode.PHP_Implied,
					(byte) OpCode.CLC_Implied,
					(byte) OpCode.CLD_Implied,
					(byte) OpCode.PLP_Implied
				});
			};

			it["should set carry flag"] = () => this.processor.Carry.should_be_true();
			it["should set decimal flag"] = () => this.processor.Decimal.should_be_true();

			it["should not set zero flag"] = () => this.processor.Zero.should_be_false();
			it["should not set interrupt flag"] = () => this.processor.Interrupt.should_be_false();
			it["should not set break flag"] = () => this.processor.Break.should_be_false();
			it["should not set overflow flag"] = () => this.processor.Overflow.should_be_false();
			it["should not set negative flag"] = () => this.processor.Negative.should_be_false();

			it["should actual stack pointer value be 0xff"] = () =>
			{
				this.processor.StackPointer.CurrentOffset.should_be(0xff);
			};
		}
	}
}
