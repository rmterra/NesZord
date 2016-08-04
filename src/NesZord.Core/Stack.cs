using System;

namespace NesZord.Core
{
	public class Stack
	{
		private readonly Memory memory;

		public Stack(Memory memory)
		{
			if (memory == null) { throw new ArgumentNullException(nameof(memory)); }

			this.memory = memory;
			this.CurrentOffset = Memory.INITIAL_STACK_OFFSET;
		}

		public byte CurrentOffset { get; set; }

		public byte Pop()
		{
			this.CurrentOffset += 1;
			return this.memory.Read(this.CurrentOffset, Memory.STACK_PAGE);
		}

		public void Push(byte value)
		{
			this.memory.Write(this.CurrentOffset, Memory.STACK_PAGE, value);
			this.CurrentOffset -= 1;
		}
	}
}