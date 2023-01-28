using MauiInteligente2022.Views.About;

namespace MauiInteligente2022;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(ABOUT_PAGE_ID, typeof(AboutPage));
        Routing.RegisterRoute(BRANCH_DETAIL_PAGE_ID, typeof(BranchDetailPage));
    }
}
