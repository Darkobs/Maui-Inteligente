﻿using MauiInteligente2022.AppBase.Validations;
using System.Net.Http.Json;

namespace MauiInteligente2022.ViewModels;

public class SignUpViewModel : BaseViewModel
{
    private readonly HttpClient signupHttpClient;
	public SignUpViewModel(HttpClient signupHttpClient)
	{
		PageId = SIGNUP_PAGE_ID;
		Title = Resources.SignUpTitle;
		SubTitle = Resources.SignUpSubtitle;
        CreateUserCommand = new(async () => await CreateUser(), () => IsValid);
        this.signupHttpClient = signupHttpClient;
	}

    #region Commands
    public Command CreateUserCommand { get; set; }

    public Command CancelCommand { get; set; }

    record NewUserDTO(string UserName, string Password, string Email, string PhoneNumber, string Address);

    private async Task CreateUser()
    {
        if(!IsBusy) 
        {   
            IsBusy = true;

            NewUserDTO newUser = new(UserName, Password, Email, PhoneNumber, Address);

            HttpResponseMessage httpResponse =
                await signupHttpClient.PostAsJsonAsync("", newUser);

            if(httpResponse.IsSuccessStatusCode) 
            {
                await Application.Current.MainPage.DisplayAlert(Resources.SignUpUserAlertTitle,
                                                            Resources.SignUpAlertSuccessUserCreation,
                                                            Resources.AcceptButton);
                CleanData();
            }
            else
                await Application.Current.MainPage.DisplayAlert(Resources.SignUpUserAlertTitle, 
                                                                Resources.SignUpAlertErrorUserCreation, 
                                                                Resources.AcceptButton);

            IsBusy = false;
        }
    }

    #endregion

    #region UserData
    private string userName;

	public string UserName
	{
		get => userName; 
		set => SetProperty(ref userName, value);
	}

    private string password;

    public string Password
    {
        get => password;
        set => SetProperty(ref password, value);
    }

    private string address;

    public string Address
    {
        get => address;
        set => SetProperty(ref address, value);
    }

    private string email;

    public string Email
    {
        get => email;
        set => SetProperty(ref email, value);
    }

    private string phoneNumber;

    public string PhoneNumber
    {
        get => phoneNumber;
        set => SetProperty(ref phoneNumber, value);
    }

    private void CleanData()
    {
        UserName = null;
        Password = null;
        Address = null;
        Email = null;
        PhoneNumber = null;
    }
    #endregion

    #region Validations
    private ValidationResult userNameValidationResult;

    public ValidationResult UserNameValidationResult
    {
        get => userNameValidationResult;
        set => SetProperty(ref userNameValidationResult, value, onChanged: ValidateAll);
    }

    private ValidationResult passwordValidationResult;

    public ValidationResult PasswordValidationResult
    {
        get => passwordValidationResult;
        set => SetProperty(ref passwordValidationResult, value, onChanged: ValidateAll);
    }

    private ValidationResult addressValidationResult;

    public ValidationResult AddressValidationResult
    {
        get => addressValidationResult;
        set => SetProperty(ref addressValidationResult, value, onChanged: ValidateAll);
    }

    private ValidationResult emailValidationResult;

    public ValidationResult EmailValidationResult
    {
        get => emailValidationResult;
        set => SetProperty(ref emailValidationResult, value, onChanged: ValidateAll);
    }

    private ValidationResult phoneNumberValidationResult;

    public ValidationResult PhoneNumberValidationResult
    {
        get => phoneNumberValidationResult;
        set => SetProperty(ref phoneNumberValidationResult, value, onChanged: ValidateAll);
    }

    private bool isValid;

    public bool IsValid
    {
        get => isValid;
        set => SetProperty(ref isValid, value, onChanged: () => CreateUserCommand.ChangeCanExecute());
    }

    private string errorMessage;

    public string ErrorMessage
    {
        get => errorMessage;
        set => SetProperty(ref errorMessage, value);
    }

    private void ValidateAll()
    {
        IsValid = UserNameValidationResult == ValidationResult.Valid
                && PasswordValidationResult == ValidationResult.Valid
                && AddressValidationResult == ValidationResult.Valid
                && EmailValidationResult == ValidationResult.Valid
                && PhoneNumberValidationResult == ValidationResult.Valid;

        if (UserNameValidationResult == ValidationResult.Invalid)
            ErrorMessage = Resources.SignUpInvalidUserNameMessage;
        else if (PasswordValidationResult == ValidationResult.Invalid)
            ErrorMessage = Resources.SignUpInvalidPasswordMessage;
        else if (AddressValidationResult == ValidationResult.Invalid)
            ErrorMessage = Resources.SignUpInvalidAddressMessage;
        else if (EmailValidationResult == ValidationResult.Invalid)
            ErrorMessage = Resources.SignUpInvalidEmailMessage;
        else if (PhoneNumberValidationResult == ValidationResult.Invalid)
            ErrorMessage = Resources.SignUpInvalidPhoneMessage;
        else
            ErrorMessage = string.Empty;
    }  
    #endregion
}
