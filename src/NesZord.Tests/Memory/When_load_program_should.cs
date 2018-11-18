using AutoFixture;
using FluentAssertions;
using NesZord.Core;
using System;
using System.Linq;
using Xunit;

namespace NesZord.Tests.New
{
	public class When_load_program_should
	{
		private static Random random = new Random();

		private readonly Fixture fixture = new Fixture();

		private Emulator emulator;

		public When_load_program_should()
		{
			this.emulator = new Emulator();
		}

		[Fact]
		public void Store_it_on_0x06_page()
		{
			// Arrange
			var randomByteCount = random.Next(0, 10);
			var bytes = this.fixture.CreateMany<byte>(randomByteCount);

			// Act
			this.emulator.LoadProgram(bytes.ToArray());

			// Assert
			for (int i = 0; i < randomByteCount - 1; i++)
			{
				this.emulator.Read(0x06, (byte)i).Should().Equals(bytes.ElementAt(i));
			}
		}
	}
}
