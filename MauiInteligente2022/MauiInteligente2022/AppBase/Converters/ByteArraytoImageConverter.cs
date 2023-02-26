using System.Globalization;

namespace MauiInteligente2022.AppBase.Converters;

public class ByteArraytoImageConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if(value is byte[] photoBytes) 
        {
            return ImageSource.FromStream(() => new MemoryStream(photoBytes));
        }
        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
