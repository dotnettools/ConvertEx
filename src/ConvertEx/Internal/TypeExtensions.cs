using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetTools.ConvertEx.Internal
{
    internal static class TypeExtensions
    {
        public static object GetDefaultValue(this Type type)
        {
            if (!type.IsValueType)
                return null;

            return Activator.CreateInstance(type);
        }
    }
}
