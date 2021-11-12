using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetTools.ConvertEx.Converters
{
    /// <summary>
    /// Converts <see cref="Uri"/> to string and vice versa.
    /// </summary>
    public class UriConverter : ITypeConverter
    {
        public bool TryConvert(object value, Type targetType, IFormatProvider formatProvider, out object convertedValue)
        {
            if (targetType != typeof(Uri))
            {
                convertedValue = null;
                return false;
            }

            convertedValue = new Uri(value.ToString());
            return true;
        }
    }
}
