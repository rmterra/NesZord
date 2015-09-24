using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NesZord.Core
{
	internal class MemoryLocation
	{
		internal MemoryLocation(byte offset, byte page)
		{
			this.Offset = offset;
			this.Page = page;
		}

		internal byte Offset { get; private set; }

		internal byte Page { get; private set; }
	}
}
