using AutoFixture;
using NesZord.Core;
using NesZord.Core.Memory;
using NesZord.Tests.Memory;
using System;

namespace NesZord.Tests.AddressingMode
{
	public class ZeroPageAddressingMode : IAddressingMode
	{
		private static Fixture fixture = new Fixture();

		private readonly OpCode opCode;

		private Cpu cpu;

		private EmulatorMock emulator;

		public ZeroPageAddressingMode(OpCode opCode)
		{
			this.opCode = opCode;
		}

		public byte OperationByte
		{
			get => this.emulator.Read(this.RandomOffset, Emulator.ZERO_PAGE);
			set => this.emulator.WriteZeroPage(this.RandomOffset, value);
		}

		public byte RandomOffset { get; private set; }

		public void Initialize(Cpu cpu, EmulatorMock emulator)
		{
			this.cpu = cpu ?? throw new ArgumentNullException(nameof(cpu));
			this.emulator = emulator ?? throw new ArgumentNullException(nameof(emulator));

			this.RandomOffset = fixture.Create<byte>();
		}

		public void RunProgram()
			=> this.cpu.RunProgram(new byte[] { (byte)this.opCode, this.RandomOffset });
	}
}
