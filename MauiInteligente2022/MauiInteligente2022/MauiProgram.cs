﻿using MauiInteligente2022.AppBase.Services.GoogleApis;
using MauiInteligente2022.RestServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using System.Reflection;

namespace MauiInteligente2022;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiMaps()
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

		using var jsonConfig = Assembly.GetExecutingAssembly().GetManifestResourceStream("MauiInteligente2022.appsettings.json");

		var config = new ConfigurationBuilder()
			.AddJsonStream(jsonConfig)
			.Build();

		builder.Configuration.AddConfiguration(config);

		builder.Services.AddTransient<LoginPage>()
						.AddTransient<LoginViewModel>()
						.AddTransient<SplashPage>()
						.AddTransient<SplashViewModel>()
						.AddTransient<LanguageSelectionPage>()
						.AddTransient<LanguageSelectionViewModel>()
						.AddTransient<TermsAndConditionsPage>()
						.AddTransient<TermsAndConditionsViewModel>()
						.AddTransient<SignUpPage>()
						.AddTransient<SignUpViewModel>()
						.AddTransient<MainMenuPage>()
						.AddTransient<MainMenuViewModel>()
						.AddTransient<AboutPage>()
						.AddTransient<AboutViewModel>()
						.AddTransient<BranchDetailPage>()
						.AddTransient<BranchDetailViewModel>()
						.AddTransient<GoogleDirectionsApiClient>()
						.AddTransient<LocationsPage>()
						.AddTransient<LocationsViewModel>()
						.AddTransient<NewReportStep1Page>()
						.AddTransient<NewReportStep1ViewModel>()
                        .AddTransient<NewReportStep2Page>()
                        .AddTransient<NewReportStep2ViewModel>()
                        .AddTransient<NewReportStep3Page>()
                        .AddTransient<NewReportStep3ViewModel>()
                        .AddTransient<ReportSummaryViewModel>()
                        .AddTransient<PreviewPhotoPage>()
						.AddTransient<PreviewPhotoViewModel>();

		builder.Services.AddSingleton<MediaHelper>()
						.AddSingleton<LocalFilesHelper>();

		builder.Services.AddHttpClient<SignUpViewModel>(client =>
		{
            //client.Timeout = TimeSpan.FromSeconds(40);
            //client.BaseAddress = new("https://apinetmauinteligente22.azurewebsites.net");
            client.Timeout = TimeSpan.FromSeconds(int.Parse(builder.Configuration["Api:Timeout"]));
			client.BaseAddress = new($"{builder.Configuration["Api:Uri"]}{builder.Configuration["Api:Signup"]}");
		});

		builder.Services.AddHttpClient<BranchRestServices>(client =>
		{
            client.Timeout = TimeSpan.FromSeconds(int.Parse(builder.Configuration["Api:Timeout"]));
            client.BaseAddress = new($"{builder.Configuration["Api:Uri"]}{builder.Configuration["Api:Branches"]}");
        });

        builder.Services.AddHttpClient<CountriesRestServices>(client =>
        {
            client.Timeout = TimeSpan.FromSeconds(int.Parse(builder.Configuration["Api:Timeout"]));
            client.BaseAddress = new($"{builder.Configuration["Api:Uri"]}{builder.Configuration["Api:Countries"]}");
        });

        builder.Services.AddHttpClient<LoginViewModel>(client =>
        {
            client.Timeout = TimeSpan.FromSeconds(int.Parse(builder.Configuration["Api:Timeout"]));
            client.BaseAddress = new($"{builder.Configuration["Api:Uri"]}{builder.Configuration["Api:Login"]}");
        });

		builder.Services.Configure<GoogleDirectionsOptions>
			(builder.Configuration.GetSection(GoogleDirectionsOptions.GoogleDirections));

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
