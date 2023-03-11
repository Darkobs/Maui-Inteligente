namespace MauiInteligente2022.AppBase.Validations;

public class TelephoneNumberValidator : FieldValidation<string>
{
    public string ObjectToValidate
    {
        get;
        set;
    }
    public TelephoneNumberValidator(string objectToValidate, string errorMessage)
    {
        ObjectToValidate = objectToValidate;
        ErrorMessage = errorMessage;
    }
    public TelephoneNumberValidator(string objectToValidate)
    {
        ObjectToValidate = objectToValidate;
        ErrorMessage = Resources.ValidationsPhone;
    }
    public override void Validate() => IsValid = Helpers.ValidationsHelper.ValidateString(ValidationType.Phone, ObjectToValidate);
}
