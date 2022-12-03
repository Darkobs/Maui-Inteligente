using static System.String;
namespace MauiInteligente2022.ViewModels;

public class LoginViewModel : BaseViewModel
{
    public LoginViewModel() 
    {
        Title = Resources.LoginTitle;
        SubTitle = Resources.LoginSubtitle;
        PageId = LOGIN_PAGE_ID;

        LoginCommand = new(async () => LoginAsync(), () => !IsNullOrWhiteSpace(_userName) && !IsNullOrWhiteSpace(_password));
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

    public Command LoginCommand { get; set; }

    private async Task LoginAsync()
    {
        if(!IsBusy)
        {
            IsBusy = true;
            
            await Task.Delay(5000);

            Application.Current.MainPage = new AppShell();

            IsBusy = false;
        }
    }
}
