using AutoFixture;
using NesZord.Core;
using System;

namespace NesZord.Tests.New.AddressingMode
{
	public class IndirectIndexedAddressingMode : IAddressingMode
	{
		private static Fixture fixture = new Fixture();

		private Microprocessor processor;

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

		public void Initialize(Microprocessor processor, MemoryMock memory)
		{
			this.processor = processor ?? throw new ArgumentNullException(nameof(processor));
			this.memory = memory ?? throw new ArgumentNullException(nameof(memory));

			this.RandomOffset = fixture.Create<byte>();
			this.YRegisterValue = fixture.Create<byte>();
		}

		public void RunProgram()
			=> this.processor.RunProgram(new byte[]
			{
				(byte)OpCode.LDY_Immediate, this.YRegisterValue,
				(byte)this.opCode, this.RandomOffset
			});
	}
}
