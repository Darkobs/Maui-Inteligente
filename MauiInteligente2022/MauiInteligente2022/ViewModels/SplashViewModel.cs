﻿using System.Globalization;
using System.IdentityModel.Tokens.Jwt;

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
        BindedPage next = null;

        if(AppConfiguration.UserToken is not null)
        {
            var securityToken = new JwtSecurityToken(AppConfiguration.UserToken);

            if(securityToken.ValidTo > DateTime.UtcNow && securityToken.ValidFrom < DateTime.UtcNow) 
            {
                //next = _sp.GetRequiredService<MainMenuPage>(); 
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                AppConfiguration.UserToken = null;
                next = _sp.GetRequiredService<LoginPage>();
            }
        }
        else
        {
            if (AppConfiguration.HasLanguageSelection)
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

            next = AppConfiguration.UserAcceptTerms
                ? _sp.GetRequiredService<LoginPage>()
                : _sp.GetRequiredService<LanguageSelectionPage>();
        } 

        if(next is not null)
        {
            Application.Current.MainPage = new NavigationPage(next);
        }
    }
}
