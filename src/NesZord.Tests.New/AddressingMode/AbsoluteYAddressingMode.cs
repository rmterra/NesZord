﻿using AutoFixture;
using NesZord.Core;
using System;

namespace NesZord.Tests.New.AddressingMode
{
	public class AbsoluteYAddressingMode : IAddressingMode
	{
		private static Fixture fixture = new Fixture();

		private Microprocessor processor;

		private MemoryMock memory;

		private MemoryLocation memoryLocation;

		private OpCode opCode;

		public AbsoluteYAddressingMode(OpCode opCode)
		{
			this.opCode = opCode;
		}

		public byte OperationByte
		{
			get => this.memory.Read(this.memoryLocation);
			set => this.memory.Write(this.memoryLocation, value);
		}

		public byte RandomOffset { get; private set; }

		public byte RandomPage { get; private set; }

		public byte YRegisterValue { get; private set; }

		public void Initialize(Microprocessor processor, MemoryMock memory)
		{
			this.processor = processor ?? throw new ArgumentNullException(nameof(processor));
			this.memory = memory ?? throw new ArgumentNullException(nameof(memory));

			this.RandomOffset = fixture.Create<byte>();
			this.RandomPage = fixture.Create<byte>();
			this.YRegisterValue = fixture.Create<byte>();

			this.memoryLocation = new MemoryLocation(this.RandomOffset, this.RandomPage).Sum(this.YRegisterValue);
		}

		public void RunProgram()
			=> this.processor.RunProgram(new byte[]
			{
				(byte)OpCode.LDY_Immediate, this.YRegisterValue,
				(byte)this.opCode, this.RandomOffset, this.RandomPage
			});
	}
}
