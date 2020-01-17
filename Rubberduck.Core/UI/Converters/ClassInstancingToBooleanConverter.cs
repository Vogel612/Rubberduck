﻿using System;
using System.Globalization;
using System.Windows.Data;
using Rubberduck.Refactorings.ExtractInterface;

namespace Rubberduck.UI.Converters
{
    class ClassInstancingToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (ClassInstancing)value == ClassInstancing.PublicNotCreatable;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value
                ? ClassInstancing.PublicNotCreatable
                : ClassInstancing.Private;
        }
    }
}
