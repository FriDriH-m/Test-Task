using System.Globalization;
using System.Text.RegularExpressions;

namespace ThirdTask
{
    public class LogProcessor
    {
        private readonly List<LogParser> _parsers;

        public LogProcessor()
        {
            _parsers = new List<LogParser>
            {
                new Format1Parser(),
                new Format2Parser()
            };
        }
        public void ParseFile(string outputPath, string inputPath, string problemsPath)
        {
            using StreamReader reader = new StreamReader(inputPath);
            using StreamWriter outputWriter = new StreamWriter(outputPath);
            using StreamWriter problemsWriter = new StreamWriter(problemsPath);

            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                LogEntry? entry = null;
                bool parsed = false;
                foreach (var parser in _parsers)
                {
                    if (parser.TryParse(line, out entry))
                    {
                        parsed = true;
                        break;
                    }
                }
                if (parsed && entry.HasValue)
                {
                    outputWriter.WriteLine(entry.Value.ToString());
                }
                else
                {
                    problemsWriter.WriteLine(line);
                }
            }
        }
    }
    public abstract class LogParser 
    { 
        public abstract bool TryParse(string line, out LogEntry? entry);
        protected string NormalizeLogLevel(string level)
        {
            return level.ToUpper() switch
            {
                "INFORMATION" => "INFO",
                "INFO" => "INFO",
                "WARNING" => "WARN",
                "WARN" => "WARN",
                "ERROR" => "ERROR",
                "DEBUG" => "DEBUG",
                _ => "INFO"
            };
        }
    }
    public struct LogEntry
    {
        public string Date { get; set; }
        public string Time { get; set; }
        public string Level { get; set; }
        public string Method { get; set; }
        public string Message { get; set; }

        public override string ToString() => $"{Date}\t{Time}\t{Level}\t{Method}\t{Message}";
    }

    public class Format1Parser : LogParser
    {
        // Регулярка захватывает: 
        // 1. Дату (dd.MM.yyyy)
        // 2. Время (чч:мм:сс.мс)
        // 3. Уровень (одно слово)
        // 4. Сообщение (всё остальное до конца строки)
        private static readonly Regex _logRegex = new Regex(
            @"^(\d{2}\.\d{2}\.\d{4})\s+(\d{2}:\d{2}:\d{2}\.\d{3})\s+(\w+)\s+(.*)$",
            RegexOptions.Compiled);

        public override bool TryParse(string line, out LogEntry? entry)
        {
            var match = _logRegex.Match(line);

            if (!match.Success)
            {
                entry = null;
                return false;
            }

            try
            {                
                string rawDate = match.Groups[1].Value;
                DateOnly dateObj = DateOnly.ParseExact(rawDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);

                string rawLevel = match.Groups[3].Value;

                entry = new LogEntry
                {
                    Date = dateObj.ToString("dd-MM-yyyy"), 
                    Time = match.Groups[2].Value,          
                    Level = NormalizeLogLevel(rawLevel),   
                    Method = "DEFAULT",                    
                    Message = match.Groups[4].Value        
                };

                return true;
            }
            catch
            {
                entry = null;
                return false;
            }
        }       
    }
    public class Format2Parser : LogParser
    {
        public override bool TryParse(string rawLine, out LogEntry? entry)
        {
            entry = null;
            if (!rawLine.Contains('|')) return false;

            try
            {
                var parts = rawLine.Split('|');
                if (parts.Length < 5) return false;

                var dateTime = parts[0].Trim().Split(' ');

                entry = new LogEntry
                {
                    Date = DateOnly.Parse(dateTime[0]).ToString("dd-MM-yyyy"),
                    Time = dateTime[1],
                    Level = NormalizeLogLevel(parts[1].Trim()),
                    Method = parts[3].Trim(), 
                    Message = parts[4].Trim()
                };
                return true;
            }
            catch { return false; }
        }
    }
}
