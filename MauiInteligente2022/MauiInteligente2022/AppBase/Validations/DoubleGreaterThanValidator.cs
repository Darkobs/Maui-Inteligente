namespace MauiInteligente2022.AppBase.Validations;

public class DoubleGreaterThanValidator : FieldValidation<Decimal>
{
    public Decimal ObjectToValidate
    {
        get;
        set;
    }
    public decimal MinimumValue { get; set; }
    public DoubleGreaterThanValidator(Decimal objectToValidate, decimal minimumValue, string errorMessage)
    {
        ObjectToValidate = objectToValidate;
        ErrorMessage = errorMessage;
        MinimumValue = minimumValue;
    }
    public DoubleGreaterThanValidator(Decimal objectToValidate, decimal minimumValue)
        : this(objectToValidate, minimumValue, string.Format(Resources.ValidationsGreater, minimumValue))
    {
    }
    public override void Validate() => IsValid = ObjectToValidate >= MinimumValue ? ValidationResult.Valid : ValidationResult.Invalid;
}
