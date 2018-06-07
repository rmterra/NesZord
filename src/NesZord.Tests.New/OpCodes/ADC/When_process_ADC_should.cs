using NesZord.Core;
using Xunit;

namespace NesZord.Tests.New.OpCodes.ADC
{
	public abstract class When_process_ADC_should
	{
		public When_process_ADC_should()
		{
			this.Memory = new MemoryMock();
			this.Processor = new Microprocessor(this.Memory);

			this.Initialize();

			this.AccumulatorValue = 0x05;
			this.SetByteToAdd(0x05);
		}

		protected byte AccumulatorValue { get; private set; }

		protected byte ByteToAdd { get; set; }

		protected MemoryMock Memory { get; }

		protected Microprocessor Processor { get; }

		protected abstract void RunProgram();

		protected abstract void SetByteToAdd(byte value);

		protected virtual void Initialize()
		{
		}

		[Fact]
		public void Add_the_specified_value_to_accumulator()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Processor.Accumulator.Value.Should().Be(0x0a);
		}

		[Fact]
		public void Not_set_carry_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Processor.Carry.Should().BeFalse();
		}

		[Fact]
		public void Not_set_overflow_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Processor.Overflow.Should().BeFalse();
		}

		[Fact]
		public void Not_set_negative_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Processor.Negative.Should().BeFalse();
		}

		[Fact]
		public void Not_set_zero_flag()
		{
			// Act
			this.RunProgram();

			// Assert
			this.Processor.Zero.Should().BeFalse();
		}

		[Fact]
		public void Add_carry_flag_to_final_result_given_that_it_is_set()
		{
			// Arrange
			this.Processor.RunProgram(new byte[] { (byte)OpCode.SEC_Implied });

			//Act
			this.RunProgram();

			// Assert
			this.Processor.Accumulator.Value.Should().Be(0x0b);
		}

		[Fact]
		public void Set_overflow_flag_given_that_accumulator_sign_bit_is_set()
		{
			// Arrange
			this.AccumulatorValue = 0xff;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Overflow.Should().BeTrue();
		}

		[Fact]
		public void Set_negative_flag_given_that_accumulator_sign_bit_is_set()
		{
			// Arrange
			this.AccumulatorValue = 0xff;

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Negative.Should().BeTrue();
		}

		[Fact]
		public void Set_zero_flag_given_that_result_on_accumulator_is_0x00()
		{
			// Arrange
			this.AccumulatorValue = 0x00;
			this.SetByteToAdd(0x00);

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Zero.Should().BeTrue();
		}

		[Fact]
		public void Set_carry_flag_given_that_result_on_accumulator_is_negative()
		{
			// Arrange
			this.SetByteToAdd(0xff);

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Carry.Should().BeTrue();
		}

		[Fact]
		public void Set_carry_flag_given_that_decimal_flag_is_set_and_added_byte_is_0x95()
		{
			// Arrange
			this.Processor.RunProgram(new byte[] { (byte)OpCode.SED_Implied });
			this.SetByteToAdd(0x95);

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Carry.Should().BeTrue();
		}

		[Fact]
		public void Not_set_carry_flag_given_that_decimal_flag_is_set_and_added_byte_is_0x02()
		{
			// Arrange
			this.Processor.RunProgram(new byte[] { (byte)OpCode.SED_Implied });
			this.SetByteToAdd(0x02);

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Carry.Should().BeFalse();
		}

		[Fact]
		public void Set_carry_flag_given_that_decimal_flag_is_not_set_and_added_byte_is_0xff()
		{
			// Arrange
			this.SetByteToAdd(0xff);

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Carry.Should().BeTrue();
		}

		[Fact]
		public void Not_set_carry_flag_given_that_decimal_flag_is_not_set_and_added_byte_is_0x00()
		{
			// Arrange
			this.SetByteToAdd(0x00);

			// Act
			this.RunProgram();

			// Assert
			this.Processor.Carry.Should().BeFalse();
		}
	}
}
