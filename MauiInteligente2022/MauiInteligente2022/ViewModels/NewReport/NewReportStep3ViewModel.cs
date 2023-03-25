using MauiInteligente2022.AppBase.Validations;

namespace MauiInteligente2022.ViewModels;

public class NewReportStep3ViewModel : BaseViewModel
{
    bool loaded = false;
    private readonly LocalFilesHelper _localFilesHelper;
    public NewReportStep3ViewModel(LocalFilesHelper localFilesHelper)
    {
        _localFilesHelper = localFilesHelper;
        Title = Resources.NewReportTitle;
        PageId = NEW_REPORT_STEP_3;
        NextCommand = new(async () => await Next());
        LoadData();
        NavigationParametersToSend[STEP3_VM_PARAMETER] = this;
    }
    public override void ApplyQueryAttributes(IDictionary<string, object> navigationParameters)
    {
        NavigationParametersToSend[STEP1_VM_PARAMETER]
            = navigationParameters[STEP1_VM_PARAMETER];
        NavigationParametersToSend[STEP2_VM_PARAMETER]
            = navigationParameters[STEP2_VM_PARAMETER];
    }
    private async Task Next()
    {
        await SaveAsync();
        bool isValid = ValidateFields();
        if (isValid)
        {
            await Shell.Current.GoToAsync(NEW_REPORT_SUMMARY, NavigationParametersToSend);
        }
    }

    public async override Task SaveAsync()
    {
        NewReportStep3 reportStep3Save = new(ReportDescription, Amount);
        var saveJson = JsonSerializer.Serialize(reportStep3Save);
        await _localFilesHelper.SaveFileAsync(NEW_REPORT_STEP_3, saveJson);
    }
    private async Task LoadData()
    {
        if (loaded == true)
            return;
        IsBusy = true;
        var savedJson = await _localFilesHelper.ReadTextFileAsync(NEW_REPORT_STEP_3);
        if (savedJson is not null)
        {
            var savedReport = JsonSerializer.Deserialize<NewReportStep3>(savedJson);
            Amount = savedReport.Amount;
            ReportDescription = savedReport.ReportDescription;
            loaded = true;
        }
        IsBusy = false;
    }
    public Command NextCommand { get; set; }
    private string reportDescription;
    public string ReportDescription
    {
        get => reportDescription;
        set => SetProperty(ref reportDescription, value);
    }
    private decimal amount;
    public decimal Amount
    {
        get => amount;
        set => SetProperty(ref amount, value);
    }
    #region Validations
    List<Validator<Decimal>> validators = new();
    private void InitValidations()
    {
        if (validators.Any())
            validators.Clear();
        AmountValidator = new(amount, false);
        AmountValidator.Validations.Add(new DoubleGreaterThanValidator(amount, 0.1m));
        validators.Add(AmountValidator);
    }
    private bool ValidateFields()
    {
        InitValidations();
        bool valid = true;
        foreach (var validator in validators)
        {
            validator.Validate();
            valid &= (validator.IsValid == ValidationResult.Valid);
        }
        return valid;
    }
    private Validator<string> reportDescriptionValidator;
    public Validator<string> ReportDescriptionValidator
    {
        get => reportDescriptionValidator;
        set => SetProperty(ref reportDescriptionValidator, value);
    }
    private Validator<Decimal> amountValidator;
    public Validator<Decimal> AmountValidator
    {
        get => amountValidator;
        set => SetProperty(ref amountValidator, value);
    }
    #endregion
}
