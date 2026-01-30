using ThirdTask;

namespace Tests
{
    public class LogProcessorTest
    {
        [Fact]
        public void ParseFile_WorksCorrectly()
        {
            var processor = new LogProcessor();
            string inputFilePath = "TestInputLogs.txt";
            string outputFilePath = "TestOutputLogs.txt";
            string problemsFilePath = "TestProblems.txt";

            File.WriteAllLines(inputFilePath, new[]
            {
                "10.03.2025 15:14:49.523 INFORMATION Application started",
                "2025-03-10 15:14:51.5882| INFO|11|MobileComputer.GetDeviceId| Connection established",
                "15.03.2025 12:00:00.000 WARNING Low disk space",
                "2025-03-16 09:15:22.1234| ERROR| 5 | AuthService | User not found",
                "Just some random text",
                "2025-03-22|INFO|Too few fields"
            });

            try
            {
                processor.ParseFile(outputFilePath, inputFilePath, problemsFilePath);

                var outputLines = File.ReadAllLines(outputFilePath);
                var problemLines = File.ReadAllLines(problemsFilePath);

                // Проверяем количество: 4 валидных строки, 2 проблемные
                Assert.Equal(4, outputLines.Length);
                Assert.Equal(2, problemLines.Length);

                // Строка 1: INFORMATION превратился в INFO, добавился DEFAULT
                Assert.Contains("10-03-2025\t15:14:49.523\tINFO\tDEFAULT\tApplication started", outputLines);

                // Строка 2: Формат 2 сохранился, дата привелась к dd-MM-yyyy
                Assert.Contains("10-03-2025\t15:14:51.5882\tINFO\tMobileComputer.GetDeviceId\tConnection established", outputLines);

                // Строка 3: WARNING превратился в WARN
                Assert.Contains("15-03-2025\t12:00:00.000\tWARN\tDEFAULT\tLow disk space", outputLines);

                // Строка 4: Пробелы убрались (Trim), ERROR остался
                Assert.Contains("16-03-2025\t09:15:22.1234\tERROR\tAuthService\tUser not found", outputLines);

                // Проверяем содержимое проблемных строк
                Assert.Contains("Just some random text", problemLines);
                Assert.Contains("2025-03-22|INFO|Too few fields", problemLines);
            }
            finally
            {
                if (File.Exists(inputFilePath)) File.Delete(inputFilePath);
                if (File.Exists(outputFilePath)) File.Delete(outputFilePath);
                if (File.Exists(problemsFilePath)) File.Delete(problemsFilePath);
            }
        }
    }
}