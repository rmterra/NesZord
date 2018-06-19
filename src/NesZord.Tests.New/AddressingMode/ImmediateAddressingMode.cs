using NesZord.Core;
using System;

namespace NesZord.Tests.New.AddressingMode
{
	public class ImmediateAddressingMode : IAddressingMode
	{
		private readonly OpCode opCode;

		private Microprocessor processor;

		public ImmediateAddressingMode(OpCode opCode)
		{
			this.opCode = opCode;
		}

		public byte OperationByte { get; set; }

		public void Initialize(Microprocessor processor, MemoryMock memory)
		{
			this.processor = processor ?? throw new ArgumentNullException(nameof(processor));
		}

		public void RunProgram()
			=> this.processor.RunProgram(new byte[] { (byte)this.opCode, this.OperationByte });
	}
}
