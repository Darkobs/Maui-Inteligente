using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;

namespace MauiInteligente2022;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureLifecycleEvents
			(
				events =>
				{

                }
            )
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
		builder.Services.AddTransient<LoginPage>()
						.AddTransient<LoginViewModel>()
						.AddTransient<SplashPage>()
						.AddTransient<SplashViewModel>()
						.AddTransient<LanguageSelectionPage>()
						.AddTransient<LanguageSelectionViewModel>()
						.AddTransient<TermsAndConditionsPage>()
						.AddTransient<TermsAndConditionsViewModel>()
						.AddTransient<SignUpPage>()
						.AddTransient<SignUpViewModel>();

		builder.Services.AddHttpClient<SignUpViewModel>(client =>
		{
			client.Timeout = TimeSpan.FromSeconds(40);
			client.BaseAddress = new("https://apinetmauinteligente22.azurewebsites.net");
		});

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
