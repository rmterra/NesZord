using NesZord.Core;
using System;

namespace NesZord.Tests.AddressingMode
{
	public class ZeroPageAddressingMode : IAddressingMode
	{
		private readonly OpCode opCode;

		private Cpu cpu;

		private EmulatorMock emulator;

		public ZeroPageAddressingMode(OpCode opCode)
		{
			this.opCode = opCode;
		}

		public byte OperationByte
		{
			get => this.emulator.ReadZeroPage(this.RandomOffset);
			set => this.emulator.WriteZeroPage(this.RandomOffset, value);
		}

		public byte RandomOffset { get; private set; }

		public void Initialize(Cpu cpu, EmulatorMock emulator)
		{
			this.cpu = cpu ?? throw new ArgumentNullException(nameof(cpu));
			this.emulator = emulator ?? throw new ArgumentNullException(nameof(emulator));

			this.RandomOffset = this.emulator.GetRandomOffset();
		}

		public void RunProgram()
			=> this.cpu.RunProgram(new byte[] { (byte)this.opCode, this.RandomOffset });
	}
}
