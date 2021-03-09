using System;
using System.Linq;
using System.Collections.Generic;

namespace TowerOfHanoi
{
    class Program
    {
        static Stack<int> source;
        static Stack<int> spare;
        static Stack<int> destination;

        static int neededSteps;

        static void Main()
        {
            int disks = int.Parse(Console.ReadLine());

            SetNeededStepsCount(disks);

            source = new Stack<int>(Enumerable.Range(1, disks).Reverse());
            spare = new Stack<int>();
            destination = new Stack<int>();

            Console.WriteLine(Report());

            FindSolution();
            reports.Reverse();
            Console.Write(string.Join(Environment.NewLine, reports));
        }

        static void SetNeededStepsCount(int disks)
        {
            int steps = 1;

            for (int i = 1; i < disks; i++)
            {
                steps *= 2;
                steps++;
            }

            neededSteps = steps;
        }

        static bool haveNotFoundSolution = true;
        static readonly List<string> reports = new List<string>();

        static void FindSolution(int step = 0)
        {
            if (step == neededSteps && source.Count == 0 && spare.Count == 0)
            {
                haveNotFoundSolution = false;
                return;
            }
            else if (step > neededSteps) return;

            step++;

            if (haveNotFoundSolution) MoveDisk(source, spare, step);
            if (haveNotFoundSolution) MoveDisk(source, destination, step);

            if (haveNotFoundSolution) MoveDisk(spare, source, step);
            if (haveNotFoundSolution) MoveDisk(spare, destination, step);

            if (haveNotFoundSolution) MoveDisk(destination, source, step);
            if (haveNotFoundSolution) MoveDisk(destination, spare, step);
        }

        static void MoveDisk(Stack<int> from, Stack<int> to, int step)
        {
            if (from.Count > 0 && (to.Count == 0 || from.Peek() < to.Peek()))
            {
                int disk = from.Pop();
                to.Push(disk);

                FindSolution(step);

                if (!haveNotFoundSolution)
                {
                    reports.Add(Report($"Step #{step}: Moved disk {disk}"));
                }

                disk = to.Pop();
                from.Push(disk);
            }
        }

        static string Report(string stepReport = "")
        {
            if (stepReport != "") stepReport += Environment.NewLine;

            string report = stepReport;
            report += $"Source: {string.Join(", ", source.Reverse())}{Environment.NewLine}";
            report += $"Destination: {string.Join(", ", destination.Reverse())}{Environment.NewLine}";
            report += $"Spare: {string.Join(", ", spare.Reverse())}{Environment.NewLine}";

            return report;
        }
    }
}