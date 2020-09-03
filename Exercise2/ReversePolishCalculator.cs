using System;
using System.Collections.Generic;

namespace Exercise2
{
    public static class ReversePolishCalculator
    {
        public static int Compute(string input)
        {
            int number = 0; // the default output is zero
            var stack = new Stack<int>();
            if (string.IsNullOrEmpty(input))
            {
                return number;
            }
            // loop over the tokens in the input
            foreach (var token in input.Split())
            {
                // if next token is a number add it to the stack
                if (int.TryParse(token, out number))
                {
                    stack.Push(number);
                }
                // otherwise it must be an operator, so we
                // need to compute the result
                switch (token)
                {
                    case "+":
                        // check that we have two numbers at the stack
                        CheckBinaryOperation(stack, token); 
                        // pop the two topmost numbers and push the addition back
                        stack.Push(stack.Pop() + stack.Pop());
                        break;
                    case "-":
                        // check that we have two numbers at the stack
                        CheckBinaryOperation(stack, token);
                        // here we need to take the order of operands into consideration
                        // for the expression 1 5 - 
                        // if we just used the stack order will give
                        // stack.Pop() - stack.Pop() = 5 - 1
                        number = stack.Pop(); // pop the topmost number
                        // and the do the operation
                        stack.Push(stack.Pop() - number);
                        break;
                    case "*":
                        // check that we have two numbers at the stack
                        // same as addition
                        CheckBinaryOperation(stack, token);
                        stack.Push(stack.Pop() * stack.Pop());
                        break;
                    case "/":
                        // check that we have two numbers at the stack
                        // same as subtraction - keep the right order
                        CheckBinaryOperation(stack, token);
                        number = stack.Pop();
                        stack.Push(stack.Pop() / number);
                        break;
                    case "^":
                    case "pow":
                        // check that we have two numbers at the stack
                        // same as subtraction - keep the right order
                        CheckBinaryOperation(stack, token);
                        number = stack.Pop();
                        stack.Push((int)Math.Pow(stack.Pop(), number));
                        break;
                    case "sqrt":
                        // check that we have one number at the stack
                        CheckUnaryOperation(stack, token);
                        stack.Push((int)Math.Sqrt(stack.Pop()));
                        break;
                }
            }
            if (stack.Count > 1)
            {
                throw new ArgumentException($"Invalid expression: {input}");
            }
            return stack.Pop();
        }


        private static void CheckBinaryOperation(Stack<int> stack, string op)
        {
            if (stack.Count < 2)
            {
                throw new ArgumentException($"Missing operands for binary operation '{op}'");
            }
        }

        private static void CheckUnaryOperation(Stack<int> stack, string op)
        {
            if (stack.Count < 1)
            {
                throw new ArgumentException($"Missing operands for unary operation '{op}'");
            }
        }
    }
}
