using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;

namespace DotNetTools.ConvertEx.Converters
{
    /// <summary>
    /// Converts to and from <see cref="Nullable{T}"/>.
    /// </summary>
    public class NullableConverter : TypeConverterBase
    {
        private static readonly Type NullableType = typeof(Nullable<>);

        public override bool TryConvert(object value, Type destType, IFormatProvider formatProvider, out object convertedValue)
        {
            if (value != null)
            {
                var sourceType = value.GetType();
                var nullableSource = sourceType.IsGenericType && sourceType.GetGenericTypeDefinition() == NullableType;
                var nullableDest = destType.IsGenericType && destType.GetGenericTypeDefinition() == NullableType;
                if (nullableSource ^ nullableDest)
                {
                    var sourceUnderlay = nullableSource ? Nullable.GetUnderlyingType(sourceType) : sourceType;
                    var destUnderlay = nullableDest ? Nullable.GetUnderlyingType(destType) : destType;
                    if (sourceUnderlay == destUnderlay)
                    {
                        if (nullableSource)
                            convertedValue = System.Convert.ChangeType(value, destType);
                        else
                            convertedValue = Activator.CreateInstance(destType, value);
                        return true;
                    }
                }
            }

            convertedValue = null;
            return false;
        }
    }
}
