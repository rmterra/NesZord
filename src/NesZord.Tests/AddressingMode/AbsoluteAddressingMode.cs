using AutoFixture;
using NesZord.Core;
using NesZord.Tests.Memory;
using System;

namespace NesZord.Tests.AddressingMode
{
	public class AbsoluteAddressingMode : IAddressingMode
	{
		private static Fixture fixture = new Fixture();

		private Cpu cpu;

		private EmulatorMock emulator;

		private OpCode opCode;

		public AbsoluteAddressingMode(OpCode opCode)
		{
			this.opCode = opCode;
		}

		public byte OperationByte
		{
			get => this.emulator.Read(this.RandomOffset, this.RandomPage);
			set => this.emulator.Write(this.RandomOffset, this.RandomPage, value);
		}

		public byte RandomOffset { get; private set; }

		public byte RandomPage { get; private set; }

		public void Initialize(Cpu cpu, EmulatorMock emulator)
		{
			this.cpu = cpu ?? throw new ArgumentNullException(nameof(cpu));
			this.emulator = emulator ?? throw new ArgumentNullException(nameof(emulator));

			this.RandomOffset = fixture.Create<byte>();
			this.RandomPage = fixture.Create<byte>();
		}

		public void RunProgram()
			=> this.cpu.RunProgram(new byte[] { (byte)this.opCode, this.RandomOffset, this.RandomPage });
	}
}
