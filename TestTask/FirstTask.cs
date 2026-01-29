

using System.Text;

namespace FirstTask
{
    internal class StringCompressor
    {
        public string Compress(string str)
        {
            if (string.IsNullOrEmpty(str)) return str;

            StringBuilder result = new StringBuilder();
            int index = 0;

            while (index < str.Length)
            {
                char currentChar = str[index];
                int count = 1;

                while (index + count < str.Length && str[index + count] == currentChar)
                {
                    count++;
                }

                result.Append(currentChar);

                if (count > 1)
                {
                    result.Append(count);
                }

                index += count;
            }

            return result.ToString();
        }
        public string Decompress(string str)
        {
            if (string.IsNullOrEmpty(str)) return str;

            StringBuilder result = new StringBuilder();
            int index = 0;

            while (index < str.Length)
            {
                char currentChar = str[index];
                index++;

                int count = 0;
                while (index < str.Length && char.IsDigit(str[index]))
                {
                    count = count * 10 + (str[index] - '0'); // можно использовать int.Parse(), это понятнее, но менее эффективно
                    index++;
                }

                if (count == 0)
                    count = 1;

                result.Append(currentChar, count);
            }

            return result.ToString();
        }
    }
}
