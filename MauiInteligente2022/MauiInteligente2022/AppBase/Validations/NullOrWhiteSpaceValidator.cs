namespace MauiInteligente2022.AppBase.Validations;

public class NullOrWhiteSpaceValidator<T> : FieldValidation<T>
{
    public NullOrWhiteSpaceValidator(object objectToValidate)
        :this(objectToValidate, Resources.ValidationsNull)
    {
    }

    public NullOrWhiteSpaceValidator(object objectToValidate, string errorMessage)
    {
        ObjectToValidate = objectToValidate;
        ErrorMessage = errorMessage;
    }

    public object ObjectToValidate { get; set; }

    public override void Validate()
    {
        if(ObjectToValidate is string text)
        {
            IsValid = !string.IsNullOrEmpty(text) ? ValidationResult.Valid : ValidationResult.Invalid;
        }
        else if(ObjectToValidate is null)
        {
            IsValid = ValidationResult.Invalid;
        }
        else
        {
            IsValid = ValidationResult.Valid;
        }
    }
}
