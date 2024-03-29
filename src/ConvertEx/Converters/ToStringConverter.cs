﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetTools.ConvertEx.Converters
{
    /// <summary>
    /// Implements <see cref="ITypeConverter"/> with the ability to change any type to string.
    /// </summary>
    public class ToStringConverter : TypeConverterBase
    {
        public override bool TryConvert(object value, Type targetType, IFormatProvider formatProvider, out object convertedValue)
        {
            if (targetType != typeof(string))
            {
                convertedValue = null;
                return false;
            }

            convertedValue = value?.ToString();
            return true;
        }
    }
}
