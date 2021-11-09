using DotNetTools.ConvertEx.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetTools.ConvertEx
{
    /// <summary>
    /// Implements <see cref="ITypeConverter"/> to convert values using digesters and converters registered on it.
    /// </summary>
    public class TypeConverter : ITypeConverter
    {
        private readonly List<ITypeDigester> _digesters = new();
        private readonly List<ITypeConverter> _converters = new();

        /// <summary>
        /// Registers a digester.
        /// </summary>
        public TypeConverter AddDigester(ITypeDigester digester)
        {
            _digesters.Add(digester);
            return this;
        }

        /// <summary>
        /// Registers a type converter.
        /// </summary>
        public TypeConverter AddConverter(ITypeConverter converter)
        {
            _converters.Add(converter);
            return this;
        }

        /// <summary>
        /// Registers a digester by its type.
        /// </summary>
        public TypeConverter AddDigester(Type type)
        {
            var digester = (ITypeDigester)Activator.CreateInstance(type);
            return AddDigester(digester);
        }

        /// <summary>
        /// Registers a converter by its type.
        /// </summary>
        public TypeConverter AddConverter(Type type)
        {
            var converter = (ITypeConverter)Activator.CreateInstance(type);
            return AddConverter(converter);
        }

        public bool TryConvert(object value, Type targetType, out object convertedValue)
        {
            var defaultValue = targetType.GetDefaultValue();
            var visitedValues = new HashSet<object>();

            while (true)
            {
                var initialValue = value;
                var type = value.GetType();

                // test target
                if (type == targetType)
                {
                    convertedValue = value;
                    return true;
                }

                // test conversion cycle
                if (!visitedValues.Add(value))
                {
                    convertedValue = defaultValue;
                    return false;
                }

                // test NULL value
                if (value == null)
                {
                    convertedValue = defaultValue;
                    return true;
                }

                // try digest
                value = Digest(value, targetType);
                if (!initialValue.Equals(value))
                    continue;

                // try convert
                value = Convert(value, targetType);
            }
        }

        private object Convert(object value, Type targetType)
        {
            foreach (var converter in _converters)
            {
                if (converter.TryConvert(value, targetType, out var val))
                    return val;
            }
            return value;
        }

        private object Digest(object value, Type targetType)
        {
            bool hasChanged = true;
            while (hasChanged)
            {
                hasChanged = false;

                foreach (var digester in _digesters)
                {
                    var newValue = digester.Digest(value, targetType);
                    if (newValue != value)
                    {
                        value = newValue;
                        hasChanged = true;
                    }
                }

                if (value?.GetType() == targetType)
                    return value;
            }
            return value;
        }
    }
}
