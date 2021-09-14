using Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modal
{
    public class MathematicalExpression
    {
        public MathematicalExpression() { }

        public MathematicalExpression(double left, double right, Operation operation)
        {
            this.leftArgument = left;
            this.rightArgument = right;
            this.operation = operation;
        }

        public double? leftArgument { get; set; }

        public double? rightArgument { get; set; }

        public Operation operation { get; set; }

        public double Sum()
        {
            return (double)(decimal)(leftArgument + rightArgument);
        }

        public double Minus()
        {
            return (double) (decimal) (leftArgument - rightArgument);
        }

        public double Multiply()
        {
            return (double)(decimal)(leftArgument * rightArgument);
        }

        public double Divide()
        {
            return (double)(decimal)(leftArgument / rightArgument);
        }

        public double GetResult()
        {
            switch (operation)
            {
                case Operation.Add:
                    return Sum();
                case Operation.Minus:
                    return Minus();
                case Operation.Multiply:
                    return Multiply();
                case Operation.Divide:
                    return Divide();
                default:
                    throw new Exception($"No operation detected!");
            }
        }
    }
}
