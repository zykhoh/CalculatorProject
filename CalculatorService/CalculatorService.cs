using Modal;
using System;
using System.Linq;

namespace Service
{
    public class CalculatorService : ICalculatorService
    {
        private static string[] operators = { "+", "-", "*", "/" };
        private int numberOfOpenBrackets = 0;
        private int numberOfClosedBrackets = 0;

        public double Calculate(string sum)
        {
            double result = CalculateRecursively(sum);

            if (numberOfClosedBrackets == numberOfOpenBrackets)
                return result;

            throw new Exception("Invalid Expression");
        }

        public double CalculateRecursively(string partial)
        {
            string[] inputs = partial.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            MathematicalExpression expression = new MathematicalExpression();
            bool OpenBracketFlag = false;

            if (partial.IndexOf("-") == 0)
                partial = partial.Substring(1);

            for (int i = 0; i < inputs.Length; i++)
            {
                //  Exit logic
                if (expression.leftArgument.HasValue && expression.operation == Operation.Add)
                {
                    expression.rightArgument = CalculateRecursively(partial.Substring(partial.IndexOf(GetOperatorBy(expression.operation)) + 1));
                    return expression.Sum();
                }
                else if (expression.leftArgument.HasValue && expression.operation == Operation.Minus)
                {
                    partial = partial.Substring(partial.IndexOf(GetOperatorBy(expression.operation)) + 2);

                    if (partial.IndexOf("(") == 0)
                    {
                        expression.rightArgument = CalculateRecursively(partial);
                        return expression.Minus();
                    }

                    expression.rightArgument = CalculateRecursively("-" + partial);
                    return expression.Sum();
                }
                else if (expression.leftArgument.HasValue && expression.rightArgument.HasValue && (expression.operation == Operation.Multiply || expression.operation == Operation.Divide))
                {
                    //  Straight Calculate Expression if [left * right] Or [left / right]
                    expression.leftArgument = expression.GetResult();
                    partial = partial.Substring(partial.IndexOf(expression.rightArgument.ToString()) + 1);
                    expression.rightArgument = null;
                    expression.operation = Operation.None;
                    i = -1;
                    inputs = partial.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    continue;
                }

                //  Assignment
                double argument;

                if (double.TryParse(inputs[i], out argument))
                {
                    if (!expression.leftArgument.HasValue)
                        expression.leftArgument = argument;
                    else if (!expression.rightArgument.HasValue)
                        expression.rightArgument = argument;
                }
                else if (IsOperator(inputs[i]))
                    expression.operation = SetOperation(inputs[i]);
                else if (IsOpenBrackets(inputs[i]))
                {
                    numberOfOpenBrackets++;
                    OpenBracketFlag = true;
                    break;
                }
                else if (IsClosedBrackets(inputs[i]))
                    numberOfClosedBrackets++;
            }

            if (OpenBracketFlag)
            {
                partial = partial.Substring(partial.IndexOf("(") + 1);
                if (expression.leftArgument.HasValue && expression.operation != Operation.None)
                {
                    expression.rightArgument = CalculateRecursively(partial);
                    return expression.GetResult();
                }

                if (expression.leftArgument.HasValue && expression.operation == Operation.None)
                {
                    expression.rightArgument = CalculateRecursively(partial);
                    return expression.Multiply();
                }

                return CalculateRecursively(partial);
            }

            if (expression.leftArgument.HasValue && expression.rightArgument.HasValue && (expression.operation == Operation.Multiply || expression.operation == Operation.Divide))
            {
                return expression.GetResult();
            }

            return (double) expression.leftArgument;
        }

        public bool IsOperator(string input)
        {
            if (operators.Contains(input))
                return true;

            return false;
        }

        public bool IsOpenBrackets(string input)
        {
            if (input == "(")
                return true;

            return false;
        }

        public bool IsClosedBrackets(string input)
        {
            if (input == ")")
                return true;

            return false;
        }

        public Operation SetOperation(string input)
        {
            switch (input)
            {
                case "+":
                    return Operation.Add;
                case "-":
                    return Operation.Minus;
                case "*":
                    return Operation.Multiply;
                case "/":
                    return Operation.Divide;
                default:
                    throw new Exception($"Invalid operator: {input}");
            }
        }

        public string GetOperatorBy(Operation operation)
        {
            switch (operation)
            {
                case Operation.Add:
                    return "+";
                case Operation.Minus:
                    return "-";
                case Operation.Multiply:
                    return "*";
                case Operation.Divide:
                    return "/";
                default:
                    return "";
            }
        }
    }

    public enum Operation
    {
        None,
        Add,
        Minus,
        Multiply,
        Divide
    }
}
