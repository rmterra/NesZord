namespace NesZord.Core.Memory
{
	internal interface IBoundedMemory
	{
		MemoryLocation FirstAddress { get; }

		MemoryLocation LastAddress { get; }

		byte Read(MemoryLocation location);

		void Write(MemoryLocation location, byte value);
	}
}