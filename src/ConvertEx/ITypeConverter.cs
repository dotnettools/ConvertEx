using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetTools.ConvertEx
{
    /// <summary>
    /// Converts values to different types.
    /// </summary>
    public interface ITypeConverter
    {
        /// <summary>
        /// Tries to convert <paramref name="value"/> to <paramref name="targetType"/>.
        /// </summary>
        /// <returns>Whether or not the conversion was successful.</returns>
        bool TryConvert(object value, Type targetType, IFormatProvider formatProvider, out object convertedValue);
    }
}
