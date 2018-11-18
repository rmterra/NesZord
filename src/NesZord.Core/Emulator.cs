using NesZord.Core.Memory;

namespace NesZord.Core
{
	public class Emulator
	{
		public const byte INITIAL_STACK_OFFSET = 0xff;

		public const byte STACK_PAGE = 0x01;

		public const byte ZERO_PAGE = 0x00;

		public static readonly MemoryAddress PROGRAM_ROM_START = new MemoryAddress(0x80, 0x00);

		public static readonly MemoryAddress PROGRAM_ROM_END = new MemoryAddress(0xff, 0xff);

		private readonly BoundedMemory romMemory;

		private readonly Ram ramMemory;

		public Emulator()
		{
			this.ramMemory = new Ram();
			this.romMemory = new BoundedMemory(PROGRAM_ROM_START, PROGRAM_ROM_END);
		}

		public void LoadProgram(byte[] program)
		{
			var address = romMemory.FirstAddress;

			for (int i = 0; i < program.Length; i++)
			{
				this.romMemory.Write(address, program[i]);

				address = address.NextAddress();
			}
		}

		public void WriteZeroPage(byte offset, byte value)
			=> this.Write(ZERO_PAGE, offset, value);

		public void Write(byte page, byte offset, byte value)
			=> this.Write(new MemoryAddress(page, offset), value);

		public void Write(MemoryAddress address, byte value)
		{
			if (address < PROGRAM_ROM_START)
			{
				this.ramMemory.Write(address, value);
			}
			else
			{
				this.romMemory.Write(address, value);
			}
		}

		public byte ReadZeroPage(byte offset)
			=> this.Read(ZERO_PAGE, offset);

		public byte Read(byte page, byte offset)
			=> this.Read(new MemoryAddress(page, offset));

		public byte Read(MemoryAddress address)
		{
			if (address < PROGRAM_ROM_START)
			{
				return this.ramMemory.Read(address);
			}
			else
			{
				return this.romMemory.Read(address);
			}
		}
	}
}
