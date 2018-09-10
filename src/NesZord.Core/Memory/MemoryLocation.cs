using NesZord.Core.Extensions;

namespace NesZord.Core.Memory
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

		public int FullLocation
		{
			get { return (this.Page << 8) + this.Offset; }
		}

		public MemoryLocation Sum(byte value)
			=> FromInt32(this.FullLocation + value);

		public MemoryLocation And(int value)
			=> FromInt32(this.FullLocation & value);

		public MemoryLocation Or(int value)
			=> FromInt32(this.FullLocation | value);

		private static MemoryLocation FromInt32(int value)
			=> new MemoryLocation(value.GetOffset(), value.GetPage());
	}
}