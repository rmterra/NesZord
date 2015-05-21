using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NesZord.Core
{
	public enum OpCode
	{
		AbsoluteStoreAccumulator = 0x8d,

		ImmediateLoadAccumulator = 0xa9,

		TransferFromAccumulatorToX = 0xaa,

		IncrementValueAtX = 0xe8
	}
}