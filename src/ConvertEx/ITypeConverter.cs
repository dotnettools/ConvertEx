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
        bool TryConvert(object value, Type targetType, out object convertedValue);
    }

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
        public static object Convert(this ITypeConverter typeConverter, object value, Type targetType)
        {
            if (typeConverter.TryConvert(value, targetType, out var result))
                return result;
            throw new InvalidCastException($"Cannot convert from {value.GetType()} to {targetType}.");
        }

        /// <summary>
        /// Registers a digester by its type.
        /// </summary>
        public static TypeConverter AddDigester<T>(this TypeConverter self)
        {
            return self.AddDigester(typeof(T));
        }

        /// <summary>
        /// Registers a converter by its type.
        /// </summary>
        public static TypeConverter AddConverter<T>(this TypeConverter self)
        {
            return self.AddConverter(typeof(T));
        }
    }
}
