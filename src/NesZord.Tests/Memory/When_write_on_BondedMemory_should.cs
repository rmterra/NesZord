using FluentAssertions;
using NesZord.Core.Memory;
using System;
using Xunit;

namespace NesZord.Tests.Memory
{
	public class When_write_on_BondedMemory_should
	{
		private BoundedMemory memory;

		public When_write_on_BondedMemory_should()
		{
			var firstAddress = new MemoryLocation(0x10, 0x00);
			var lastAddress = new MemoryLocation(0x20, 0x00);

			this.memory = new BoundedMemory(firstAddress, lastAddress);
		}

		[Fact]
		public void Throw_ArgumentRangeException_when_received_location_is_below_of_FirstAddress()
		{
			// Act
			var location = new MemoryLocation(0x05, 0x00);
			Action act = () => this.memory.Write(location, 1);

			// Assert
			act.Should().Throw<ArgumentOutOfRangeException>();
		}

		[Fact]
		public void Throw_ArgumentRangeException_when_received_location_is_above_of_LastAddress()
		{
			// Act
			var location = new MemoryLocation(0xff, 0x00);
			Action act = () => this.memory.Write(location, 1);

			// Assert
			act.Should().Throw<ArgumentOutOfRangeException>();
		}

		[Fact]
		public void Store_received_value_when_value_is_above_of_FirstAddress()
		{
			// Arrange
			var location = new MemoryLocation(0x11, 0x00);

			// Act
			this.memory.Write(location, 1);

			// Assert
			this.memory.Read(location).Should().Be(1);
		}

		[Fact]
		public void Store_received_value_when_value_is_below_of_LastAddress()
		{
			// Arrange
			var location = new MemoryLocation(0x1f, 0x00);

			// Act
			this.memory.Write(location, 1);

			// Assert
			this.memory.Read(location).Should().Be(1);
		}
	}
}
