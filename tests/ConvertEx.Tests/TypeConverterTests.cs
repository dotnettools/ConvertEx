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
        public void Test_SimpleConversion(object value, Type targetType, object expectedValue)
        {
            TestConversion(value, targetType, expectedValue);
        }

        [Fact]
        public void Test_ValueToNullable()
        {
            TestConversion(100, typeof(int?), (int?)100);
        }

        [Fact]
        public void Test_NullableToValue()
        {
            TestConversion((int?)100, typeof(int), 100);
        }

        [Fact]
        public void Test_NullableToNullable()
        {
            double? val = 100;
            int? expectedVal = 100;
            TestConversion(val, typeof(int?), expectedVal);
        }

        private void TestConversion(object value, Type targetType, object expectedValue)
        {
            var result = DotNetTools.ConvertEx.ConvertEx.ChangeType(value, targetType);
            Assert.Equal(expectedValue, result);
        }
    }
}
