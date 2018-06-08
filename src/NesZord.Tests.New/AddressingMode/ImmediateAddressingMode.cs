using NesZord.Core;

namespace NesZord.Tests.New.AddressingMode
{
	public class ImmediateAddressingMode : IAddressingMode
	{
		public byte OperationByte { get; set; }

		public void Initialize(Microprocessor processor, MemoryMock memory) { }
	}
}
