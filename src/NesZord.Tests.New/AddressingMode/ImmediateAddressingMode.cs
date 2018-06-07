using NesZord.Core;

namespace NesZord.Tests.New.AddressingMode
{
	public class ImmediateAddressingMode : IAddressingMode
	{
		public byte ByteToCompare { get; private set; }

		public void Initialize() { }

		public void SetOperationByte(MemoryMock memory, byte value)
			=> this.ByteToCompare = value;
	}
}
