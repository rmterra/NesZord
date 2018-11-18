using AutoFixture;
using NesZord.Core;
using System;

namespace NesZord.Tests.AddressingMode
{
	public class ZeroPageXAddressingMode : IAddressingMode
	{
		private static Fixture fixture = new Fixture();

		private readonly OpCode opCode;

		private Cpu cpu;

		private EmulatorMock emulator;

		public ZeroPageXAddressingMode(OpCode opCode)
		{
			this.opCode = opCode;
		}

		public byte OperationByte
		{
			get => this.emulator.ReadZeroPage((byte)(this.XRegisterValue + this.RandomOffset));
			set => this.emulator.WriteZeroPage((byte)(this.XRegisterValue + this.RandomOffset), value);
		}

		public byte RandomOffset { get; private set; }

		public byte XRegisterValue { get; private set; }

		public void Initialize(Cpu cpu, EmulatorMock emulator)
		{
			this.cpu = cpu ?? throw new ArgumentNullException(nameof(cpu));
			this.emulator = emulator ?? throw new ArgumentNullException(nameof(emulator));

			this.RandomOffset = this.emulator.GetRandomOffset();
			this.XRegisterValue = fixture.Create<byte>();
		}

		public void RunProgram()
			=> this.cpu.RunProgram(new byte[]
			{
				(byte)OpCode.LDX_Immediate, this.XRegisterValue,
				(byte)this.opCode, this.RandomOffset
			});
	}
}
