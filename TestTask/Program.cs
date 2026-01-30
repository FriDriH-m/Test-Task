using ThirdTask;

namespace Program
{
    
    public static class Program
    {
        public static async Task Main()
        {
            var processor = new LogProcessor();
            processor.ParseFile("OutputLogs.txt", "InputLogs.txt", "Problems.txt");

            //StringCompressor compressor = new StringCompressor();

            //Console.WriteLine(compressor.Compress("aaabbbbbcdaa"));
            //Console.WriteLine(compressor.Decompress(compressor.Compress("aaabbbbbcdaa")));

            //var tasks = new List<Task>();

            //for (int i = 0; i < 10; i++)
            //{
            //    tasks.Add(Task.Run(() =>
            //        Server.GetCount()
            //    ));
            //}
            //for (int i = 0; i < 10; i++)
            //{
            //    int value = 1;
            //    tasks.Add(Task.Run(() => Server.AddToCount(value)));
            //}

            //await Task.WhenAll(tasks.ToArray());

            //Console.WriteLine($"Final count: {Server.GetCount()}");
        }
    }
}