using NesZord.Core;
using System;

namespace NesZord.Tests.New.AddressingMode
{
	public class AccumulatorAddressingMode : IAddressingMode
	{
		private Microprocessor processor;

		private OpCode opCode;

		public AccumulatorAddressingMode(OpCode opCode)
		{
			this.opCode = opCode;
		}

		public byte OperationByte
		{
			get => this.processor.Accumulator.Value;
			set => this.processor.Accumulator.Value = value;
		}

		public void Initialize(Microprocessor processor, MemoryMock memory)
			=> this.processor = processor ?? throw new ArgumentNullException(nameof(processor));

		public void RunProgram()
			=> this.processor.RunProgram(new byte[] { (byte)this.opCode });
	}
}
