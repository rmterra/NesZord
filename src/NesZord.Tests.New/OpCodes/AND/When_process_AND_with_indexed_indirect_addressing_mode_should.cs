﻿using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.AND
{
	public class When_process_AND_with_indexed_indirect_addressing_mode_should
		: When_process_AND_should<IndexedIndirectAddressingMode>
	{
		public When_process_AND_with_indexed_indirect_addressing_mode_should()
			: base(new IndexedIndirectAddressingMode(OpCode.AND_IndexedIndirect))
		{
		}
	}
}
