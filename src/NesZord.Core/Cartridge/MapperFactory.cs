namespace NesZord.Core.Cartridge
{
	internal class MapperFactory
	{
		private MapperFactory() { }

		internal static IMapper Create(int mapperId)
		{
			return new Mapper003();
		}
	}
}
