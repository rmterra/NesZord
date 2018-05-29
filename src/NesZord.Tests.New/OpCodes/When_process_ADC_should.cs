using NesZord.Core;
using Xunit;

namespace NesZord.Tests.New.OpCodes
{
	public class When_process_ADC_should
	{
		private readonly MemoryMock memory;

		private readonly Microprocessor processor;

		private byte accumulatorValue;

		private byte byteToAdd;

		public When_process_ADC_should()
		{
			this.memory = new MemoryMock();
			this.processor = new Microprocessor(this.memory);

			this.accumulatorValue = 0x05;
			this.byteToAdd = 0x05;
		}

		[Fact]
		public void Add_the_specified_value_to_accumulator()
		{
			// Act
			this.RunProgram();

			//Arrange
			Assert.Equal(0x0a, this.processor.Accumulator.Value);
		}

		[Fact]
		public void Not_set_carry_flag()
		{
			// Act
			this.RunProgram();

			//Arrange
			Assert.False(this.processor.Carry);
		}

		[Fact]
		public void Not_set_overflow_flag()
		{
			// Act
			this.RunProgram();

			//Arrange
			Assert.False(this.processor.Overflow);
		}

		[Fact]
		public void Not_set_negative_flag()
		{
			// Act
			this.RunProgram();

			//Arrange
			Assert.False(this.processor.Negative);
		}

		[Fact]
		public void Not_set_zero_flag()
		{
			// Act
			this.RunProgram();

			//Arrange
			Assert.False(this.processor.Zero);
		}

		[Fact]
		public void Add_carry_flag_to_final_result_given_that_it_is_set()
		{
			// Arrange
			this.processor.RunProgram(new byte[] { (byte)OpCode.SEC_Implied });

			//Act
			this.RunProgram();

			//Assert
			Assert.Equal(0x0b, this.processor.Accumulator.Value);
		}

		[Fact]
		public void Set_overflow_flag_given_that_accumulator_sign_bit_is_set()
		{
			// Arrange
			this.accumulatorValue = 0xff;

			// Act
			this.RunProgram();

			//Arrange
			Assert.True(this.processor.Overflow);
		}

		[Fact]
		public void Set_negative_flag_given_that_accumulator_sign_bit_is_set()
		{
			// Arrange
			this.accumulatorValue = 0xff;

			// Act
			this.RunProgram();

			//Arrange
			Assert.True(this.processor.Negative);
		}

		[Fact]
		public void Set_zero_flag_given_that_result_on_accumulator_is_0x00()
		{
			// Arrange
			this.accumulatorValue = 0x00;
			this.byteToAdd = 0x00;

			// Act
			this.RunProgram();

			//Arrange
			Assert.True(this.processor.Zero);
		}

		[Fact]
		public void Set_carry_flag_given_that_result_on_accumulator_is_negative()
		{
			// Arrange
			this.byteToAdd = 0xff;

			// Act
			this.RunProgram();

			//Arrange
			Assert.True(this.processor.Carry);
		}

		[Fact]
		public void Set_carry_flag_given_that_decimal_flag_is_set_and_added_byte_is_0x95()
		{
			// Arrange
			this.processor.RunProgram(new byte[] { (byte)OpCode.SED_Implied });
			this.byteToAdd = 0x95;

			// Act
			this.RunProgram();

			//Arrange
			Assert.True(this.processor.Carry);
		}

		[Fact]
		public void Not_set_carry_flag_given_that_decimal_flag_is_set_and_added_byte_is_0x02()
		{
			// Arrange
			this.processor.RunProgram(new byte[] { (byte)OpCode.SED_Implied });
			this.byteToAdd = 0x02;

			// Act
			this.RunProgram();

			//Arrange
			Assert.False(this.processor.Carry);
		}

		[Fact]
		public void Set_carry_flag_given_that_decimal_flag_is_not_set_and_added_byte_is_0xff()
		{
			// Arrange
			this.byteToAdd = 0xff;

			// Act
			this.RunProgram();

			//Arrange
			Assert.True(this.processor.Carry);
		}

		[Fact]
		public void Not_set_carry_flag_given_that_decimal_flag_is_not_set_and_added_byte_is_0x00()
		{
			// Arrange
			this.byteToAdd = 0x00;

			// Act
			this.RunProgram();

			//Arrange
			Assert.False(this.processor.Carry);
		}

		private void RunProgram()
		{
			this.processor.RunProgram(new byte[]
			{
				(byte)OpCode.LDA_Immediate, this.accumulatorValue,
				(byte)OpCode.ADC_Immediate, this.byteToAdd
			});
		}
	}
}
