using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesZord.Tests
{
	public class Describe_microprocessor_branch_operations : nspec
	{
		private static readonly Fixture fixture = new Fixture();

		public void When_branch_if_not_equal()
		{
			Microprocessor processor = null;

			before = () => { processor = new Microprocessor(); };

			context["given that executed program must branch while X register is not 0x03"] = () =>
			{
				act = () =>
				{
					processor.RunProgram(new byte[]
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

				it["should X register value be equal 0x03"] = () => { processor.X.should_be(0x03); };
				it["should value at $0200 be equal 0x03"] = () => { processor.ValueAt(0x02, 0x00).should_be(0x03); };
				it["should value at $0201 be equal 0x03"] = () => { processor.ValueAt(0x02, 0x01).Is(0x03); };
				it["should Carry flag turn on"] = () => { processor.Carry.should_be_true(); };
				it["should Zero flag turn on"] = () => { processor.Zero.should_be_true(); };
			};
		}
	}
}