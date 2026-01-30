using ThirdTask;

namespace Program
{
    
    public static class Program
    {
        public static async Task Main()
        {
            var processor = new LogProcessor();
            processor.ParseFile("OutputLogs.txt", "InputLogs.txt", "Problems.txt");
        }
    }
}