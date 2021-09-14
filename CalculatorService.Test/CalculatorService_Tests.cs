using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CalculatorService.Test
{
    public class CalculatorService_Tests
    {
        [Theory]
        [InlineData("+")]
        [InlineData("-")]
        [InlineData("*")]
        [InlineData("/")]
        public void IsOperator_ReturnTrue(string symbol)
        {
            var calculatorService = new Service.CalculatorService();
            bool result = calculatorService.IsOperator(symbol);

            Assert.True(result, $"{symbol} should be valid operator");
        }

        [Theory]
        [InlineData("1")]
        [InlineData("11.1")]
        [InlineData(">")]
        [InlineData("(")]
        public void IsOperator_ReturnFalse(string symbol)
        {
            var calculatorService = new Service.CalculatorService();
            bool result = calculatorService.IsOperator(symbol);

            Assert.False(result, $"{symbol} should not be valid operator");
        }

        [Theory]
        [InlineData("(")]
        public void IsOpenBrackets_ReturnTrue(string symbol)
        {
            var calculatorService = new Service.CalculatorService();
            bool result = calculatorService.IsOpenBrackets(symbol);

            Assert.True(result, $"{symbol} should be valid open bracket.");
        }

        [Theory]
        [InlineData(")")]
        [InlineData("1")]
        [InlineData("+")]
        public void IsOpenBrackets_ReturnFalse(string symbol)
        {
            var calculatorService = new Service.CalculatorService();
            bool result = calculatorService.IsOpenBrackets(symbol);

            Assert.False(result, $"{symbol} should not be valid open bracket.");
        }

        [Theory]
        [InlineData(")")]
        public void IsClosedBrackets_ReturnTrue(string symbol)
        {
            var calculatorService = new Service.CalculatorService();
            bool result = calculatorService.IsClosedBrackets(symbol);

            Assert.True(result, $"{symbol} should be valid closed bracket.");
        }

        [Theory]
        [InlineData("(")]
        [InlineData("1")]
        [InlineData("+")]
        public void IsClosedBrackets_ReturnFalse(string symbol)
        {
            var calculatorService = new Service.CalculatorService();
            bool result = calculatorService.IsClosedBrackets(symbol);

            Assert.False(result, $"{symbol} should not be valid closed bracket.");
        }

        [Theory]
        [InlineData("+")]
        public void SetOperation_ReturnAdd(string symbol)
        {
            var calculatorService = new Service.CalculatorService();
            var result = calculatorService.SetOperation(symbol);

            Assert.True(result == Service.Operation.Add, $"{symbol} should return Operation.Add");
        }

        [Theory]
        [InlineData("-")]
        public void SetOperation_ReturnMinus(string symbol)
        {
            var calculatorService = new Service.CalculatorService();
            var result = calculatorService.SetOperation(symbol);

            Assert.True(result == Service.Operation.Minus, $"{symbol} should return Operation.Minus");
        }

        [Theory]
        [InlineData("*")]
        public void SetOperation_ReturnMultiply(string symbol)
        {
            var calculatorService = new Service.CalculatorService();
            var result = calculatorService.SetOperation(symbol);

            Assert.True(result == Service.Operation.Multiply, $"{symbol} should return Operation.Multiply");
        }

        [Theory]
        [InlineData("/")]
        public void SetOperation_ReturnDivide(string symbol)
        {
            var calculatorService = new Service.CalculatorService();
            var result = calculatorService.SetOperation(symbol);

            Assert.True(result == Service.Operation.Divide, $"{symbol} should return Operation.Divide");
        }

        [Theory]
        [InlineData("1 + 1", 2)]
        [InlineData("2 * 2", 4)]
        [InlineData("1 + 2 + 3", 6)]
        [InlineData("6 / 2", 3)]
        [InlineData("11 + 23", 34)]
        [InlineData("11.1 + 23", 34.1)]
        [InlineData("1 + 1 * 3", 4)]
        [InlineData("( 11.5 + 15.4 ) + 10.1", 37)]
        [InlineData("23 - ( 29.3 - 12.5 )", 6.2)]
        [InlineData("( 1 / 2 ) - 1 + 1", 0.5)]
        [InlineData("10 - ( 2 + 3 * ( 7 - 5 ) )", 2)]
        public void Calculate_ReturnTrue(string expression, double expected)
        {
            Service.ICalculatorService calculatorService = new Service.CalculatorService();
            var result = calculatorService.Calculate(expression);

            Assert.True(result == expected, $"{expression} should return {expected}");
        }
    }
}
