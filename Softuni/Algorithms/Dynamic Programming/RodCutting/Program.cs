using System;
using System.Linq;
using System.Collections.Generic;

namespace RodCutting
{
    public class Program
    {
        static void Main()
        {
            var rawInputPrices = Console.ReadLine();
            int rodLength = int.Parse(Console.ReadLine());
            int[] prices = rawInputPrices.Split().Select(int.Parse).Where((price, len) => rodLength >= len).ToArray();

            Output result = BestPriceForRod(rodLength, prices);
            Console.WriteLine(result.BestPrice);
            Console.WriteLine(result.Path);
        }

        static readonly Dictionary<int, Output> outputs = new Dictionary<int, Output>() { { 0, new Output(0, "") } };

        static Output BestPriceForRod(int length, int[] prices)
        {
            if (outputs.ContainsKey(length))
            {
                return outputs[length];
            }

            Output output = new Output(0, "");

            for (int i = 1; i < prices.Length; i++)
            {
                if (length < i)
                {
                    break;
                }

                Output curr = BestPriceForRod(length - i, prices);
                int price = prices[i] + curr.BestPrice;

                if (price > output.BestPrice)
                {
                    output.BestPrice = price;
                    output.Path = i + " " + curr.Path;
                }
            }

            outputs.Add(length, output);
            return output;
        }
    }

    class Output
    {
        public Output(int bestPrice, string path)
        {
            BestPrice = bestPrice;
            Path = path;
        }

        public int BestPrice { get; set; }
        public string Path { get; set; }
    }
}