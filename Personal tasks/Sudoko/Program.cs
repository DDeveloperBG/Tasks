namespace Sudoko
{
    using System.Linq;
    using System.Text;

    class Program
    {
        static void Main()
        {
            int seededNumbersCount = int.Parse(Console.ReadLine());

            Field field = new Field(3, seededNumbersCount);
            Console.WriteLine("Start:");
            Console.WriteLine(field);
            field.Solve();
            Console.WriteLine("Solution:");
            Console.WriteLine(field);
        }
    }

    public class Field
    {
        private readonly int[] allowedNums = Enumerable.Range(1, 9).ToArray();
        private readonly int dimensionSize;

        public Field(int dimensionSize, int baseElementsCount)
        {
            this.dimensionSize = dimensionSize;

            this.Horizontal = new List<HashSet<int>>();
            this.Vertical = new List<HashSet<int>>();
            for (int i = 0; i < dimensionSize * dimensionSize; i++)
            {
                this.Horizontal.Add(new HashSet<int>());
                this.Vertical.Add(new HashSet<int>());
            }

            this.Groups = new Group[dimensionSize, dimensionSize];
            for (int x = 0; x < dimensionSize; x++)
            {
                for (int y = 0; y < dimensionSize; y++)
                {
                    this.Groups[y, x] = new Group(
                        x, y, 
                        dimensionSize, 
                        this.Horizontal, this.Vertical);
                }
            }

            this.Seed(baseElementsCount);
        }

        public Group[,] Groups { get; set; }

        public List<HashSet<int>> Horizontal { get; set; }

        public List<HashSet<int>> Vertical { get; set; }

        public bool CheckIsSolved()
        {
            foreach (var group in this.Groups)
            {
                if (!group.IsFilled())
                {
                    return false;
                }
            }

            return true;
        }

        public void Seed(int elementsCount)
        {
            var rnd = new Random();

            for (int i = 0; i < elementsCount; i++)
            {               
                bool wasAdded;
                do
                {
                    int groupX = rnd.Next(0, this.dimensionSize);
                    int groupY = rnd.Next(0, this.dimensionSize);

                    int x = rnd.Next(0, this.dimensionSize);
                    int y = rnd.Next(0, this.dimensionSize);

                    int number = rnd.Next(1, this.allowedNums.Length);

                    wasAdded = this.Groups[groupY, groupX].Add(x, y, number);

                } while (!wasAdded);
            }
        }

        public bool Solve(int groupRow = 0, int groupCol = 0, int y = 0, int x = 0)
        {
            if (x == this.dimensionSize)
            {
                x = 0;
                y++;
            }

            if (y == this.dimensionSize)
            {
                y = 0;
                groupCol++;
            }

            if (groupCol == this.dimensionSize)
            {
                groupCol = 0;
                groupRow++;
            }

            if (groupRow == this.dimensionSize)
            {
                return this.CheckIsSolved();
            }

            var group = this.Groups[groupRow, groupCol];
            if (group.Field[y, x] != default)
            {
                return Solve(groupRow, groupCol, y, x + 1);
            }

            var nums = this.allowedNums.Except(group.GroupNumsUsed);
            foreach (var number in nums)
            {
                if (group.Add(x, y, number))
                {
                    if (Solve(groupRow, groupCol, y, x + 1))
                    {
                        return true;
                    }

                    group.Remove(x, y);
                }
            }

            group.Remove(x, y);

            return false;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            for (int row = 0; row < this.dimensionSize; row++)
            {
                for (int groupRow = 0; groupRow < this.dimensionSize; groupRow++)
                {
                    for (int column = 0; column < this.dimensionSize; column++)
                    {
                        result.Append(" ");

                        for (int groupCol = 0; groupCol < this.dimensionSize; groupCol++)
                        {
                            result.Append(Groups[row, column].Field[groupRow, groupCol] + " ");
                        }

                        if (column + 1 != this.dimensionSize)
                        {
                            result.Append('~');
                        }
                    }

                    if (groupRow + 1 != this.dimensionSize)
                    {
                        result.AppendLine();
                        for (int i = 0; i < this.dimensionSize - 1; i++)
                        {
                            result.Append(new string(' ', this.dimensionSize * 2));
                            result.Append(" ~");
                        }
                        result.AppendLine();
                    }
                    else
                    {
                        result.AppendLine();
                        result.AppendLine(new string('~', (int)Math.Pow(this.dimensionSize, 3) - 5));
                    }
                }
            }

            return result.ToString();
        }
    }

    public class Group
    {
        private readonly int dimensionSize;

        private readonly int groupX;
        private readonly int groupY;

        private readonly List<HashSet<int>> Horizontal;
        private readonly List<HashSet<int>> Vertical;

        public Group(int groupX, int groupY, int dimensionSize, List<HashSet<int>> horizontal, List<HashSet<int>> vertical)
        {
            this.Horizontal = horizontal;
            this.Vertical = vertical;
            this.dimensionSize = dimensionSize;
            this.groupX = groupX;
            this.groupY = groupY;

            this.Field = new int[dimensionSize, dimensionSize];
            this.GroupNumsUsed = new HashSet<int>();
        }

        public int[,] Field { get; set; }

        public HashSet<int> GroupNumsUsed { get; init; }

        public bool IsFilled()
        {
            return this.GroupNumsUsed.Count == 9;
        }

        public bool Add(int x, int y, int numberToAdd)
        {
            var horizontal = this.GetHorizontal(x);
            var vertical = this.GetVertical(y);

            if (this.Field[y, x] != default
                || horizontal.Contains(numberToAdd)
                || vertical.Contains(numberToAdd)
                || this.GroupNumsUsed.Contains(numberToAdd))
            {
                return false;
            }

            this.Field[y, x] = numberToAdd;
            horizontal.Add(numberToAdd);
            vertical.Add(numberToAdd);
            this.GroupNumsUsed.Add(numberToAdd);

            return true;
        }

        public HashSet<int> GetHorizontal(int x)
        {
            int row = this.groupX * this.dimensionSize + x;
            return this.Horizontal[row];
        }

        public HashSet<int> GetVertical(int y)
        {
            int col = this.groupY * this.dimensionSize + y;
            return this.Vertical[col];
        }

        public void Remove(int x, int y)
        {
            var num = this.Field[y, x];
            this.Field[y, x] = default;

            this.GetHorizontal(x).Remove(num);
            this.GetVertical(y).Remove(num);
            this.GroupNumsUsed.Remove(num);
        }
    }
}