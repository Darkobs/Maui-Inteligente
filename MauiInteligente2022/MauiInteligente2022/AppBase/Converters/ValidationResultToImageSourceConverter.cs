using System.Globalization;
using MauiInteligente2022.AppBase.Validations;
namespace MauiInteligente2022.AppBase.Converters;

public class ValidationResultToImageSourceConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if(value is ValidationResult validationResult)
        {
            return validationResult switch
            {
                ValidationResult.None => null,
                ValidationResult.Valid => ImageSource.FromFile("correct"),
                ValidationResult.Invalid => ImageSource.FromFile("incorrec"),
                _ => null
            };
        }
        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
