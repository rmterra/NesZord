namespace NesZord.Core.Memory
{
	internal interface IBoundedMemory
	{
		int FirstAddress { get; }

		int LastAddress { get; }

		byte Read(MemoryLocation location);

		void Write(MemoryLocation location, byte value);
	}
}