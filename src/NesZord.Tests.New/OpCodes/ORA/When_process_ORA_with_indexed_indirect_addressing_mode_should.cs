﻿using NesZord.Core;
using NesZord.Tests.New.AddressingMode;

namespace NesZord.Tests.New.OpCodes.ORA
{
	public class When_process_ORA_with_indexed_indirect_addressing_mode_should
		: When_process_ORA_should<IndexedIndirectAddressingMode>
	{
		public When_process_ORA_with_indexed_indirect_addressing_mode_should()
			: base(new IndexedIndirectAddressingMode(OpCode.ORA_IndexedIndirect))
		{
		}
	}
}
