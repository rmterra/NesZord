namespace NesZord.Core.Memory
{
	internal interface IBoundedMemory
	{
		MemoryAddress FirstAddress { get; }

		MemoryAddress LastAddress { get; }

		byte Read(MemoryAddress address);

		void Write(MemoryAddress address, byte value);
	}
}