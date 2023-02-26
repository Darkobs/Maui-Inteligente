namespace MauiInteligente2022.ViewModels;

public class MainMenuViewModel : BaseViewModel
{
	public MainMenuViewModel(IServiceProvider sp)
	{
		Title = Resources.MainMenuTitle;
		SubTitle = Resources.MainMenuSubtitle;
		PageId = MAIN_MENU_PAGE_ID;

        AboutCommand = new(async () => await NavigateToAsync(ABOUT_PAGE_ID));

        LocationsCommand = new(async () => await NavigateToAsync(BRANCHES_PAGE_ID));

        LogoutCommand = new(() =>
        {
            AppConfiguration.UserToken = null;
            App.Current.MainPage = new NavigationPage(sp.GetRequiredService<LoginPage>());
        });

        NewReportCommand = new(async () => await NavigateToAsync(NEW_REPORT_STEP_1));
    }

    public Command AboutCommand { get; set; }

    public Command LocationsCommand { get; set; }

    public Command LogoutCommand { get; set; }

    public Command NewReportCommand { get; set; }

    private async Task NavigateToAsync(string route)
        => await Shell.Current.GoToAsync(route);
}
