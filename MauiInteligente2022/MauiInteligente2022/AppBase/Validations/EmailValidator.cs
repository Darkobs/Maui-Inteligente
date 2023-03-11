namespace MauiInteligente2022.AppBase.Validations;

public class EmailValidator : FieldValidation<string>
{
    public EmailValidator(string textToValidate, string errorMessage)
    {
        TextToValidate = textToValidate;
        ErrorMessage = errorMessage;
    }

    public EmailValidator(string textToValidate) 
        : this(textToValidate, Resources.ValidationsEmail)
    
    {
    }

    public string TextToValidate { get; set; }

    public override void Validate()
        => IsValid = Helpers.ValidationsHelper.ValidateString(ValidationType.Email, TextToValidate);
}
