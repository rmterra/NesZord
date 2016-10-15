using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;
using System;

namespace NesZord.Tests.OpCodes
{
	public abstract class Describe_microprocessor_operation : nspec
	{
		protected Fixture Fixture { get; } = new Fixture();

		protected MemoryMock Memory { get; private set; }

		protected Microprocessor Processor { get; private set; }

		public void before_each()
		{
			this.Memory = new MemoryMock();
			this.Processor = new Microprocessor(this.Memory);
		}

		protected void RunProgram(Func<byte[]> getProgram)
		{
			act = () =>
			{
				var program = getProgram?.Invoke();
				this.Processor.RunProgram(program);
			};
		}

		protected void BreakFlagShouldBeFalse()
		{
			this.FlagShouldBeFalse(() => this.Processor.Break, nameof(this.Processor.Break));
		}

		protected void CarryFlagShouldBeFalse()
		{
			this.FlagShouldBeFalse(() => this.Processor.Carry, nameof(this.Processor.Carry));
		}

		protected void DecimalFlagShouldBeFalse()
		{
			this.FlagShouldBeFalse(() => this.Processor.Decimal, nameof(this.Processor.Decimal));
		}

		protected void InterruptFlagShouldBeFalse()
		{
			this.FlagShouldBeFalse(() => this.Processor.Interrupt, nameof(this.Processor.Interrupt));
		}

		protected void NegativeFlagShouldBeFalse()
		{
			this.FlagShouldBeFalse(() => this.Processor.Negative, nameof(this.Processor.Negative));
		}

		protected void OverflowFlagShouldBeFalse()
		{
			this.FlagShouldBeFalse(() => this.Processor.Overflow, nameof(this.Processor.Overflow));
		}

		protected void ZeroFlagShouldBeFalse()
		{
			this.FlagShouldBeFalse(() => this.Processor.Zero, nameof(this.Processor.Zero));
		}

		private void FlagShouldBeFalse(Func<bool> current, string name)
		{
			this.TestFlag(current, false, name);
		}

		protected void BreakFlagShouldBeTrue()
		{
			this.FlagShouldBeTrue(() => this.Processor.Break, nameof(this.Processor.Break));
		}

		protected void CarryFlagShouldBeTrue()
		{
			this.FlagShouldBeTrue(() => this.Processor.Carry, nameof(this.Processor.Carry));
		}

		protected void DecimalFlagShouldBeTrue()
		{
			this.FlagShouldBeTrue(() => this.Processor.Decimal, nameof(this.Processor.Decimal));
		}

		protected void InterruptFlagShouldBeTrue()
		{
			this.FlagShouldBeTrue(() => this.Processor.Interrupt, nameof(this.Processor.Interrupt));
		}

		protected void NegativeFlagShouldBeTrue()
		{
			this.FlagShouldBeTrue(() => this.Processor.Negative, nameof(this.Processor.Negative));
		}

		protected void OverflowFlagShouldBeTrue()
		{
			this.FlagShouldBeTrue(() => this.Processor.Overflow, nameof(this.Processor.Overflow));
		}

		protected void ZeroFlagShouldBeTrue()
		{
			this.FlagShouldBeTrue(() => this.Processor.Zero, nameof(this.Processor.Zero));
		}

		private void FlagShouldBeTrue(Func<bool> getCurrent, string name)
		{
			this.TestFlag(getCurrent, true, name);
		}

		protected void TestFlag(Func<bool> getCurrent, bool expected, string flagName)
		{
			var sentence = expected ? $"should set {flagName} flag" : $"should not set {flagName} flag";
			it[sentence] = () => getCurrent().should_be(expected);
		}
	}
}