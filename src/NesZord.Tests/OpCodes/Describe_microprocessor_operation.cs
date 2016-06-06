using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;
using System;

namespace NesZord.Tests.OpCodes
{
	public abstract class Describe_microprocessor_operation : nspec
	{
		protected Fixture Fixture { get; private set; }

		protected MemoryMock Memory { get; private set; }

		protected Microprocessor Processor { get; private set; }

		public void before_each()
		{
			this.Fixture = new Fixture();
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

		protected void CarryFlagShouldBeFalse()
		{
			this.FlagShouldBeFalse(() => this.Processor.Carry, nameof(this.Processor.Carry));
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

		protected void CarryFlagShouldBeTrue()
		{
			this.FlagShouldBeTrue(() => this.Processor.Carry, nameof(this.Processor.Carry));
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