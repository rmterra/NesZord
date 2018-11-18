using NesZord.Core;
using System;

namespace NesZord.Tests.AddressingMode
{
	public class AbsoluteAddressingMode : IAddressingMode
	{
		private Cpu cpu;

		private EmulatorMock emulator;

		private OpCode opCode;

		public AbsoluteAddressingMode(OpCode opCode)
		{
			this.opCode = opCode;
		}

		public byte OperationByte
		{
			get => this.emulator.Read(this.RandomPage, this.RandomOffset);
			set => this.emulator.Write(this.RandomPage, this.RandomOffset, value);
		}

		public byte RandomOffset { get; private set; }

		public byte RandomPage { get; private set; }

		public void Initialize(Cpu cpu, EmulatorMock emulator)
		{
			this.cpu = cpu ?? throw new ArgumentNullException(nameof(cpu));
			this.emulator = emulator ?? throw new ArgumentNullException(nameof(emulator));

			var randomAddress = this.emulator.GetRandomAddress();

			this.RandomPage = randomAddress.Page;
			this.RandomOffset = randomAddress.Offset;
		}

		public void RunProgram()
			=> this.cpu.RunProgram(new byte[] { (byte)this.opCode, this.RandomOffset, this.RandomPage });
	}
}
