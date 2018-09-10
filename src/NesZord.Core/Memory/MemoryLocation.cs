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
		{
			var fullLocation = this.FullLocation;
			var newLocation = fullLocation + value;

			return new MemoryLocation(newLocation.GetOffset(), newLocation.GetPage());
		}
	}
}