﻿using NesZord.Core;
using NesZord.Tests.AddressingMode;

namespace NesZord.Tests.OpCodes.CMP
{
	public class When_process_CMP_with_indexed_indirect_addressing_mode_should 
		: When_process_CMP_should<IndexedIndirectAddressingMode>
	{
		public When_process_CMP_with_indexed_indirect_addressing_mode_should() 
			: base(new IndexedIndirectAddressingMode(OpCode.CMP_IndexedIndirect))
		{
		}
	}
}
