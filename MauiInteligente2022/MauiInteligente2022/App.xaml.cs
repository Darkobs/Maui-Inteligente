namespace MauiInteligente2022;

public partial class App : Application
{
	public App(SignUpPage splashPage)
	{
		InitializeComponent();

		MainPage = new NavigationPage(splashPage);
	}
}
