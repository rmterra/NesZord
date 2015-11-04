using NesZord.Core;
using NSpec;
using Ploeh.AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesZord.Tests
{
	public class Describe_memory : nspec
	{
		private readonly Fixture fixture = new Fixture();

		private Memory memory;

		public void before_each() { this.memory = new Memory(); }

		public void When_load_memory_with_program()
		{
			var randomByteCount = new Random().Next(0, 10);
			var bytes = this.fixture.CreateMany<byte>(randomByteCount);

			act = () => { this.memory.LoadMemory(bytes.ToArray()); };

			for (int i = 0; i < randomByteCount - 1; i++)
			{
				String spec = String.Format("should memory at at 0x06 0x0{0:x} be 0x{1:x}", i, bytes.ElementAt(i));
				it[spec] = () => { this.memory.Read(0x06, (byte)i).should_be(bytes.ElementAt(i)); };
			}
		}
	}
}
