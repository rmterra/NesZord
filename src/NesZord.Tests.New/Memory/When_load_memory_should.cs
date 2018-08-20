using AutoFixture;
using FluentAssertions;
using NesZord.Core;
using System;
using System.Linq;
using Xunit;

namespace NesZord.Tests.New
{
	public class When_load_memory_should
	{
		private static Random random = new Random();

		private readonly Fixture fixture = new Fixture();

		private Memory memory;

		public When_load_memory_should()
		{
			this.memory = new Memory();
		}

		[Fact]
		public void Store_it_on_0x06_page()
		{
			// Arrange
			var randomByteCount = random.Next(0, 10);
			var bytes = this.fixture.CreateMany<byte>(randomByteCount);

			// Act
			this.memory.LoadMemory(bytes.ToArray());

			// Assert
			for (int i = 0; i < randomByteCount - 1; i++)
			{
				this.memory.Read((byte)i, 0x06).Should().Equals(bytes.ElementAt(i));
			}
		}
	}
}
