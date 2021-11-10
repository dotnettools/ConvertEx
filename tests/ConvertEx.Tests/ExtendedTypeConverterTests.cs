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
            const string url = "https://github.com/";
            TestConversion(new Uri(url), typeof(string), url);
        }

        [Fact]
        public void Test_StringToUri()
        {
            const string url = "https://github.com/";
            TestConversion(url, typeof(Uri), new Uri(url));
        }

        [Theory]
        [InlineData("00.00:01:00", 60000)]
        [InlineData((uint) 60000, 60000)]
        [InlineData(60000d, 60000)]
        public void Test_ConvertToTimeSpan(object fromValue, double expectedMilliseconds)
        {
            TestConversion(fromValue, typeof(TimeSpan), TimeSpan.FromMilliseconds(expectedMilliseconds));
        }

        [Theory]
        [InlineData(60000, typeof(string), "00:01:00")]
        [InlineData(60000, typeof(long), 60000L)]
        public void Test_ConvertFromTimeSpan(double milliseconds, Type targetType, object expectedValue)
        {
            var ts = TimeSpan.FromMilliseconds(milliseconds);
            TestConversion(ts, targetType, expectedValue);
        }

        [Theory]
        [InlineData("Val2", TestEnum.Val2)]
        [InlineData("val3", TestEnum.Val3)]
        [InlineData(1, TestEnum.Val1)]
        [InlineData(3d, TestEnum.Val3)]
        public void Test_ConvertToEnum(object value, TestEnum expectedValue)
        {
            TestConversion(value, typeof(TestEnum), expectedValue);
        }

        [Theory]
        [InlineData(TestEnum.Val2, typeof(string), "Val2")]
        [InlineData(TestEnum.Val1, typeof(int), 1)]
        [InlineData(TestEnum.Val3, typeof(double), 3d)]
        public void Test_ConvertFromEnum(TestEnum value, Type targetType, object expectedValue)
        {
            TestConversion(value, targetType, expectedValue);
        }

        private static void TestConversion(object value, Type targetType, object expectedValue)
        {
            var result = DotNetTools.ConvertEx.ConvertEx.ChangeType(value, targetType);
            Assert.Equal(expectedValue, result);
            Assert.Equal(result, expectedValue);
        }

        public enum TestEnum
        {
            Val1 = 1,
            Val2 = 2,
            Val3 = 3
        }
    }
}