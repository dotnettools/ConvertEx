using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ConvertEx.Tests
{
    public class ExtendedTypeConverterTests
    {
        [Fact]
        public void Test_UriToString()
        {
            var url = "https://github.com/";
            TestConversion(new Uri(url), typeof(string), url);
        }

        [Fact]
        public void Test_StringToUri()
        {
            var url = "https://github.com/";
            TestConversion(url, typeof(Uri), new Uri(url));
        }

        private void TestConversion(object value, Type targetType, object expectedValue)
        {
            var result = DotNetTools.ConvertEx.ConvertEx.ChangeType(value, targetType);
            Assert.Equal(expectedValue, result);
            Assert.Equal(result, expectedValue);
        }
    }
}
