using NesZord.Core.Memory;
using System;

namespace NesZord.Core
{
	public class Emulator
	{
		public const byte INITIAL_STACK_OFFSET = 0xff;

		public const byte STACK_PAGE = 0x01;

		public const byte ZERO_PAGE = 0x00;

		public const int PROGRAM_ROM_START = 0x0600;

		private readonly byte[][] memory;

		public Emulator()
		{
			this.memory = new byte[0x100][];
			for (int i = 0; i <= Byte.MaxValue; i++) { this.memory[i] = new byte[0x100]; }
		}

		public void LoadProgram(byte[] program)
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
			this.Write(new MemoryAddress(Emulator.ZERO_PAGE, offset), value);
		}

		public void Write(byte offset, byte page, byte value)
		{
			this.Write(new MemoryAddress(page, offset), value);
		}

		public void Write(MemoryAddress address, byte value)
		{
			this.memory[address.Page][address.Offset] = value;
		}

		public byte Read(byte offset, byte page)
		{
			return this.Read(new MemoryAddress(page, offset));
		}

		public byte Read(MemoryAddress address)
		{
			return this.memory[address.Page][address.Offset];
		}
	}
}
