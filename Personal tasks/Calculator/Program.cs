using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Calculator
{
    class Program
    {
        static void Main()
        {
            string input = Console.ReadLine().Replace(" ", "");
            StringBuilder correctMathProblem = new StringBuilder();

            for (int i = 0; i < input.Length - 1; i++)
            {
                correctMathProblem.Append(input[i]);

                if (char.IsDigit(input[i]) && i + 1 != input.Length && !char.IsDigit(input[i + 1]))
                {
                    correctMathProblem.Append(' ');
                }
                else if (!char.IsDigit(input[i]))
                {
                    correctMathProblem.Append(' ');
                }
            }

            correctMathProblem.Append(input[input.Length - 1]);

            Console.Write("Your result is: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(Math.Round(double.Parse(SolveBrackets(correctMathProblem.ToString().Split(' ').ToList())), 2));
            Console.ForegroundColor = ConsoleColor.White;
        }

        static string SolveBrackets(List<string> mathProblem)
        {
            Stack<int> indexes = new Stack<int>();

            for (int i = 0; i < mathProblem.Count; i++)
            {
                if (mathProblem[i] == "(")
                {
                    indexes.Push(i);
                }
                else if (mathProblem[i] == ")")
                {
                    List<string> newMathProblem = new List<string>();

                    mathProblem.RemoveAt(indexes.Peek());
                    int lastIndex = i;

                    for (int i1 = indexes.Peek() + 1; i1 < lastIndex; i1++)
                    {
                        newMathProblem.Add(mathProblem[indexes.Peek()]);
                        mathProblem.RemoveAt(indexes.Peek());
                        i--;
                    }

                    mathProblem.RemoveAt(indexes.Peek());
                    i -= 2;

                    mathProblem.Insert(indexes.Pop(), SolveBrackets(newMathProblem));
                }
            }

            if (mathProblem.Contains("*") || mathProblem.Contains("/") || mathProblem.Contains("^"))
            {
                SolveProblemsWithHightImportance(mathProblem);
            }

            if (mathProblem.Contains("+") || mathProblem.Contains("-"))
            {
                SolveProblemsWithLowImportance(mathProblem);
            }

            return mathProblem[0];
        }

        static void SolveProblemsWithHightImportance(List<string> mathProblem)
        {
            double result = 0;
            double numGoingToBeRaised = 0;

            for (int i = 0; i < mathProblem.Count; i++)
            {
                switch (mathProblem[i])
                {
                    case "/":

                        if (result == 0)
                        {
                            throw new Exception("Cannot devide by zero");
                        }

                        result /= double.Parse(mathProblem[i + 1]);
                        mathProblem.RemoveAt(i);
                        mathProblem.RemoveAt(i);
                        mathProblem.Insert(i, result.ToString());
                        result = 0;
                        i--;

                        break;

                    case "*":

                        result *= double.Parse(mathProblem[i + 1]);
                        mathProblem.RemoveAt(i);
                        mathProblem.RemoveAt(i);
                        mathProblem.Insert(i, result.ToString());
                        result = 0;
                        i--;

                        break;

                    case "^":

                        numGoingToBeRaised = Math.Pow(numGoingToBeRaised, double.Parse(mathProblem[i + 1]));
                        mathProblem.RemoveAt(i);
                        mathProblem.RemoveAt(i);
                        mathProblem.Insert(i, numGoingToBeRaised.ToString());
                        numGoingToBeRaised = 0;
                        i--;

                        break;

                    default:

                        if (mathProblem[i] != "+" && mathProblem[i] != "-")
                        {
                            if (i + 1 != mathProblem.Count &&
                                mathProblem[i + 1] != "+" && mathProblem[i + 1] != "-")
                            {
                                if (mathProblem[i + 1] == "^")
                                {
                                    numGoingToBeRaised = double.Parse(mathProblem[i]);
                                }
                                else
                                {
                                    result = double.Parse(mathProblem[i]);
                                }

                                mathProblem.RemoveAt(i);
                                i--;
                            }
                        }

                        break;
                }
            }
        }

        static void SolveProblemsWithLowImportance(List<string> mathProblem)
        {
            double result = 0;

            while (mathProblem.Count > 0)
            {
                switch (mathProblem[0])
                {
                    case "-":

                        result -= double.Parse(mathProblem[1]);
                        mathProblem.RemoveAt(0);
                        mathProblem.RemoveAt(0);

                        break;

                    case "+":

                        result += double.Parse(mathProblem[1]);
                        mathProblem.RemoveAt(0);
                        mathProblem.RemoveAt(0);

                        break;

                    default:

                        result = double.Parse(mathProblem[0]);
                        mathProblem.RemoveAt(0);

                        break;
                }
            }

            mathProblem.Add(result.ToString());
        }
    }
}