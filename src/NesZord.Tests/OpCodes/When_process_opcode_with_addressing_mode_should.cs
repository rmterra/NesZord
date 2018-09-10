using NesZord.Core;
using NesZord.Tests.AddressingMode;
using NesZord.Tests.Memory;
using System;

namespace NesZord.Tests.OpCodes
{
	public abstract class When_process_opcode_with_addressing_mode_should<T> where T : IAddressingMode
	{
		public When_process_opcode_with_addressing_mode_should(T addressingMode)
		{
#pragma warning disable IDE0016 // Use 'throw' expression
			if (addressingMode == null) { throw new ArgumentNullException(nameof(addressingMode)); }
#pragma warning restore IDE0016 // Use 'throw' expression

			this.AddressingMode = addressingMode;
			this.Memory = new MemoryMapperMock();
			this.Cpu = new Cpu(this.Memory);

			this.AddressingMode.Initialize(this.Cpu, this.Memory);
		}

		protected T AddressingMode { get; }

		protected MemoryMapperMock Memory { get; }

		protected byte OperationByte
		{
			get { return this.AddressingMode.OperationByte; }
			set { this.AddressingMode.OperationByte = value; }
		}

		protected Cpu Cpu { get; }

		protected void RunProgram()
			=> this.AddressingMode.RunProgram();
	}
}
