namespace MauiInteligente2022.AppBase.Validations;

public class ExactLengthValidator : FieldValidation<string>
{
    public string ObjectToValidate
    {
        get;
        set;
    }
    public int Length { get; set; }
    public ExactLengthValidator(string objectToValidate, int length, string errorMessage)
    {
        ObjectToValidate = objectToValidate;
        ErrorMessage = errorMessage;
        Length = length;
    }
    public ExactLengthValidator(string objectToValidate, int length)
        : this(objectToValidate, length, string.Format(Resources.ValidationsExactLength, length))
    {
    }
    public override void Validate()
            => IsValid = ObjectToValidate.Length == Length ? ValidationResult.Valid : ValidationResult.Invalid;
}
