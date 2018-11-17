using AutoFixture;
using NesZord.Core;
using NesZord.Tests.Memory;
using System;

namespace NesZord.Tests.AddressingMode
{
	public class IndexedIndirectAddressingMode : IAddressingMode
	{
		private static Fixture fixture = new Fixture();

		private Cpu cpu;

		private EmulatorMock emulator;

		private OpCode opCode;

		public IndexedIndirectAddressingMode(OpCode opCode)
		{
			this.opCode = opCode;
		}

		public byte OperationByte
		{
			get => this.emulator.Read(emulator.GetIndexedIndirectAddress(this.RandomOffset, this.XRegisterValue));
			set => this.emulator.MockIndexedIndirectMemoryWrite(this.RandomOffset, this.XRegisterValue, value);
		}

		public byte RandomOffset { get; private set; }

		public byte XRegisterValue { get; private set; }

		public void Initialize(Cpu cpu, EmulatorMock emulator)
		{
			this.cpu = cpu ?? throw new ArgumentNullException(nameof(cpu));
			this.emulator = emulator ?? throw new ArgumentNullException(nameof(emulator));

			this.RandomOffset = fixture.Create<byte>();
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
