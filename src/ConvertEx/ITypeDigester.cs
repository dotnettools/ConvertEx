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
        /// Offers a type to convert to first before converting to <paramref name="targetType"/>.
        /// </summary>
        /// <returns>NULL or <paramref name="targetType"/> if the digester has nothing to offer; otherwise the midst type.</returns>
        IEnumerable<Type> Offer(Type sourceType, Type targetType);
    }
}
