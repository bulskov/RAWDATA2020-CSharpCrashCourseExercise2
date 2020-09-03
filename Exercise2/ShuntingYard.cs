using System;
using System.Collections.Generic;


namespace Exercise2
{
    public static class ShuntingYard
    {
        enum Associativity
        {
            Unknown,
            Left,
            Right
        }

        public static string Parse(string input)
        {
            var stack = new Stack<string>(); // for the operators not jet added to the output
            var queue = new Queue<string>(); // for the output

            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            // loop over the tokens
            foreach (var token in input.Split())
            {
                // is it a number, just add it to the output queue
                if (int.TryParse(token, out var number))
                {
                    queue.Enqueue(token);
                }
                // handle all other tokens
                else
                {
                    switch (token)
                    {
                        case "pi":
                            queue.Enqueue(token);
                            break;
                        // push sqrt and pow to the stack
                        case "sqrt":
                        case "pow":
                        case "sin":
                        case "max":
                            stack.Push(token);
                            break;
                        case ",": // ignore
                            break;
                        case "+":
                        case "-":
                        case "*":
                        case "/":
                        case "^":
                            // if we have operators on the stack
                            if (stack.Count > 0)
                            {
                                if (CheckLeftAssociativeAndPrecedence(token, stack.Peek()) || CheckRightAssociativeAndPrecedence(token, stack.Peek()))
                                {
                                    queue.Enqueue(stack.Pop());
                                }
                            }
                            stack.Push(token);
                            break;
                        case "(":
                            stack.Push(token);
                            break;
                        case ")":
                            while (stack.Peek() != "(")
                            {
                                queue.Enqueue(stack.Pop());
                                if (stack.Count == 0)
                                {
                                    throw new ArgumentException("Missing left parentheses");
                                }
                            }
                            stack.Pop();
                            break;

                    }
                }
            }

            foreach (var op in stack)
            {
                queue.Enqueue(op);
            }

            return string.Join(" ", queue);
        }

        public static bool CheckRightAssociativeAndPrecedence(string o1, string o2)
        {
            return GetAssociativity(o1) == Associativity.Right
                && Precedence(o1) < Precedence(o2);
        }

        public static bool CheckLeftAssociativeAndPrecedence(string o1, string o2)
        {
            return GetAssociativity(o1) == Associativity.Left
                && Precedence(o1) <= Precedence(o2);
        }

        // used values from https://en.wikipedia.org/wiki/Order_of_operations
        private static int Precedence(string oper)
        {
            switch (oper)
            {
                case "+": case "-": return 1;
                case "*": case "/": return 2;
                case "^": case "sqrt": case "pow": case "max": case "sin": return 3;
            }
            return 0;
        }

        // used info from https://en.wikipedia.org/wiki/Operator_associativity
        private static Associativity GetAssociativity(string op)
        {
            switch (op)
            {
                case "+": case "-": case "*": case "/": return Associativity.Left;
                case "^": return Associativity.Right;
                default: return Associativity.Unknown;
            }
        }
    }
}
