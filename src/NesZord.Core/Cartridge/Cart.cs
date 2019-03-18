using NesZord.Core.Memory;
using System;
using System.IO;
using System.Text;

namespace NesZord.Core.Cartridge
{
	public class Cart
	{
		private const int PROGRAM_BANK_SIZE = 16 * 1024;

		private const int GRAPHIC_BANK_SIZE = 8 * 1024;

		private BoundedMemory[] graphicBanks;

		private BoundedMemory[] programBanks;

		private IMapper mapper;

		private Cart() { }

		public bool HasPersistentStorage { get; private set; }

		public bool HasTrainerData { get; private set; }

		public bool IgnoreMirroringControl { get; private set; }

		public bool UseVerticalMirroring { get; private set; }

		public int MapperId { get; private set; }

		public static Cart Create(byte[] cartBuffer)
		{
			if (cartBuffer == null) { throw new ArgumentNullException(nameof(cartBuffer)); }

			var cart = new Cart();

			using (var stream = new MemoryStream(cartBuffer))
			{
				var buffer = new byte[4];
				stream.Read(buffer, 0, buffer.Length);

				var headerId = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
				if (headerId != "NES\x1a") { throw new ArgumentException($"Invalid cart header: {headerId}"); }

				// PRG Banks comes in size of 16KB but CPU reserve 32KB for PRG
				cart.programBanks = new BoundedMemory[stream.ReadByte() / 2];
				cart.graphicBanks = new BoundedMemory[stream.ReadByte()];

				var flag6 = stream.ReadByte();

				cart.UseVerticalMirroring = Convert.ToBoolean(flag6 & 0b0000_0001);
				cart.HasPersistentStorage = Convert.ToBoolean(flag6 & 0b0000_0010);
				cart.HasTrainerData = Convert.ToBoolean(flag6 & 0b0000_0100);
				cart.IgnoreMirroringControl = Convert.ToBoolean(flag6 & 0b0000_1000);

				cart.MapperId = flag6 >> 4;
				cart.mapper = MapperFactory.Create(cart.MapperId);

				// Ignored for now
				var flag7 = stream.ReadByte();
				var flag8 = stream.ReadByte();
				var flag9 = stream.ReadByte();
				var flag10 = stream.ReadByte();

				// Ignore blank header padding 11-15
				stream.Seek(5, SeekOrigin.Current);

				for (int i = 0; i < cart.programBanks.Length; i++)
				{
					var programBuffer = new byte[PROGRAM_BANK_SIZE * 2];
					stream.Read(programBuffer, 0, programBuffer.Length);
					cart.programBanks[i] = new BoundedMemory(programBuffer);
				}

				for (int i = 0; i < cart.graphicBanks.Length; i++)
				{
					var graphicBuffer = new byte[GRAPHIC_BANK_SIZE];
					stream.Read(graphicBuffer, 0, graphicBuffer.Length);
					cart.graphicBanks[i] = new BoundedMemory(graphicBuffer);
				}
			}

			return cart;
		}

		public byte ReadGraphics(MemoryAddress address)
			=> this.Read(this.graphicBanks, this.mapper.GraphicBank, address);

		public byte ReadProgram(MemoryAddress address)
			=> this.Read(this.programBanks, this.mapper.ProgramBank, address);

		private byte Read(BoundedMemory[] banks, int index, MemoryAddress address)
		{
			if (address == null) { throw new ArgumentNullException(nameof(address)); }

			var normalizedAddress = address.And(banks[index].LastAddress);
			return banks[index].Read(address);
		}

		public void WriteProgram(MemoryAddress address, byte value)
		{
			if (address == null) { throw new ArgumentNullException(nameof(address)); }

			this.mapper.Write(address, value);
		}
	}
}
