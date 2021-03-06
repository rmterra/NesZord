﻿using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.ORA
{
	public class When_process_ORA_with_indirect_indexed_addressing_mode_should
		: When_process_ORA_should<IndirectIndexedAddressingMode>
	{
		public When_process_ORA_with_indirect_indexed_addressing_mode_should()
			: base(new IndirectIndexedAddressingMode(OpCode.ORA_IndirectIndexed))
		{
		}
	}
}
