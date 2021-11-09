using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetTools.ConvertEx
{
    /// <summary>
    /// Default type converter
    /// </summary>
    public static class ConvertEx
    {
        /// <summary>
        /// Represents the default type converter. This value is read-only.
        /// </summary>
        public static readonly ITypeConverter DefaultConverter = null;

        /// <summary>
        /// Changes the type of <paramref name="value"/> to <paramref name="conversionType"/>.
        /// </summary>
        /// <exception cref="InvalidCastException">Thrown in case of conversion failure.</exception>
        public static object TryChangeType(object value, Type conversionType)
        {
        }

        /// <summary>
        /// Changes the type of <paramref name="value"/> to <paramref name="conversionType"/>.
        /// </summary>
        /// <exception cref="InvalidCastException">Thrown in case of conversion failure.</exception>
        public static object ChangeType(object value, Type conversionType)
        {
        }
    }
}