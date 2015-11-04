using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NesZord.Core
{
	public class MemoryLocation
	{
		public MemoryLocation(byte offset, byte page)
		{
			this.Offset = offset;
			this.Page = page;
		}

		public byte Offset { get; private set; }

		public byte Page { get; private set; }
	}
}
