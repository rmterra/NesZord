using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesZord.Tests.OpCodes
{
	public class Describe_microprocessor_transfer_operations : nspec
	{
		private static readonly Fixture fixture = new Fixture();

		private Microprocessor processor;

		public void before_each() { this.processor = new Microprocessor(new Memory()); }

		public void When_transfer_from_accumulator_to_x_register()
		{
			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDA_Immediate, fixture.Create<byte>(),
					(byte)OpCode.TAX_Implied
				});
			};

			it["x must be equal to accumulator"] = () => { processor.X.Value.should_be(processor.Accumulator.Value); };
		}

		public void When_transfer_from_accumulator_to_y_register()
		{
			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDA_Immediate, fixture.Create<byte>(),
					(byte)OpCode.TAY_Implied
				});
			};

			it["y must be equal to accumulator"] = () => { processor.Y.Value.should_be(processor.Accumulator.Value); };
		}

		public void When_transfer_from_x_register_to_accumulator()
		{
			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDX_Immediate, fixture.Create<byte>(),
					(byte)OpCode.TXA_Implied
				});
			};

			it["accumulator must be equal to x"] = () => { processor.Accumulator.Value.should_be(processor.X.Value); };
		}

		public void When_transfer_from_y_register_to_accumulator()
		{
			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDY_Immediate, fixture.Create<byte>(),
					(byte)OpCode.TYA_Implied
				});
			};

			it["accumulator must be equal to y"] = () => { processor.Accumulator.Value.should_be(processor.Y.Value); };
		}

		public void When_transfer_from_x_to_stack_pointer()
		{
			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte)OpCode.LDX_Immediate, fixture.Create<byte>(),
					(byte)OpCode.TXS_Implied
				});
			};

			it["should stack pointer be equal to x register"] = () => this.processor.StackPointer.should_be(this.processor.X.Value);
		}

		public void When_transfer_from_stack_pointer_to_x()
		{
			act = () =>
			{
				this.processor.RunProgram(new byte[] { (byte)OpCode.TSX_Implied });
			};

			it["should x register be equal to stack pointer"] = () => this.processor.X.Value.should_be(this.processor.StackPointer);
			it["should set negative flag"] = () => this.processor.Negative.should_be_true();
			it["should not set zero flag"] = () => this.processor.Zero.should_be_false();

			context["given that stack pointer value is 0x00"] = () =>
			{
				act = () =>
				{
					this.processor.RunProgram(new byte[]
					{
						(byte)OpCode.LDX_Immediate, 0x00,
						(byte)OpCode.TXS_Implied,
						(byte)OpCode.TSX_Implied
					});
				};

				it["should set zero flag"] = () => this.processor.Zero.should_be_true();
			};
		}
	}
}