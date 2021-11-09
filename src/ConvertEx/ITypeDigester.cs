using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetTools.ConvertEx
{
    /// <summary>
    /// Helps type conversion by transforming values to types closer to target.
    /// </summary>
    public interface ITypeDigester
    {
        /// <summary>
        /// Digests <paramref name="value"/> so it's closer to or of <paramref name="targetType"/>.
        /// </summary>
        object Digest(object value, Type targetType);
    }
}
