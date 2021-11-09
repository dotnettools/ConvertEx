using DotNetTools.ConvertEx;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ConvertEx.Tests
{
    public class TypeConverterTests
    {
        [Theory]
        [InlineData(85, typeof(int), 85)]
        [InlineData("test", typeof(string), "test")]
        public void Test_SameType(object value, Type targetType, object expectedValue)
        {
            TestConversion(value, targetType, expectedValue);
        }

        [Theory]
        [InlineData(85, typeof(double), 85d)]
        [InlineData(85, typeof(string), "85")]
        [InlineData("85", typeof(int), 85)]
        [InlineData("true", typeof(bool), true)]
        public void Test_SimpleConversions(object value, Type targetType, object expectedValue)
        {
            TestConversion(value, targetType, expectedValue);
        }

        private void TestConversion(object value, Type targetType, object expectedValue)
        {
            var result = DotNetTools.ConvertEx.ConvertEx.ChangeType(value, targetType);
            Assert.Equal(expectedValue, result);
        }
    }
}
