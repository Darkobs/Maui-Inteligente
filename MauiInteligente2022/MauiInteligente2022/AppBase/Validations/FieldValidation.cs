namespace MauiInteligente2022.AppBase.Validations;

public abstract class FieldValidation<T> : ObservableObject
{
    private ValidationResult isValid;

    public ValidationResult IsValid
    {
        get => isValid; 
        set => SetProperty(ref isValid,value);
    }

    private string errorMessage;

    public string ErrorMessage
    {
        get => errorMessage;
        set => SetProperty(ref errorMessage,value);
    }

    public abstract void Validate();
}
