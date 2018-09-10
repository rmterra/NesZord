using NesZord.Core;
using NesZord.Tests.Memory;
using System;

namespace NesZord.Tests.AddressingMode
{
	public class AccumulatorAddressingMode : IAddressingMode
	{
		private Cpu cpu;

		private OpCode opCode;

		public AccumulatorAddressingMode(OpCode opCode)
		{
			this.opCode = opCode;
		}

		public byte OperationByte
		{
			get => this.cpu.Accumulator.Value;
			set => this.cpu.Accumulator.Value = value;
		}

		public void Initialize(Cpu cpu, MemoryMapperMock memory)
			=> this.cpu = cpu ?? throw new ArgumentNullException(nameof(cpu));

		public void RunProgram()
			=> this.cpu.RunProgram(new byte[] { (byte)this.opCode });
	}
}
