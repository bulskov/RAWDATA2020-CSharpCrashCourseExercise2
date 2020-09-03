using System;
using Exercise2;
using FluentAssertions;
using Xunit;

namespace Exercise2Tests
{
    public class ReversePolishCalculatorTests
    {
        [Fact]
        public void Compute_WithEmptyString_ReturnZero()
        {
            ReversePolishCalculator.Compute("").Should().Be(0);
        }
        
        [Fact]
        public void Compute_WithNullString_ReturnZero()
        {
            ReversePolishCalculator.Compute(null).Should().Be(0);
        }

        [Fact]
        public void Compute_WithSingleNumber_ReturnTheNumber()
        {
            ReversePolishCalculator.Compute("7").Should().Be(7);
        }

        [Fact]
        public void Compute_WithAdditionOfTwoNumbers_ReturnTheSum()
        {
            ReversePolishCalculator.Compute("7 5 +").Should().Be(12);
        }

        [Fact]
        public void Compute_WithSubstractionOfTwoNumbers_ReturnTheDifference()
        {
            ReversePolishCalculator.Compute("7 5 -").Should().Be(2);
        }

        [Fact]
        public void Compute_WithMultiplicationOfTwoNumbers_ReturnTheProduct()
        {
            ReversePolishCalculator.Compute("7 5 *").Should().Be(35);
        }

        [Fact]
        public void Compute_WithDivisionOfTwoNumbers_ReturnTheQuotient()
        {
            ReversePolishCalculator.Compute("10 5 /").Should().Be(2);
        }

        [Fact]
        public void Compute_WithNumberRaisedToExponent_ReturnThePower()
        {
            ReversePolishCalculator.Compute("10 5 ^").Should().Be(100000);
        }

        [Fact]
        public void Compute_512Plus4TimesPlus3Minus_Return14()
        {
            ReversePolishCalculator.Compute("5 1 2 + 4 * + 3 -").Should().Be(14);
        }

        [Fact]
        public void Compute_Sqrt16_Return4()
        {
            ReversePolishCalculator.Compute("16 sqrt").Should().Be(4);
        }

        [Fact]
        public void Compute_Pow2To8_Return256()
        {
            ReversePolishCalculator.Compute("2 8 pow").Should().Be(256);

        }

        [Fact]
        public void Compute_InvalidExpression_ThrowsException()
        {
            Action act = () => ReversePolishCalculator.Compute("5 1");
            act.Should().Throw<ArgumentException>();

        }

        [Fact]
        public void Compute_InvalidExpression2_ThrowsException()
        {
            Action act = () => ReversePolishCalculator.Compute("+ 1");
            act.Should().Throw<ArgumentException>();
        }
    }
}
