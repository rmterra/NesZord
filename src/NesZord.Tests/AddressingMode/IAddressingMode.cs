﻿using NesZord.Core;
using NesZord.Tests.Memory;

namespace NesZord.Tests.AddressingMode
{
	public interface IAddressingMode
	{
		byte OperationByte { get; set; }

		void Initialize(Cpu cpu, MemoryMapperMock memory);

		void RunProgram();
	}
}
