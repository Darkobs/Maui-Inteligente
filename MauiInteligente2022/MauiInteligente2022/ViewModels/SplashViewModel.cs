namespace MauiInteligente2022.ViewModels;

public class SplashViewModel : BaseViewModel
{
    private readonly IServiceProvider _sp;

	public SplashViewModel(IServiceProvider sp)
	{
        _sp = sp;
        PageId = SPLASH_PAGE;
	}

    public async override Task OnAppearing()
    {
        await Task.Delay(3000);
        BindedPage next = _sp.GetRequiredService<LoginPage>();

        Application.Current.MainPage = new NavigationPage(next);
    }
}
