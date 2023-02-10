class Program
{
    static void Main()
    {
        string text = "123456";

        int count = Permutate(text);
        Console.WriteLine(count);
    }

    static int Permutate(string text, int at = 0)
    {
        if (at + 1 == text.Length)
        {
            Console.WriteLine(text);
            return 1;
        }

        int count = 0;
        for (int withInd = at; withInd < text.Length; withInd++)
        {
            string swapedText = Swap(text, at, withInd);
            count += Permutate(swapedText, at + 1);
        }

        return count;
    }

    static string Swap(string text, int at, int withInd)
    {
        char[] textArr = text.ToCharArray();

        var swapedElement = textArr[at];
        textArr[at] = textArr[withInd];
        textArr[withInd] = swapedElement;

        return new string(textArr);
    }
}