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

		AbsoluteStoreXRegister = 0x8e,

		ImmediateLoadYRegister = 0xa0,

		ImmediateLoadXRegister = 0xa2,

		ImmediateLoadAccumulator = 0xa9,

		TransferFromAccumulatorToX = 0xaa,

		DecrementValueAtX = 0xca,

		BranchIfNotEqual = 0xd0,

		ImmediateCompareXRegister = 0xe0,

		IncrementValueAtX = 0xe8
	}
}