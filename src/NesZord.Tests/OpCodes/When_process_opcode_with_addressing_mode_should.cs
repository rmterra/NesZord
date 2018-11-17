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
			this.Emulator = new EmulatorMock();
			this.Cpu = new Cpu(this.Emulator);

			this.AddressingMode.Initialize(this.Cpu, this.Emulator);
		}

		protected T AddressingMode { get; }

		protected EmulatorMock Emulator { get; }

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
