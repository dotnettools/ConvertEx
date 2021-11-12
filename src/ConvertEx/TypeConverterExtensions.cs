using System;
using System.Runtime.CompilerServices;

namespace DotNetTools.ConvertEx
{
    /// <summary>
    /// Defines extension methods for <see cref="ITypeConverter"/>.
    /// </summary>
    public static class TypeConverterExtensions
    {
        /// <summary>
        /// Converts <paramref name="value"/> to <paramref name="targetType"/>.
        /// </summary>
        /// <returns>The converted value.</returns>
        /// <exception cref="InvalidCastException">Thrown in case of conversion error.</exception>
#if USE_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static object Convert(this ITypeConverter typeConverter, object value, Type targetType, IFormatProvider formatProvider)
        {
            if (typeConverter.TryConvert(value, targetType, formatProvider, out var result))
                return result;
            throw new InvalidCastException($"Cannot convert from {value.GetType()} to {targetType}.");
        }

        /// <summary>
        /// Converts <paramref name="value"/> to <paramref name="targetType"/>.
        /// </summary>
        /// <returns>The converted value.</returns>
        /// <exception cref="InvalidCastException">Thrown in case of conversion error.</exception>
#if USE_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static object Convert(this ITypeConverter typeConverter, object value, Type targetType)
        {
            return Convert(typeConverter, value, targetType, null);
        }

        /// <summary>
        /// Clones a <see cref="TypeConverter"/>.
        /// </summary>
#if USE_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static TypeConverter Clone(this TypeConverter self)
        {
            return new(self);
        }

        /// <summary>
        /// Registers a digester by its type.
        /// </summary>
#if USE_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static TypeConverter AddDigester<T>(this TypeConverter self)
            where T : ITypeDigester
        {
            return self.AddDigester(typeof(T));
        }

        /// <summary>
        /// Registers a converter by its type.
        /// </summary>
#if USE_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static TypeConverter AddConverter<T>(this TypeConverter self)
            where T : ITypeConverter
        {
            return self.AddConverter(typeof(T));
        }
    }
}
