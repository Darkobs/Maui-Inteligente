using MauiInteligente2022.AppBase.Converters;
using MauiInteligente2022.AppBase.Validations;
using System.Collections.ObjectModel;

namespace MauiInteligente2022.ViewModels;

public class NewReportStep2ViewModel : BaseViewModel
{
    private readonly LocalFilesHelper _localFilesHelper;
    private readonly Dictionary<string, object> _navigationParameters = new();
    private bool loaded;

    private readonly CountriesRestServices _countriesRestServices;
    public NewReportStep2ViewModel(LocalFilesHelper localFilesHelper, CountriesRestServices countriesRestServices)
    {
        _countriesRestServices = countriesRestServices;
        _localFilesHelper = localFilesHelper;
        PageId = NEW_REPORT_STEP_2;
        Title = Resources.NewReportTitle;
        SubTitle = Resources.NewReportStep2Subtitle;
        NextCommand = new(async () => await NextAsync());
        InitValidations();
        _navigationParameters[STEP2_VM_PARAMETER] = this;
        LoadData();
    }

    public override void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        _navigationParameters[STEP1_VM_PARAMETER] = query[STEP1_VM_PARAMETER];
    }

    private async Task LoadData()
    {
        if (!IsBusy)
        {
            IsBusy = true;
            await LoadCatalogs();

            var savedJson = await _localFilesHelper.ReadTextFileAsync(NEW_REPORT_STEP_2);
            if (savedJson is not null)
            {
                var savedReport = System.Text.Json.JsonSerializer.Deserialize<NewReportStep2>(savedJson);
                ClientName = savedReport.ClientName;
                ClientNumber = savedReport.ClientNumber;
                ClientPhoneNumber = savedReport.ClientPhoneNumber;
                ClientCity = savedReport.ClientCity;
                ClientEmail = savedReport.ClientEmail;
                SelectedCountry = Countries.SingleOrDefault(c => c.CountryCode == savedReport.ClientCountry);
                SelectedDocument = Documents.SingleOrDefault(d => d == savedReport.ClientDocument);
                ClientDocumentNumber = savedReport.ClientDocumentNumber;
            }

            loaded = true;
            IsBusy = false;
        }
    }

    private async Task LoadCatalogs()
    {
        loaded = false;

        var response = await _countriesRestServices.GetAllAsync("");

        if (response.Status == BaseRestClientCore.Enums.RestResponseStatus.Success)
            Countries = new(response.ResponseContent);

        Documents = new()
        {
            Resources.DocumentCatalogCountryID,
            Resources.DocumentCatalogDriversLicense,
            Resources.DocumentsCatalogPassport
        };
    }

    public async override Task SaveAsync()
    {
        NewReportStep2 reportStep2Save = new(ClientNumber, ClientName, ClientPhoneNumber, ClientEmail,
            SelectedCountry?.CountryCode, ClientCity, selectedDocument, ClientDocumentNumber);
        
        var saveJson = System.Text.Json.JsonSerializer.Serialize(reportStep2Save);
        
        await _localFilesHelper.SaveFileAsync(NEW_REPORT_STEP_2, saveJson);
    }

    #region Properties
    public Command NextCommand { get; set; }
    private ObservableCollection<Country> countries;   
    public ObservableCollection<Country> Countries    
    {       
        get => countries;       
        set => SetProperty(ref countries, value);    
    }
   
private ObservableCollection<string> documents;
    public ObservableCollection<string> Documents
    {
        get => documents;
        set => SetProperty(ref documents, value);
    }
    private string clientName;
    public string ClientName
    {
        get => clientName;
        set => SetProperty(ref clientName, value);
    }
    private string clientPhoneNumber;
    public string ClientPhoneNumber
    {
        get => clientPhoneNumber;
        set => SetProperty(ref clientPhoneNumber, value);
    }
    private string clientEmail;
    public string ClientEmail
    {
        get => clientEmail;
        set => SetProperty(ref clientEmail, value);
    }
    private string clientNumber;
    public string ClientNumber
    {
        get => clientNumber;
        set => SetProperty(ref clientNumber, value);
    }
    private Country selectedCountry;    
    
    public Country SelectedCountry    
    {        
        get => selectedCountry;        
        set        
        {            
            SetProperty(ref selectedCountry, value);            
            if (loaded) 
                SaveAsync();        
        }
    }
    
    private string clientCity;
    public string ClientCity
    {
        get => clientCity;
        set => SetProperty(ref clientCity, value);
    }

    private string clientCountry;
    public string ClientCountry
    {
        get => clientCountry;
        set => SetProperty(ref clientCountry, value);
    }

    private string selectedDocument;
    public string SelectedDocument
    {
        get => selectedDocument;
        set => SetProperty(ref selectedDocument, value);
    }
    private string clientDocumentNumber;
    public string ClientDocumentNumber
    {
        get => clientDocumentNumber;
        set => SetProperty(ref clientDocumentNumber, value);
    }
    #endregion

    #region Validators
    private Validator<string> clientNameValidator;
    public Validator<string> ClientNameValidator
    {
        get => clientNameValidator;
        set => SetProperty(ref clientNameValidator, value);
    }
    private Validator<string> clientPhoneNumberValidator;
    public Validator<string> ClientPhoneNumberValidator
    {
        get => clientPhoneNumberValidator;
        set => SetProperty(ref clientPhoneNumberValidator, value);
    }
    private Validator<string> clientEmailValidator;
    public Validator<string> ClientEmailValidator
    {
        get => clientEmailValidator;
        set => SetProperty(ref clientEmailValidator, value);
    }
    private Validator<string> clientNumberValidator;
    public Validator<string> ClientNumberValidator
    {
        get => clientNumberValidator;
        set => SetProperty(ref clientNumberValidator, value);
    }
    private Validator<string> clientCountryValidator;
    public Validator<string> ClientCountryValidator
    {
        get => clientCountryValidator;
        set => SetProperty(ref clientCountryValidator, value);
    }
    private Validator<string> clientCityValidator;
    public Validator<string> ClientCityValidator
    {
        get => clientCityValidator;
        set => SetProperty(ref clientCityValidator, value);
    }
    private Validator<string> clientDocumentValidator;
    public Validator<string> ClientDocumentValidator
    {
        get => clientDocumentValidator;
        set => SetProperty(ref clientDocumentValidator, value);
    }
    private Validator<string> clientDocumentNumberValidator;
    public Validator<string> ClientDocumentNumberValidator
    {
        get => clientDocumentNumberValidator;
        set => SetProperty(ref clientDocumentNumberValidator, value);
    }
    #endregion

    #region ValidationsLogic

    List<Validator<string>> validators = new();

    private void InitValidations()
    {
        validators.Clear();

        ClientNumberValidator = new(ClientNumber, true);
        ClientNumberValidator.Validations.Add(new ExactLengthValidator(ClientNumber, 10));

        ClientNameValidator = new(ClientName, true);

        ClientPhoneNumberValidator = new(ClientPhoneNumber, true);
        ClientPhoneNumberValidator.Validations.Add(new TelephoneNumberValidator(clientPhoneNumber));

        ClientEmailValidator = new(ClientEmail, true);
        ClientEmailValidator.Validations.Add(new EmailValidator(clientEmail));

        ClientCityValidator = new(ClientCity, true);

        ClientDocumentValidator = new(selectedDocument, true);

        ClientDocumentNumberValidator = new(ClientDocumentNumber, true);

        ClientCountryValidator = new(SelectedCountry?.CountryCode, true);

        validators.Add(ClientNumberValidator);
        validators.Add(ClientNameValidator);
        validators.Add(ClientPhoneNumberValidator);
        validators.Add(ClientEmailValidator);
        validators.Add(ClientCityValidator);
        validators.Add(ClientDocumentValidator);
        validators.Add(ClientDocumentNumberValidator);
        validators.Add(ClientCountryValidator);

    }

    private bool Validate()
    {
        InitValidations();
        bool isValid = true;

        foreach(var validator in validators)
        {
            validator.Validate();
            isValid &= validator.IsValid == ValidationResult.Valid;
        }

        return isValid;
    }
    
    #endregion

    private async Task NextAsync()
    {
        if (Validate())
        {
            _navigationParameters[STEP2_VM_PARAMETER] = this;
            await Shell.Current.GoToAsync(NEW_REPORT_STEP_3, _navigationParameters);
        }
    }
}
