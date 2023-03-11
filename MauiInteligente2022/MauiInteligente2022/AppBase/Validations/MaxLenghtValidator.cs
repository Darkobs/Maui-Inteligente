namespace MauiInteligente2022.AppBase.Validations;

public class MaxLengthValidator : FieldValidation<string>
{
    public string ObjectToValidate
    {
        get;
        set;
    }
    public int MaxLenght { get; set; }
    public MaxLengthValidator(string objectToValidate, int maxLength, string errorMessage)
    {
        ObjectToValidate = objectToValidate;
        ErrorMessage = errorMessage;
        MaxLenght = maxLength;
    }
    public MaxLengthValidator(string objectToValidate, int maxLength)
        : this(objectToValidate, maxLength, string.Format(Resources.ValidationsMaxLength))
    {
    }
    public override void Validate() => IsValid = ObjectToValidate.Length <= MaxLenght ? ValidationResult.Valid : ValidationResult.Invalid;
}
