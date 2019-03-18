using NesZord.Core.Memory;

namespace NesZord.Core.Cartridge
{
	public interface IMapper
	{
		int GraphicBank { get; }

		int ProgramBank { get; }

		void Write(MemoryAddress address, byte value);
	}
}
