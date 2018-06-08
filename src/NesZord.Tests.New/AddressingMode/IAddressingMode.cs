using NesZord.Core;

namespace NesZord.Tests.New.AddressingMode
{
	public interface IAddressingMode
	{
		byte OperationByte { get; set; }

		void Initialize(Microprocessor processor, MemoryMock memory);
	}
}
