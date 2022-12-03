using System.Globalization;

namespace MauiInteligente2022.ViewModels;

public class SplashViewModel : BaseViewModel
{
    private readonly IServiceProvider _sp;

	public SplashViewModel(IServiceProvider sp)
	{
        _sp = sp;
        PageId = SPLASH_PAGE_ID;
	}

    public async override Task OnAppearing()
    {
        if(AppConfiguration.HasLanguageSelection)
        {
            CultureInfo cultureInfo = AppConfiguration.AppLanguage switch
            {
                Languages.Spanish => new("es-mx"),
                Languages.English => new("en"),
                _ => new("en")
            };

            Resources.Culture = cultureInfo;
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }

        BindedPage next = AppConfiguration.UserAcceptTerms
            ? _sp.GetRequiredService<LoginPage>()
            : _sp.GetRequiredService<LanguageSelectionPage>();

        Application.Current.MainPage = new NavigationPage(next);
    }
}
