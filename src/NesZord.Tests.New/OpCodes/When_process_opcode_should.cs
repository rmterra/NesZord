using NesZord.Core;
using NesZord.Tests.New.AddressingMode;
using System;

namespace NesZord.Tests.New.OpCodes
{
	public abstract class When_process_opcode_should<T> where T : IAddressingMode
	{
		public When_process_opcode_should(T addressingMode)
		{
#pragma warning disable IDE0016 // Use 'throw' expression
			if (addressingMode == null) { throw new ArgumentNullException(nameof(addressingMode)); }
#pragma warning restore IDE0016 // Use 'throw' expression

			this.AddressingMode = addressingMode;
			this.Memory = new MemoryMock();
			this.Processor = new Microprocessor(this.Memory);
			
			this.AddressingMode.Initialize(this.Processor, this.Memory);
		}

		protected T AddressingMode { get; }

		protected MemoryMock Memory { get; }

		protected byte OperationByte
		{
			get { return this.AddressingMode.OperationByte; }
			set { this.AddressingMode.OperationByte = value; }
		}

		protected Microprocessor Processor { get; }

		protected abstract void RunProgram();
	}
}
