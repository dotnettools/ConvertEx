using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetTools.ConvertEx.Converters
{
    /// <summary>
    /// Wraps or unwraps <see cref="Nullable{T}"/>.
    /// </summary>
    public class NullableDigester : ITypeDigester
    {
        private static readonly Type NullableType = typeof(Nullable<>);

        public IEnumerable<Type> Offer(Type sourceType, Type destType)
        {
            var nullableSource = sourceType.IsGenericType && sourceType.GetGenericTypeDefinition() == NullableType;
            var nullableDest = destType.IsGenericType && destType.GetGenericTypeDefinition() == NullableType;

            if (nullableSource)
                yield return Nullable.GetUnderlyingType(sourceType);
            if (nullableDest)
            {
                var underlay = Nullable.GetUnderlyingType(destType);
                if (underlay != sourceType)
                    yield return underlay;
            }
        }
    }
}
