using NesZord.Core.Memory;
using System;

namespace NesZord.Core
{
	public class Stack
	{
		private readonly MemoryMapper memory;

		public Stack(MemoryMapper memory)
		{
			if (memory == null) { throw new ArgumentNullException(nameof(memory)); }

			this.memory = memory;
			this.CurrentOffset = MemoryMapper.INITIAL_STACK_OFFSET;
		}

		public byte CurrentOffset { get; set; }

		public byte Pop()
		{
			this.CurrentOffset += 1;
			return this.memory.Read(this.CurrentOffset, MemoryMapper.STACK_PAGE);
		}

		public void Push(byte value)
		{
			this.memory.Write(this.CurrentOffset, MemoryMapper.STACK_PAGE, value);
			this.CurrentOffset -= 1;
		}
	}
}