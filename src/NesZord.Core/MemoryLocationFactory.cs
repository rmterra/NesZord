using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NesZord.Core
{
	internal static class MemoryLocationFactory
	{
		internal static MemoryLocation Create(AddressingMode addressingMode, Microprocessor processor)
		{
			var offset = default(byte);

			if (addressingMode == AddressingMode.Absolute) { offset = processor.ReadProgramByte(); }
			else if (addressingMode == AddressingMode.AbsoluteX) { offset = (byte)(processor.ReadProgramByte() + processor.X); }
			else if (addressingMode == AddressingMode.AbsoluteY) { offset = (byte)(processor.ReadProgramByte() + processor.Y); }

			return new MemoryLocation(offset, processor.ReadProgramByte());
		}
	}
}
