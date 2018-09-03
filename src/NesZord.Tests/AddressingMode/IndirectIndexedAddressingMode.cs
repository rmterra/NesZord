using AutoFixture;
using NesZord.Core;
using NesZord.Tests.Memory;
using System;

namespace NesZord.Tests.AddressingMode
{
	public class IndirectIndexedAddressingMode : IAddressingMode
	{
		private static Fixture fixture = new Fixture();

		private Cpu cpu;

		private MemoryMock memory;

		private OpCode opCode;

		public IndirectIndexedAddressingMode(OpCode opCode)
		{
			this.opCode = opCode;
		}

		public byte OperationByte
		{
			get => this.memory.Read(memory.GetIndirectIndexedLocation(this.RandomOffset, this.YRegisterValue));
			set => this.memory.MockIndirectIndexedMemoryWrite(this.RandomOffset, this.YRegisterValue, value);
		}

		public byte RandomOffset { get; private set; }

		public byte YRegisterValue { get; private set; }

		public void Initialize(Cpu cpu, MemoryMock memory)
		{
			this.cpu = cpu ?? throw new ArgumentNullException(nameof(cpu));
			this.memory = memory ?? throw new ArgumentNullException(nameof(memory));

			this.RandomOffset = fixture.Create<byte>();
			this.YRegisterValue = fixture.Create<byte>();
		}

		public void RunProgram()
			=> this.cpu.RunProgram(new byte[]
			{
				(byte)OpCode.LDY_Immediate, this.YRegisterValue,
				(byte)this.opCode, this.RandomOffset
			});
	}
}
