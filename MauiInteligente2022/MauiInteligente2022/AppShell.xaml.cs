using MauiInteligente2022.Views.About;

namespace MauiInteligente2022;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(ABOUT_PAGE_ID, typeof(AboutPage));
	}
}
