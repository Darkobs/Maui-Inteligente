using System.Net.Http.Json;
using static System.String;
namespace MauiInteligente2022.ViewModels;

public class LoginViewModel : BaseViewModel
{
    private readonly IServiceProvider serviceProvider;
    private readonly HttpClient httpClient;

    public LoginViewModel(IServiceProvider serviceProvider, HttpClient httpClient) 
    {
        Title = Resources.LoginTitle;
        SubTitle = Resources.LoginSubtitle;
        PageId = LOGIN_PAGE_ID;

        this.serviceProvider = serviceProvider;
        this.httpClient = httpClient;

        LoginCommand = new(async () => LoginAsync(), () => {
            CanExecuteLogin = !IsNullOrWhiteSpace(_userName) && !IsNullOrWhiteSpace(_password);
            return CanExecuteLogin;
        });

        SignupCommand = new Command(async () => await SignupAsync());

#if DEBUG
        UserName = "cmurillo";
        Password = "cmurillo123*";
#endif

    }

    private string _userName;

    public string UserName
    {
        get => _userName;
        set => SetProperty(ref _userName, value, onChanged: () => LoginCommand.ChangeCanExecute());
    }

    private string _password;

    public string Password
    {
        get => _password;
        set => SetProperty(ref _password, value, onChanged: () => LoginCommand.ChangeCanExecute());
    }

    private bool canExecuteLogin;

    public bool CanExecuteLogin
    {
        get => canExecuteLogin;
        set => SetProperty(ref canExecuteLogin, value, onChanged: () => LoginCommand.ChangeCanExecute());
    }

    public Command LoginCommand { get; set; }

    public Command SignupCommand { get; set; }

    private async Task LoginAsync()
    {
        if(!IsBusy)
        {
            IsBusy = true;
            
            //await Task.Delay(5000);
            LoginCredentials loginCredentials = new(UserName, Password);

            var loginResponse = await httpClient.PostAsJsonAsync("", loginCredentials);

            if (loginResponse.IsSuccessStatusCode)
            {
                var userToken = await loginResponse.Content.ReadFromJsonAsync<string>();
                AppConfiguration.UserToken = userToken;
                Application.Current.MainPage = new AppShell();
            }
                
            else
                await Application.Current.MainPage.DisplayAlert(Resources.LoginUserAlertTitle,
                    Resources.LoginAlertError,
                    Resources.AcceptButton);

            IsBusy = false;
        }
    }

    private async Task SignupAsync()
    {
        await App.Current.MainPage.Navigation.PushAsync(serviceProvider.GetRequiredService<SignUpPage>());
    }
}
