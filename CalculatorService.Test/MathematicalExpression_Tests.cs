using Modal;
using Service;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CalculatorService.Test
{
    public class MathematicalExpression_Tests
    {
        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(1.1, 2.3, 3.4)]
        [InlineData(109, 14, 123)]
        [InlineData(1, 2.2, 3.2)]
        public void Sum_ReturnTrue(double left, double right, double expected)
        {
            var expression = new MathematicalExpression(left, right, Operation.None);
            var result = expression.Sum();

            Assert.True(result == expected, $"result, {result} should be [{left} + {right} = {expected}]");
        }

        [Theory]
        [InlineData(1, 2, -1)]
        [InlineData(1.1, 2.3, -1.2)]
        [InlineData(109, 14, 95)]
        [InlineData(1, 2.2, -1.2)]
        public void Minus_ReturnTrue(double left, double right, double expected)
        {
            var expression = new MathematicalExpression(left, right, Operation.None);
            var result = expression.Minus();

            Assert.True(result == expected, $"result, {result} should be [{left} - {right} = {expected}]");
        }

        [Theory]
        [InlineData(1, 2, 2)]
        [InlineData(1.1, 2.3, 2.53)]
        [InlineData(109, 14, 1526)]
        [InlineData(1, 2.2, 2.2)]
        public void Multiply_ReturnTrue(double left, double right, double expected)
        {
            var expression = new MathematicalExpression(left, right, Operation.None);
            var result = expression.Multiply();

            Assert.True(result == expected, $"result, {result} should be [{left} * {right} = {expected}]");
        }

        [Theory]
        [InlineData(1, 2, 0.5)]
        [InlineData(1.1, 2, 0.55)]
        [InlineData(109, 12, 9.0833333333333333333)]
        [InlineData(1, 2.2, 0.45454545454545455454)]
        public void Divide_ReturnTrue(double left, double right, decimal expected)
        {
            var expression = new MathematicalExpression(left, right, Operation.None);
            var result = expression.Divide();

            Assert.True((decimal)result == expected, $"result, {result} should be [{left} / {right} = {expected}]");
        }

        [Theory]
        [InlineData(1, 2, Operation.Add, 3)]
        [InlineData(1.1, 2, Operation.Minus, -0.9)]
        [InlineData(109, 12, Operation.Multiply, 1308)]
        [InlineData(1, 2.2, Operation.Divide, 0.4545454545454545454)]
        public void GetResult_ReturnTrue(double left, double right, Operation operation, decimal expected)
        {
            var expression = new MathematicalExpression(left, right, operation);
            var result = expression.GetResult();

            Assert.True((decimal)result == expected, $"result, {result} should be {expected}");
        }
    }
}
