using System;

namespace LongestValidParentheses
{
    class Program
    {
        static void Main()
        {
            string input = "()(())"; // Example input
            int result = LongestValidParentheses(input); // Receive result from main method for the task
            Console.WriteLine(result); // Print result
        }

        // Declaration of the needed constants
        internal const char openBracket = '(';
        internal const char closingBracket = ')';

        /// <summary>
        /// Finds the longest valid parentheses
        /// </summary>
        /// <param name="input">Input for the task</param>
        /// <returns>Count of the longest valid parentheses</returns>
        static int LongestValidParentheses(string input)
        {
            // Checks if input data is correct
            if (input is null || input.Length == 0)
            {
                return 0;
            }

            int longestCount = 0;
            int currentCount = 0;

            // Goes through all elements of the input to find the parentheses
            for (int i = 0; i < input.Length;)
            {
                if (input[i] == closingBracket)
                {
                    // Calls method for finding all next inner parentheses and returns their count
                    int length = FindParentheses(input, i + 1);

                    // Checks if after the current bracket doesn't come closing bracket
                    if (length > 0)
                    {
                        i += length;
                        currentCount += length;
                    }
                    else
                    {
                        CheckCurrentCount(ref currentCount, ref longestCount, ref i);
                    }
                }
                else
                {
                    CheckCurrentCount(ref currentCount, ref longestCount, ref i);
                }
            }

            if (currentCount > longestCount)
            {
                longestCount = currentCount;
            }

            return longestCount;
        }

        static void CheckCurrentCount(ref int currentCount, ref int longestCount, ref int index)
        {
            if (currentCount > longestCount)
            {
                longestCount = currentCount;
            }

            currentCount = 0;
            index++;
        }

        static int FindParentheses(string input, int currentIndex)
        {
            // Checks if the current values are correct
            if (currentIndex == input.Length)
            {
                // End recursion
                return 0;
            }
            else if (input[currentIndex] == openBracket)
            {
                return 2;
            }

            int length = 0;

            // Goes through all inner brackets
            for (int i = currentIndex; i < input.Length;)
            {
                if (input[i] == closingBracket)
                {
                    // Search for closing bracked of the current one
                    int resultLength = FindParentheses(input, i + 1);

                    if (resultLength == 0)
                    {
                        // End recursion
                        break;
                    }
                    else
                    {
                        length += resultLength;
                        i += resultLength;
                    }
                }
                else return length + 2;
            }

            // End recursion because the current parentheses aren't correct
            return 0;
        }
    }
}
