﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using TestPathDesigner.ConnectionStatusLibrary.Enums;

namespace TestPathDesigner.ConnectionStatusLibrary
{
    public class ConnectionStatusToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ConnectionStatusEnum connectionType = (ConnectionStatusEnum)value;
            switch (connectionType)
            {
                case ConnectionStatusEnum.Unknown:
                    return Brushes.Black;
                case ConnectionStatusEnum.Connected:
                    return "Connected";
                case ConnectionStatusEnum.Disconnected:
                    return "Disconnected";
                case ConnectionStatusEnum.Connecting:
                    return Brushes.Aqua;
                case ConnectionStatusEnum.Disconnecting:
                    return Brushes.Orange;
                default:
                    return Brushes.White;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
