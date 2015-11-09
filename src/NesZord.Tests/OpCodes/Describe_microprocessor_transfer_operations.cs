﻿using NesZord.Core;
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
					(byte)OpCode.ImmediateLDA, fixture.Create<byte>(),
					(byte)OpCode.TAX
				});
			};

			it["x must be equal to accumulator"] = () => { processor.X.should_be(processor.Accumulator); };
		}

		public void When_transfer_from_accumulator_to_y_register()
		{
			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte)OpCode.ImmediateLDA, fixture.Create<byte>(),
					(byte)OpCode.TAY
				});
			};

			it["y must be equal to accumulator"] = () => { processor.Y.should_be(processor.Accumulator); };
		}

		public void When_transfer_from_x_register_to_accumulator()
		{
			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte)OpCode.ImmediateLDX, fixture.Create<byte>(),
					(byte)OpCode.TXA
				});
			};

			it["accumulator must be equal to x"] = () => { processor.Accumulator.should_be(processor.X); };
		}

		public void When_transfer_from_y_register_to_accumulator()
		{
			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte)OpCode.ImmediateLDY, fixture.Create<byte>(),
					(byte)OpCode.TYA
				});
			};

			it["accumulator must be equal to y"] = () => { processor.Accumulator.should_be(processor.Y); };
		}
	}
}