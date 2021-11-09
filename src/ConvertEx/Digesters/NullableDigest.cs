using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetTools.ConvertEx.Converters
{
    /// <summary>
    /// Wraps or unwraps <see cref="Nullable{T}"/>.
    /// </summary>
    public class NullableDigest : ITypeDigester
    {
        public object Digest(object value, Type targetType)
        {
            return value;
        }
    }
}
