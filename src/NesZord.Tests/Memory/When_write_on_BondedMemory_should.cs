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
			var firstAddress = new MemoryAddress(0x00, 0x10);
			var lastAddress = new MemoryAddress(0x00, 0x20);

			this.memory = new BoundedMemory(firstAddress, lastAddress);
		}

		[Fact]
		public void Throw_ArgumentRangeException_when_received_address_is_below_of_FirstAddress()
		{
			// Act
			var address = new MemoryAddress(0x00, 0x05);
			Action act = () => this.memory.Write(address, 1);

			// Assert
			act.Should().Throw<ArgumentOutOfRangeException>();
		}

		[Fact]
		public void Throw_ArgumentRangeException_when_received_address_is_above_of_LastAddress()
		{
			// Act
			var address = new MemoryAddress(0x00, 0xff);
			Action act = () => this.memory.Write(address, 1);

			// Assert
			act.Should().Throw<ArgumentOutOfRangeException>();
		}

		[Fact]
		public void Store_received_value_when_value_is_above_of_FirstAddress()
		{
			// Arrange
			var address = new MemoryAddress(0x00, 0x11);

			// Act
			this.memory.Write(address, 1);

			// Assert
			this.memory.Read(address).Should().Be(1);
		}

		[Fact]
		public void Store_received_value_when_value_is_below_of_LastAddress()
		{
			// Arrange
			var address = new MemoryAddress(0x00, 0x1f);

			// Act
			this.memory.Write(address, 1);

			// Assert
			this.memory.Read(address).Should().Be(1);
		}
	}
}
