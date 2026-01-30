namespace Tests
{
    public class StringCompressorTests
    {
        private readonly FirstTask.StringCompressor _compressor;
        public StringCompressorTests()
        {
            _compressor = new FirstTask.StringCompressor();
        }
        [Theory]
        [InlineData("aaabbcccdde", "a3b2c3d2e")]
        [InlineData("aaabbbbbcdaa", "a3b5cda2")]
        [InlineData("abc", "abc")]               
        [InlineData("aaaaa", "a5")]              
        [InlineData("a", "a")]                   
        [InlineData("", "")]                     
        [InlineData(null, null)]                 
        public void Compress_VariousInputs_ReturnsExpectedResult(string input, string expected)
        {
            string result = _compressor.Compress(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Compress_ShouldNotAddDigit_WhenCharacterIsSingle()
        {
            string input = "abcd";
            string expected = "abcd";

            string result = _compressor.Compress(input);

            Assert.Equal(expected, result);
        }
        [Fact]
        public void Decompress_WorksCorrectly()
        {
            string compressed = "a3b2w4a";
            string expected = "aaabbwwwwa";
            Assert.Equal(expected, _compressor.Decompress(compressed));
        }
        [Fact]
        public void Decompress_EmptyString_ReturnsEmptyString()
        {
            string compressed = "";
            string expected = "";
            Assert.Equal(expected, _compressor.Decompress(compressed));
        }
        [Theory]
        [InlineData("a3b2w4a", "aaabbwwwwa")]
        [InlineData("abc", "abc")]               
        [InlineData("", "")]                     
        public void Decompress_VariousInputs(string input, string expected)
        {
            Assert.Equal(expected, _compressor.Decompress(input));
        }    
    }
}