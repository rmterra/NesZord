﻿using AutoFixture;
using FluentAssertions;
using NesZord.Core;
using Xunit;

namespace NesZord.Tests.New.OpCodes.TXA
{
	public class When_process_TXA_should : When_process_opcode_should
	{
		private Fixture fixture;

		public When_process_TXA_should()
		{
			this.fixture = new Fixture();
		}

		[Fact]
		public void Transfer_x_value_to_accumulator()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Processor.Accumulator.Value.Should().Equals(this.Processor.X.Value);
		}

		protected override void RunProgram()
		{
			this.Processor.RunProgram(new byte[]
			{
				(byte)OpCode.LDX_Immediate, fixture.Create<byte>(),
				(byte)OpCode.TXA_Implied
			});
		}
	}
}
