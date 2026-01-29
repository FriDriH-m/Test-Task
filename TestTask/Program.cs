using FirstTask;

namespace Program
{
    public static class Program
    {
        public static void Main()
        {
            StringCompressor firstTask = new StringCompressor();

            Console.WriteLine(firstTask.Compress("aaabbbbbcdaa"));
            Console.WriteLine(firstTask.Decompress(firstTask.Compress("aaabbbbbcdaa")));
        }
    }
}

