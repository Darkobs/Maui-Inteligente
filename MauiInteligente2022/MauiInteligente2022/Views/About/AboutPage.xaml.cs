namespace MauiInteligente2022.Views.About;

public partial class AboutPage : BindedPage
{
	public AboutPage(AboutViewModel aboutViewModel)
	{
		InitializeComponent();
		BindingContext = aboutViewModel;
	}
}