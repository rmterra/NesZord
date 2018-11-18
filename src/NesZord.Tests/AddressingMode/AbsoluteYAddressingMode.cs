using AutoFixture;
using NesZord.Core;
using NesZord.Core.Memory;
using System;

namespace NesZord.Tests.AddressingMode
{
	public class AbsoluteYAddressingMode : IAddressingMode
	{
		private static Fixture fixture = new Fixture();

		private Cpu cpu;

		private EmulatorMock emulator;

		private MemoryAddress memoryAddress;

		private OpCode opCode;

		public AbsoluteYAddressingMode(OpCode opCode)
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

		public byte YRegisterValue { get; private set; }

		public void Initialize(Cpu cpu, EmulatorMock emulator)
		{
			this.cpu = cpu ?? throw new ArgumentNullException(nameof(cpu));
			this.emulator = emulator ?? throw new ArgumentNullException(nameof(emulator));

			var randomAddress = this.emulator.GetRandomAddress();

			this.RandomPage = randomAddress.Page;
			this.RandomOffset = randomAddress.Offset;
			this.YRegisterValue = fixture.Create<byte>();

			this.memoryAddress = randomAddress.Sum(this.YRegisterValue);
		}

		public void RunProgram()
			=> this.cpu.RunProgram(new byte[]
			{
				(byte)OpCode.LDY_Immediate, this.YRegisterValue,
				(byte)this.opCode, this.RandomOffset, this.RandomPage
			});
	}
}
