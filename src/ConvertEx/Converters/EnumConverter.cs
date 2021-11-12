using System;
using DotNetTools.ConvertEx.Internal;

namespace DotNetTools.ConvertEx.Converters
{
    /// <summary>
    /// Converts values to enum.
    /// </summary>
    public class EnumConverter : TypeConverterBase
    {
        /// <summary>
        /// Gets or sets whether or not to ignore enum string case.
        /// </summary>
        public bool IgnoreCase { get; set; } = true;

        public override bool TryConvert(object value, Type targetType, IFormatProvider formatProvider, out object convertedValue)
        {
            if (targetType.IsEnum)
            {
                var valueType = value.GetType();
                if (valueType == typeof(string))
                {
                    convertedValue = Enum.Parse(targetType, value.ToString(), IgnoreCase);
                    return true;
                }
                else if (valueType.IsNumericType())
                {
                    var integer = Convert.ChangeType(value, typeof(int));
                    convertedValue = Enum.ToObject(targetType, integer);
                    return true;
                }
            }

            convertedValue = null;
            return false;
        }
    }
}