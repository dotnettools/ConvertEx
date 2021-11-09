using DotNetTools.ConvertEx.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        public static readonly ITypeConverter DefaultConverter = Defaults.CreateDefaultConverter();

        /// <summary>
        /// Changes the type of <paramref name="value"/> to <paramref name="conversionType"/>.
        /// </summary>
        /// <exception cref="InvalidCastException">Thrown in case of conversion failure.</exception>
#if USE_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool TryChangeType(object value, Type conversionType, out object convertedValue)
        {
            return DefaultConverter.TryConvert(value, conversionType, out convertedValue);
        }

        /// <summary>
        /// Changes the type of <paramref name="value"/> to <paramref name="conversionType"/>.
        /// </summary>
        /// <exception cref="InvalidCastException">Thrown in case of conversion failure.</exception>
#if USE_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static object ChangeType(object value, Type conversionType)
        {
            return DefaultConverter.Convert(value, conversionType);
        }
    }
}