using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetTools.ConvertEx.Converters
{
    /// <summary>
    /// Uses <see cref="System.Convert"/> to convert the types.
    /// </summary>
    public class SystemConverter : TypeConverterBase
    {
        public override bool TryConvert(object value, Type targetType, out object convertedValue)
        {
            try
            {
                convertedValue = System.Convert.ChangeType(value, targetType);
                return true;
            }
            catch (InvalidCastException)
            {
                convertedValue = null;
                return false;
            }
        }
    }
}
