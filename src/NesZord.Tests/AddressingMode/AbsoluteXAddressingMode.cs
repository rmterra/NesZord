using AutoFixture;
using NesZord.Core;
using NesZord.Core.Memory;
using System;

namespace NesZord.Tests.AddressingMode
{
	public class AbsoluteXAddressingMode : IAddressingMode
	{
		private static Fixture fixture = new Fixture();

		private Cpu cpu;

		private EmulatorMock emulator;

		private MemoryAddress memoryAddress;

		private OpCode opCode;

		public AbsoluteXAddressingMode(OpCode opCode)
		{
			this.opCode = opCode;
		}

		public byte OperationByte
		{
			get => this.emulator.Read(this.memoryAddress);
			set => this.emulator.Write(this.memoryAddress, value);
		}

		public byte RandomOffset { get; private set; }

		public byte RandomPage { get; private set; }

		public byte XRegisterValue { get; private set; }

		public void Initialize(Cpu cpu, EmulatorMock emulator)
		{
			this.cpu = cpu ?? throw new ArgumentNullException(nameof(cpu));
			this.emulator = emulator ?? throw new ArgumentNullException(nameof(emulator));

			var randomAddress = this.emulator.GetRandomAddress();

			this.RandomPage = randomAddress.Page;
			this.RandomOffset = randomAddress.Offset;
			this.XRegisterValue = fixture.Create<byte>();

			this.memoryAddress = randomAddress.Sum(this.XRegisterValue);
		}

		public void RunProgram()
			=> this.cpu.RunProgram(new byte[]
			{
				(byte)OpCode.LDX_Immediate, this.XRegisterValue,
				(byte)this.opCode, this.RandomOffset, this.RandomPage
			});
	}
}
