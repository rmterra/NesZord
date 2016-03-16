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
	public class Describe_microprocessor_jump_operations : nspec
	{
		private static readonly Fixture fixture = new Fixture();

		private MemoryMock memory;

		private Microprocessor processor;

		public void before_each()
		{
			this.memory = new MemoryMock();
			this.processor = new Microprocessor(this.memory);
		}

		public void When_jump_and_use_absolute_addressing_mode()
		{
			var randomOffset = default(byte);
			before = () =>
			{
				randomOffset = fixture.Create<byte>();
				this.memory.WriteZeroPage(randomOffset, 0x05);
			};

			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte)OpCode.JMP_Absolute, randomOffset, Memory.ZERO_PAGE
				});
			};

			xit["should set program counter as memory location value"] = () => this.processor.ProgramCounter.should_be(0x05);
		}

		public void When_jump_and_use_indirect_addressing_mode()
		{
			var randomOffset = default(byte);
			before = () =>
			{
				randomOffset = fixture.Create<byte>();
				this.memory.MockIndirectMemoryWrite(randomOffset, 0x05);
			};

			act = () =>
			{
				this.processor.RunProgram(new byte[]
				{
					(byte)OpCode.JMP_Indirect, randomOffset
				});
			};

			xit["should set program counter as memory location value"] = () => this.processor.ProgramCounter.should_be(0x05);
		}
	}
}
