using DotNetTools.ConvertEx.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetTools.ConvertEx.Internal
{
    internal static class Defaults
    {
        public static TypeConverter CreateDefaultConverter()
        {
            return new TypeConverter()
                .AddDigester<NullableDigest>()
                .AddConverter<ToStringConverter>()
                .AddConverter<SystemConverter>();
        }
    }
}
