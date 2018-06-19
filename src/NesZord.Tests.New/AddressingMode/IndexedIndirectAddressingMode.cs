using AutoFixture;
using NesZord.Core;
using System;

namespace NesZord.Tests.New.AddressingMode
{
	public class IndexedIndirectAddressingMode : IAddressingMode
	{
		private static Fixture fixture = new Fixture();

		private Microprocessor processor;

		private MemoryMock memory;

		private OpCode opCode;

		public IndexedIndirectAddressingMode(OpCode opCode)
		{
			this.opCode = opCode;
		}

		public byte OperationByte
		{
			get => this.memory.Read(memory.GetIndexedIndirectLocation(this.RandomOffset, this.XRegisterValue));
			set => this.memory.MockIndexedIndirectMemoryWrite(this.RandomOffset, this.XRegisterValue, value);
		}

		public byte RandomOffset { get; private set; }

		public byte XRegisterValue { get; private set; }

		public void Initialize(Microprocessor processor, MemoryMock memory)
		{
			this.processor = processor ?? throw new ArgumentNullException(nameof(processor));
			this.memory = memory ?? throw new ArgumentNullException(nameof(memory));

			this.RandomOffset = fixture.Create<byte>();
			this.XRegisterValue = fixture.Create<byte>();
		}

		public void RunProgram()
			=> this.processor.RunProgram(new byte[]
			{
				(byte)OpCode.LDX_Immediate, this.XRegisterValue,
				(byte)this.opCode, this.RandomOffset
			});
	}
}
