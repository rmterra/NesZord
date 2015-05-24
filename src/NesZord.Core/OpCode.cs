using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NesZord.Core
{
	public enum OpCode
	{
		Break = 0x00,

		ImmediateAddWithCarry = 0x69,

		AbsoluteStoreAccumulator = 0x8d,

		ImmediateLoadAccumulator = 0xa9,

		TransferFromAccumulatorToX = 0xaa,

		IncrementValueAtX = 0xe8
	}
}