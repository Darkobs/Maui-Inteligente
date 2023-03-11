namespace MauiInteligente2022.AppBase.Validations;

public class Validator<T> : ObservableObject
{
    public Validator()
    {
        
    }

    public Validator(T objectToValidate, bool validateNullOrWhiteSpace)
    {
        if(validateNullOrWhiteSpace)
            Validations.Add(new NullOrWhiteSpaceValidator<T>(objectToValidate));
    }

    public List<FieldValidation<T>> Validations { get; set; } = new();

    private ValidationResult isValid;

    public ValidationResult IsValid
    {
        get => isValid;
        set => SetProperty(ref isValid, value);
    }

    private string errorMessage;

    public string ErrorMessage
    {
        get => errorMessage;
        set => SetProperty(ref errorMessage, value);
    }

    public void Validate() 
    {
        foreach (var validation in Validations)
        {
            validation.Validate();

            IsValid = validation.IsValid;
            ErrorMessage = validation.ErrorMessage;

            if (IsValid == ValidationResult.Invalid)
                break;
        }
    }
}
