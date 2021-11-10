using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetTools.ConvertEx
{
    /// <summary>
    /// Base class for implementing <see cref="ITypeConverter"/>
    /// </summary>
    /// <remarks>
    /// It's recommended that you inherit from this class instead of implementing <see cref="ITypeConverter"/> directly.
    /// Doing so will ensure maximum compatibility with possible library updates in the future.
    /// </remarks>
    public abstract class TypeConverterBase : ITypeConverter
    {
        public abstract bool TryConvert(object value, Type targetType, out object convertedValue);
    }
}
