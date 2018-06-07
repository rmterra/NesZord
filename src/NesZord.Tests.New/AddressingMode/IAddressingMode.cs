namespace NesZord.Tests.New.AddressingMode
{
	public interface IAddressingMode
	{
		void Initialize();

		void SetOperationByte(MemoryMock memory, byte value);
	}
}
