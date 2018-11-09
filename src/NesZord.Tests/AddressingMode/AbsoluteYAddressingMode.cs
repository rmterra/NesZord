using AutoFixture;
using NesZord.Core;
using NesZord.Core.Memory;
using NesZord.Tests.Memory;
using System;

namespace NesZord.Tests.AddressingMode
{
	public class AbsoluteYAddressingMode : IAddressingMode
	{
		private static Fixture fixture = new Fixture();

		private Cpu cpu;

		private MemoryMapperMock memory;

		private MemoryAddress memoryAddress;

		private OpCode opCode;

		public AbsoluteYAddressingMode(OpCode opCode)
		{
			this.opCode = opCode;
		}

		public byte OperationByte
		{
			get => this.memory.Read(this.memoryAddress);
			set => this.memory.Write(this.memoryAddress, value);
		}

		public byte RandomOffset { get; private set; }

		public byte RandomPage { get; private set; }

		public byte YRegisterValue { get; private set; }

		public void Initialize(Cpu cpu, MemoryMapperMock memory)
		{
			this.cpu = cpu ?? throw new ArgumentNullException(nameof(cpu));
			this.memory = memory ?? throw new ArgumentNullException(nameof(memory));

			this.RandomOffset = fixture.Create<byte>();
			this.RandomPage = fixture.Create<byte>();
			this.YRegisterValue = fixture.Create<byte>();

			this.memoryAddress = new MemoryAddress(this.RandomOffset, this.RandomPage).Sum(this.YRegisterValue);
		}

		public void RunProgram()
			=> this.cpu.RunProgram(new byte[]
			{
				(byte)OpCode.LDY_Immediate, this.YRegisterValue,
				(byte)this.opCode, this.RandomOffset, this.RandomPage
			});
	}
}
