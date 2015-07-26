using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;

namespace NesZord.Tests
{
	public class Describe_microprocessor_branch_operations : nspec
	{
		private static readonly Fixture fixture = new Fixture();

		private Microprocessor processor;

		public void before_each() { this.processor = new Microprocessor(); }

		public void When_branch_if_not_equal()
		{
			context["given that executed program must branch while X register is not 0x03"] = () =>
			{
				act = () =>
				{
					this.processor.RunProgram(new byte[]
					{
						(byte) OpCode.ImmediateLoadXRegister, 0x08,
						(byte) OpCode.DecrementValueAtX,
						(byte) OpCode.AbsoluteStoreXRegister, 0x00, 0x02,
						(byte) OpCode.ImmediateCompareXRegister, 0x03,
						(byte) OpCode.BranchIfNotEqual, 0xf8,
						(byte) OpCode.AbsoluteStoreXRegister, 0x01, 0x02,
						(byte) OpCode.Break
					});
				};

				it["should X register value be equal 0x03"] = () => { this.processor.X.should_be(0x03); };
				it["should value at $0200 be equal 0x03"] = () => { this.processor.ValueAt(0x02, 0x00).should_be(0x03); };
				it["should value at $0201 be equal 0x03"] = () => { this.processor.ValueAt(0x02, 0x01).Is(0x03); };
				it["should Carry flag turn on"] = () => { this.processor.Carry.should_be_true(); };
				it["should Zero flag turn on"] = () => { this.processor.Zero.should_be_true(); };
			};
		}

		public void When_branch_if_equal()
		{
			context["given that executed program must branch while Y register is not 0x02"] = () =>
			{
				act = () =>
				{
					this.processor.RunProgram(new byte[]
					{
						(byte) OpCode.ImmediateLoadYRegister, 0x01,
						(byte) OpCode.IncrementValueAtY,
						(byte) OpCode.ImmediateCompareYRegister, 0x02,
						(byte) OpCode.BranchIfEqual, 0xfb,
						(byte) OpCode.AbsoluteStoreYRegister, 0x00, 0x02,
						(byte) OpCode.Break
					});
				};

				it["should Y register value be equal 0x03"] = () => { this.processor.Y.should_be(0x03); };
				it["should value at $0200 be equal 0x03"] = () => { this.processor.ValueAt(0x02, 0x00).should_be(0x03); };
				it["should Carry flag turn on"] = () => { this.processor.Carry.should_be_true(); };
				it["should Zero flag turn on"] = () => { this.processor.Zero.should_be_false(); };
			};
		}

		public void When_branch_if_carry_is_clear()
		{
			context["given that executed program must branch while Carry is clear"] = () =>
			{
				act = () =>
				{
					this.processor.RunProgram(new byte[]
					{
						(byte) OpCode.ImmediateLoadXRegister, 0xf5,
						(byte) OpCode.TransferFromXToAccumulator,
						(byte) OpCode.AbsoluteStoreAccumulator, 0x00, 0x20,
						(byte) OpCode.ImmediateAddWithCarry, 0x01,
						(byte) OpCode.BranchIfCarryIsClear, 0xf9,
						(byte) OpCode.AbsoluteStoreAccumulator, 0x00, 0x20,
						(byte) OpCode.Break
					});
				};

				it["should X register value be equal 0xf5"] = () => { this.processor.X.should_be(0xf5); };
				it["should Accumulator register value be equal 0x00"] = () => { this.processor.Accumulator.should_be(0x00); };
				it["should value at $0200 be equal 0x00"] = () => { this.processor.ValueAt(0x02, 0x00).should_be(0x00); };
				it["should Carry flag turn on"] = () => { this.processor.Carry.should_be_true(); };
			};
		}

		public void When_branch_if_carry_is_set()
		{
			context["given that executed program must branch while Carry is set"] = () =>
			{
				act = () =>
				{
					this.processor.RunProgram(new byte[]
					{
						(byte) OpCode.ImmediateLoadXRegister, 0x08,
						(byte) OpCode.DecrementValueAtX,
						(byte) OpCode.AbsoluteStoreXRegister, 0x00, 0x02,
						(byte) OpCode.ImmediateCompareXRegister, 0x03,
						(byte) OpCode.BranchIfCarryIsSet, 0xf8,
						(byte) OpCode.AbsoluteStoreXRegister, 0x01, 0x02,
						(byte) OpCode.Break
					});
				};

				it["should X register value be equal 0x02"] = () => { this.processor.X.should_be(0x02); };
				it["should value at $0200 be equal 0x02"] = () => { this.processor.ValueAt(0x02, 0x00).should_be(0x02); };
				it["should value at $0201 be equal 0x02"] = () => { this.processor.ValueAt(0x02, 0x01).Is(0x02); };
				it["should Carry flag turn on"] = () => { this.processor.Carry.should_be_false(); };
			};
		}
	}
}