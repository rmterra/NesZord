using NesZord.Core.Memory;
using System;

namespace NesZord.Core
{
	public class Stack
	{
		private readonly Emulator emulator;

		public Stack(Emulator emulator)
		{
			this.emulator = emulator ?? throw new ArgumentNullException(nameof(emulator));

			this.CurrentOffset = Emulator.INITIAL_STACK_OFFSET;
		}

		public byte CurrentOffset { get; set; }

		public byte Pop()
		{
			this.CurrentOffset += 1;
			return this.emulator.Read(this.CurrentOffset, Emulator.STACK_PAGE);
		}

		public void Push(byte value)
		{
			this.emulator.Write(this.CurrentOffset, Emulator.STACK_PAGE, value);
			this.CurrentOffset -= 1;
		}
	}
}