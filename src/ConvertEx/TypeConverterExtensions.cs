using System;

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
        public static object Convert(this ITypeConverter typeConverter, object value, Type targetType)
        {
            if (typeConverter.TryConvert(value, targetType, out var result))
                return result;
            throw new InvalidCastException($"Cannot convert from {value.GetType()} to {targetType}.");
        }

        /// <summary>
        /// Clones a <see cref="TypeConverter"/>.
        /// </summary>
        public static TypeConverter Clone(this TypeConverter self)
        {
            return new(self);
        }

        /// <summary>
        /// Registers a digester by its type.
        /// </summary>
        public static TypeConverter AddDigester<T>(this TypeConverter self)
            where T : ITypeDigester
        {
            return self.AddDigester(typeof(T));
        }

        /// <summary>
        /// Registers a converter by its type.
        /// </summary>
        public static TypeConverter AddConverter<T>(this TypeConverter self)
            where T : ITypeConverter
        {
            return self.AddConverter(typeof(T));
        }
    }
}
