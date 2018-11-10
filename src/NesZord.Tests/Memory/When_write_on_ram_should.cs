using FluentAssertions;
using NesZord.Core.Memory;
using Xunit;

namespace NesZord.Tests.Memory
{
	public class When_write_on_ram_should
	{
		private Ram ram;

		public When_write_on_ram_should()
		{
			this.ram = new Ram();
		}

		[Fact]
		public void Given_that_received_a_main_address_should_be_possible_to_read_stored_value()
		{
			// Arrange
			var address = new MemoryAddress(0x00, 0x01);

			// Act -> Assert
			this.Given_that_received_an_address_should_be_possible_to_read_stored_value(address, 0x05);
		}

		[Fact]
		public void Given_that_received_a_main_address_should_replicate_stored_value_to_mirror1()
		{
			// Arrange
			var address = new MemoryAddress(0x00, 0x01);
			var mirrorAddress = new MemoryAddress(0x08, 0x01);

			// Act -> Assert
			this.Given_that_received_an_address_should_replicate_stored_value_to_another_address(address, mirrorAddress, 0x05);
		}

		[Fact]
		public void Given_that_received_a_main_address_should_replicate_stored_value_to_mirror2()
		{
			// Arrange
			var address = new MemoryAddress(0x00, 0x01);
			var mirrorAddress = new MemoryAddress(0x10, 0x01);

			// Act -> Assert
			this.Given_that_received_an_address_should_replicate_stored_value_to_another_address(address, mirrorAddress, 0x05);
		}

		[Fact]
		public void Given_that_received_a_main_address_should_replicate_stored_value_to_mirror3()
		{
			// Arrange
			var address = new MemoryAddress(0x00, 0x01);
			var mirrorAddress = new MemoryAddress(0x18, 0x01);

			// Act -> Assert
			this.Given_that_received_an_address_should_replicate_stored_value_to_another_address(address, mirrorAddress, 0x05);
		}

		[Fact]
		public void Given_that_received_a_mirror1_address_should_be_possible_to_read_stored_value()
		{
			// Arrange
			var address = new MemoryAddress(0x08, 0x01);

			// Act -> Assert
			this.Given_that_received_an_address_should_be_possible_to_read_stored_value(address, 0x05);
		}

		[Fact]
		public void Given_that_received_a_mirror1_address_should_replicate_stored_value_to_main_memory()
		{
			// Arrange
			var address = new MemoryAddress(0x08, 0x01);
			var mirrorAddress = new MemoryAddress(0x00, 0x01);

			// Act -> Assert
			this.Given_that_received_an_address_should_replicate_stored_value_to_another_address(address, mirrorAddress, 0x05);
		}

		[Fact]
		public void Given_that_received_a_mirror1_address_should_replicate_stored_value_to_mirror2()
		{
			// Arrange
			var address = new MemoryAddress(0x08, 0x01);
			var mirrorAddress = new MemoryAddress(0x10, 0x01);

			// Act -> Assert
			this.Given_that_received_an_address_should_replicate_stored_value_to_another_address(address, mirrorAddress, 0x05);
		}

		[Fact]
		public void Given_that_received_a_mirror1_address_should_replicate_stored_value_to_mirror3()
		{
			// Arrange
			var address = new MemoryAddress(0x08, 0x01);
			var mirrorAddress = new MemoryAddress(0x18, 0x01);

			// Act -> Assert
			this.Given_that_received_an_address_should_replicate_stored_value_to_another_address(address, mirrorAddress, 0x05);
		}

		[Fact]
		public void Given_that_received_a_mirror2_address_should_be_possible_to_read_stored_value()
		{
			// Arrange
			var address = new MemoryAddress(0x10, 0x01);

			// Act -> Assert
			this.Given_that_received_an_address_should_be_possible_to_read_stored_value(address, 0x05);
		}

		[Fact]
		public void Given_that_received_a_mirror2_address_should_replicate_stored_value_to_main_memory()
		{
			// Arrange
			var address = new MemoryAddress(0x10, 0x01);
			var mirrorAddress = new MemoryAddress(0x00, 0x01);

			// Act -> Assert
			this.Given_that_received_an_address_should_replicate_stored_value_to_another_address(address, mirrorAddress, 0x05);
		}

		[Fact]
		public void Given_that_received_a_mirror2_address_should_replicate_stored_value_to_mirror1()
		{
			// Arrange
			var address = new MemoryAddress(0x10, 0x01);
			var mirrorAddress = new MemoryAddress(0x08, 0x01);

			// Act -> Assert
			this.Given_that_received_an_address_should_replicate_stored_value_to_another_address(address, mirrorAddress, 0x05);
		}

		[Fact]
		public void Given_that_received_a_mirror2_address_should_replicate_stored_value_to_mirror3()
		{
			// Arrange
			var address = new MemoryAddress(0x10, 0x01);
			var mirrorAddress = new MemoryAddress(0x18, 0x01);

			// Act -> Assert
			this.Given_that_received_an_address_should_replicate_stored_value_to_another_address(address, mirrorAddress, 0x05);
		}

		[Fact]
		public void Given_that_received_a_mirror3_address_should_be_possible_to_read_stored_value()
		{
			// Arrange
			var address = new MemoryAddress(0x18, 0x01);

			// Act -> Assert
			this.Given_that_received_an_address_should_be_possible_to_read_stored_value(address, 0x05);
		}

		[Fact]
		public void Given_that_received_a_mirror3_address_should_replicate_stored_value_to_main_memory()
		{
			// Arrange
			var address = new MemoryAddress(0x18, 0x01);
			var mirrorAddress = new MemoryAddress(0x00, 0x01);

			// Act -> Assert
			this.Given_that_received_an_address_should_replicate_stored_value_to_another_address(address, mirrorAddress, 0x05);
		}

		[Fact]
		public void Given_that_received_a_mirror3_address_should_replicate_stored_value_to_mirror1()
		{
			// Arrange
			var address = new MemoryAddress(0x18, 0x01);
			var mirrorAddress = new MemoryAddress(0x08, 0x01);

			// Act -> Assert
			this.Given_that_received_an_address_should_replicate_stored_value_to_another_address(address, mirrorAddress, 0x05);
		}

		[Fact]
		public void Given_that_received_a_mirror3_address_should_replicate_stored_value_to_mirror2()
		{
			// Arrange
			var address = new MemoryAddress(0x18, 0x01);
			var mirrorAddress = new MemoryAddress(0x10, 0x01);

			// Act -> Assert
			this.Given_that_received_an_address_should_replicate_stored_value_to_another_address(address, mirrorAddress, 0x05);
		}

		private void Given_that_received_an_address_should_be_possible_to_read_stored_value(MemoryAddress address, byte value)
		{
			// Act
			this.ram.Write(address, value);

			// Assert
			this.ram.Read(address).Should().Be(value);
		}

		private void Given_that_received_an_address_should_replicate_stored_value_to_another_address(MemoryAddress address, MemoryAddress another, byte value)
		{
			// Act
			this.ram.Write(address, value);

			// Assert
			this.ram.Read(another).Should().Be(value);
		}
	}
}
