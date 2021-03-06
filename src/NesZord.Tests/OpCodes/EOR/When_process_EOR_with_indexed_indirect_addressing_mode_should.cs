﻿using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.EOR
{
	public class When_process_EOR_with_indexed_indirect_addressing_mode_should
		: When_process_EOR_should<IndexedIndirectAddressingMode>
	{
		public When_process_EOR_with_indexed_indirect_addressing_mode_should()
			: base(new IndexedIndirectAddressingMode(OpCode.EOR_IndexedIndirect))
		{
		}
	}
}
