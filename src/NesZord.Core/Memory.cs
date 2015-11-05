using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NesZord.Core
{
	public class Memory
	{
		public const byte ZERO_PAGE = 0x00;

		public const int PROGRAM_ROM_START = 0x0600;

		private readonly byte[][] memory;

		public Memory()
		{
			this.memory = new byte[0x100][];
			for (int i = 0; i <= Byte.MaxValue; i++) { this.memory[i] = new byte[0x100]; }
		}

		public void LoadMemory(byte[] program)
		{
			byte page = PROGRAM_ROM_START >> 8;
			byte offset = PROGRAM_ROM_START & 0xff;

			for (int i = 0; i < program.Length; i++)
			{
				this.memory[page][offset] = program[i];
				offset++;
			}
		}

		public void WriteZeroPage(byte offset, byte value)
		{
			this.Write(new MemoryLocation(offset, 0x00), value);
		}

		public void Write(byte page, byte offset, byte value)
		{
			this.Write(new MemoryLocation(offset, page), value);
		}

		public void Write(MemoryLocation location, byte value)
		{
			this.memory[location.Page][location.Offset] = value;
		}

		public byte Read(byte page, byte offset)
		{
			return this.Read(new MemoryLocation(offset, page));
		}

		public byte Read(MemoryLocation location)
		{
			return this.memory[location.Page][location.Offset];
		}
	}
}
