using System;
using DotNetTools.ConvertEx.Internal;

namespace DotNetTools.ConvertEx.Converters
{
    /// <summary>
    /// Converts numeric and string values to <see cref="TimeSpan"/> and vice versa.
    /// </summary>
    public class TimeSpanConverter : TypeConverterBase
    {
        public override bool TryConvert(object value, Type targetType, IFormatProvider formatProvider, out object convertedValue)
        {
            var valueType = value.GetType();
            if (targetType != typeof(TimeSpan) && valueType != typeof(TimeSpan))
            {
                convertedValue = null;
                return false;
            }

            if (targetType == typeof(TimeSpan))
            {
                // convert to TimeSpan
                if (valueType == typeof(string))
                {
                    convertedValue = TimeSpan.Parse((string)value);
                }
                else if (valueType.IsNumericType())
                {
                    var milliseconds = (double)Convert.ChangeType(value, TypeCode.Double);
                    convertedValue = TimeSpan.FromMilliseconds(milliseconds);
                }
                else
                {
                    convertedValue = null;
                    return false;
                }
            }
            else
            {
                // convert from TimeSpan
                var timeSpan = (TimeSpan)value;
                if (targetType == typeof(string))
                {
                    convertedValue = timeSpan.ToString();
                }
                else if (targetType.IsNumericType())
                {
                    convertedValue = Convert.ChangeType(timeSpan.TotalMilliseconds, targetType);
                }
                else
                {
                    convertedValue = null;
                    return false;
                }
            }

            return true;
        }
    }
}