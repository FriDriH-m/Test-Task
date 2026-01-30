namespace Tests
{
    [Collection("ServerSequential")]
    public class ServerTest
    {
        [Fact]
        public void ConcurrentAccess_Test()
        {
            int initialCount = SecondTask.Server.GetCount();
            int tasksCount = 100;
            int incrementValue = 5;
            var tasks = new List<Task>();
            for (int i = 0; i < tasksCount; i++)
            {
                tasks.Add(Task.Run(() =>
                {
                    SecondTask.Server.AddToCount(incrementValue);
                }));
            }
            Task.WaitAll(tasks.ToArray());
            int expectedCount = initialCount + (tasksCount * incrementValue);
            int finalCount = SecondTask.Server.GetCount();
            Assert.Equal(expectedCount, finalCount);
        }
    }
}
