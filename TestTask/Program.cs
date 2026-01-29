using FirstTask;
using SecondTask;

namespace Program
{
    public static class Program
    {
        public static async Task Main()
        {
            StringCompressor compressor = new StringCompressor();

            Console.WriteLine(compressor.Compress("aaabbbbbcdaa"));
            Console.WriteLine(compressor.Decompress(compressor.Compress("aaabbbbbcdaa")));

            Server server = new Server();
            var tasks = new List<Task>();

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Run(() =>
                    server.GetCount()
                ));
            }
            for (int i = 0; i < 10; i++)
            {
                int value = 1;
                tasks.Add(Task.Run(() => server.AddToCount(value)));
            }

            await Task.WhenAll(tasks.ToArray());

            Console.WriteLine($"Final count: {server.GetCount()}");
        }
    }
}

