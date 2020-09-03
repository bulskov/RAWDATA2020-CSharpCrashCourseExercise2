using System;
using Exercise2;
using FluentAssertions;
using Xunit;

namespace Exercise2Tests
{
    public class ShuntingYardTests
    {
        // using the operations from https://en.wikipedia.org/wiki/Shunting-yard_algorithm
        // as input for the tests

        [Fact]
        public void Parse_Number_ReturnNumber()
        {
            ShuntingYard.Parse("3").Should().Be("3");
        }

        [Fact]
        public void Parse_NumberAddition_ReturnRpn()
        {
            ShuntingYard.Parse("3 + 4").Should().Be("3 4 +");
            Calculate("3 + 4").Should().Be(7);
        }

        [Fact]
        public void Parse_AdditionAndMultiplication_ReturnRpn()
        {
            ShuntingYard.Parse("3 + 4 * 2").Should().Be("3 4 2 * +");
            Calculate("3 + 4 * 2").Should().Be(11);
        }

        [Fact]
        public void Parse_ExpressionInParentheses_ReturnRpn()
        {
            ShuntingYard.Parse("( 1 - 5 )").Should().Be("1 5 -");
            Calculate("1 5 -").Should().Be(-4);
        }

        [Fact]
        public void Parse_MissingLeftParenthesesInExpression_ThrowsException()
        {
            Action act = () => ShuntingYard.Parse("1 - 5 )");
            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Parse_CheckRightAssociative_ReturnBool()
        {
            ShuntingYard.CheckRightAssociativeAndPrecedence("^", "-").Should().BeFalse();
            ShuntingYard.CheckRightAssociativeAndPrecedence("^", "*").Should().BeFalse();
            ShuntingYard.CheckRightAssociativeAndPrecedence("^", "+").Should().BeFalse();
            ShuntingYard.CheckRightAssociativeAndPrecedence("^", "/").Should().BeFalse();
        }

        [Theory]
        [InlineData("+", "-", true)]
        [InlineData("+", "*", true)]
        [InlineData("*", "-", false)]
        [InlineData("*", "/", true)]
        [InlineData("*", "^", true)]
        [InlineData("^", "*", false)]
        public void Parse_CheckLeftAssociative_ReturnBool(string op1, string op2, bool expected)
        {
            ShuntingYard.CheckLeftAssociativeAndPrecedence(op1, op2).Should().Be(expected);
        }

        [Fact]
        public void Parse_MixedExpressionWithParentheses_ReturnRpn()
        {
            ShuntingYard.Parse("3 + 4 * 2 / ( 1 - 5 )").Should().Be("3 4 2 * 1 5 - / +");
            Calculate("3 + 4 * 2 / ( 1 - 5 )").Should().Be(1);
        }

        [Fact]
        public void Parse_MixedExpressionWithParenthesesAndPower_ReturnRpn()
        {
            ShuntingYard.Parse("3 + 4 * 2 / ( 1 - 5 ) ^ 2 ^ 3").Should().Be("3 4 2 * 1 5 - 2 3 ^ ^ / +");
        }

        [Fact]
        public void Parse_Sqrt_ReturnRpn()
        {
            ShuntingYard.Parse("sqrt ( 16 )").Should().Be("16 sqrt");
            Calculate("sqrt ( 16 )").Should().Be(4);
        }

        [Fact]
        public void Parse_Pow_ReturnRpn()
        {
            ShuntingYard.Parse("pow ( 2 , 8 )").Should().Be("2 8 pow");
            Calculate("pow ( 2 , 8 )").Should().Be(256);
        }

        //sin ( max ( 2, 3 ) ÷ 3 × π )
        [Fact]
        public void Parse_ExpressionWithNestedFunctions_ReturnRpn()
        {
            ShuntingYard.Parse("sin ( max ( 2 , 3 ) / 3 * pi )").Should().Be("2 3 max 3 / pi * sin");
        }

        [Fact]
        public void Parse_MissingLeftParentheses_ThrowsException()
        {
            Action act = () => ShuntingYard.Parse("pow 2 , 8 )");
            act.Should().Throw<ArgumentException>();
        }

        // Helper methods 
        // to make the tests more readable
        private int Calculate(string expression)
        {
            return ReversePolishCalculator.Compute(ShuntingYard.Parse(expression));
        }
    }
}
