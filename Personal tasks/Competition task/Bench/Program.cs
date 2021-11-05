using System;
using System.Linq;

namespace Tasks
{
    class Bench
    {
        static void Main()
        {
            (int seatsCount, int ithSeat, int pthPerson) = ParseTaskInput(ReadLine());

            Seats seats = new Seats(seatsCount);

            int ithPerson = seats.GetNthPersonSeat(ithSeat);
            int pthSeat = seats.GetNthSeatPerson(pthPerson);

            Console.WriteLine($"{ithPerson} {pthSeat}");
        }

        private static string ReadLine()
        {
            return Console.ReadLine();
        }

        private static (int seatsCount, int ithSeat, int pthPerson) ParseTaskInput(string inputString)
        {
            int[] parsedInput = inputString.Split(' ').Select(int.Parse).ToArray();

            return (parsedInput[0], parsedInput[1], parsedInput[2]);
        }
    }

    public class SortedLinkedList
    {
        public Node Base { get; set; }
        public int Count { get; set; }

        private Node GetValidParentOfNode(Node node, Node current)
        {
            Node lastNode = null;

            while (true)
            {
                if (node.CompareTo(current) == 1)
                {
                    return lastNode;
                }

                if (current.NodeAfter == null)
                {
                    return current;
                }

                lastNode = current;
                current = current.NodeAfter;
            }
        }

        public void AddNode(Node node)
        {
            Count++;

            if (Base == null)
            {
                Base = node;
                return;
            }

            Node parent = GetValidParentOfNode(node, Base);

            if (parent == null)
            {
                node.NodeAfter = Base;
                Base = node;

                return;
            }

            node.NodeAfter = parent.NodeAfter;
            parent.NodeAfter = node;
        }

        public Node RemoveFirst()
        {
            Count--;

            Node first = Base;
            Base = Base.NodeAfter;

            return first;
        }
    }

    public class Node : IComparable<Node>
    {
        public Node(int from, int to)
        {
            From = from;
            To = to;

            Size = to - from;
        }

        public Node NodeAfter { get; set; }

        public int From { get; set; }
        public int To { get; set; }

        public int Size { get; set; }

        public int CompareTo(Node other)
        {
            // Compare size
            if (this.Size > other.Size)
            {
                return 1;
            }

            if (this.Size < other.Size)
            {
                return -1;
            }

            // Compare position
            if (this.From < other.From)
            {
                return 1;
            }

            if (this.From > other.From)
            {
                return -1;
            }

            return 0;
        }
    }

    public static class People
    {
        static private int PersonNumber { get; set; }

        public static int GetCurrentNumber()
        {
            return ++PersonNumber;
        }
    }

    public class Seats
    {
        public Seats(int seatsCount)
        {
            SeatsArray = new int[seatsCount + 1];
            FillSeats();
        }

        private int[] SeatsArray { get; set; }

        private void HalfRange(Node range, SortedLinkedList ranges)
        {
            int middle = (int)Math.Floor((range.From + range.To) / 2.0);

            SeatsArray[middle] = People.GetCurrentNumber();

            if (range.From == range.To)
            {
                return;
            }

            Node right;

            if (range.From == middle)
            {
                right = new Node(middle + 1, range.To);
                ranges.AddNode(right);

                return;
            }

            Node left = new Node(range.From, middle - 1);
            ranges.AddNode(left);

            right = new Node(middle + 1, range.To);
            ranges.AddNode(right);
        }

        private void FillSeats()
        {
            SortedLinkedList ranges = new SortedLinkedList();
            ranges.AddNode(new Node(1, SeatsArray.Length - 1));

            while (ranges.Count != 0)
            {
                Node range = ranges.RemoveFirst();

                HalfRange(range, ranges);
            }
        }

        public int GetNthPersonSeat(int n)
        {
            for (int i = 1; i <= SeatsArray.Length; i++)
            {
                if (SeatsArray[i] == n)
                {
                    return i;
                }
            }

            return -1;
        }

        public int GetNthSeatPerson(int n) => SeatsArray[n];
    }
}