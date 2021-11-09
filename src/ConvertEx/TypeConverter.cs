using DotNetTools.ConvertEx.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetTools.ConvertEx
{
    /// <summary>
    /// Implements <see cref="ITypeConverter"/> to convert values using digesters and converters that are registered on it.
    /// </summary>
    public class TypeConverter : ITypeConverter
    {
        private readonly List<ITypeDigester> _digesters = new();
        private readonly List<ITypeConverter> _converters = new();

        public TypeConverter() { }

        public TypeConverter(TypeConverter converter)
        {
            _digesters.AddRange(converter._digesters);
            _converters.AddRange(converter._converters);
        }

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

        //public bool TryConvert(object value, Type targetType, out object convertedValue)
        //{
        //    var defaultValue = targetType.GetDefaultValue();
        //    var visitedValues = new HashSet<object>();

        //    while (true)
        //    {
        //        var initialValue = value;
        //        var type = value.GetType();

        //        // test target
        //        if (type == targetType)
        //        {
        //            convertedValue = value;
        //            return true;
        //        }

        //        // test conversion cycle
        //        if (!visitedValues.Add(value))
        //        {
        //            convertedValue = defaultValue;
        //            return false;
        //        }

        //        // test NULL value
        //        if (value == null)
        //        {
        //            convertedValue = defaultValue;
        //            return true;
        //        }

        //        // try digest
        //        value = Digest(value, targetType);
        //        if (!initialValue.Equals(value))
        //            continue;

        //        // try convert
        //        value = Convert(value, targetType);
        //    }
        //}
        //private object Convert(object value, Type targetType)
        //{
        //    foreach (var converter in _converters)
        //    {
        //        if (converter.TryConvert(value, targetType, out var val))
        //            return val;
        //    }
        //    return value;
        //}

        //private object Digest(object value, Type targetType)
        //{
        //    bool hasChanged = true;
        //    while (hasChanged)
        //    {
        //        hasChanged = false;

        //        foreach (var digester in _digesters)
        //        {
        //            var newValue = digester.Digest(value, targetType);
        //            if (newValue != value)
        //            {
        //                value = newValue;
        //                hasChanged = true;
        //            }
        //        }

        //        if (value?.GetType() == targetType)
        //            return value;
        //    }
        //    return value;
        //}

        public bool TryConvert(object value, Type targetType, out object convertedValue)
        {
            // get conversion path
            var path = GetCachedConversionPath(value.GetType(), targetType);
            if (path == null)
            {
                convertedValue = null;
                return false;
            }

            // convert value through the path
            foreach (var type in path)
            {
                if (!TryConvertInternal(value, type, out value))
                {
                    convertedValue = null;
                    return false;
                }
            }

            convertedValue = value;
            return true;
        }

        private IList<Type> GetCachedConversionPath(Type sourceType, Type destType)
        {
            return GetConversionPath(sourceType, destType);
        }

        private IList<Type> GetConversionPath(Type sourceType, Type destType)
        {
            var list = new List<Type>();
            var visitedTypes = new HashSet<Type>();

            for (var currentType = sourceType; currentType != destType;)
            {
                // avoid circular conversion
                if (!visitedTypes.Add(currentType))
                    return null;

                // find a digester
                if (OfferDigestion(destType, list, currentType))
                {
                    currentType = list[list.Count - 1];
                    continue;
                }

                list.Add(destType);
                break;
            }

            return list;
        }

        private bool OfferDigestion(Type destType, List<Type> list, Type currentType)
        {
            foreach (var digester in _digesters)
            {
                var offers = digester.Offer(currentType, destType);
                foreach (var offer in offers)
                {
                    if (offer != null && offer != destType)
                    {
                        list.Add(offer);
                        return true;
                    }
                }
            }
            return false;
        }

        private bool TryConvertInternal(object value, Type targetType, out object convertedValue)
        {
            foreach (var converter in _converters)
            {
                if (converter.TryConvert(value, targetType, out convertedValue))
                    return true;
            }

            convertedValue = null;
            return false;
        }
    }
}
