
namespace MauiInteligente2022.ViewModels;

public class TermsAndConditionsViewModel : BaseViewModel
{
	private readonly IServiceProvider _sp;

	public TermsAndConditionsViewModel(IServiceProvider sp)
	{
		PageId = TERMS_PAGE_ID;
		Title = Resources.TermsPageTitle;
		_sp = sp;
		AccepTermsCommand = new(() => AcceptTerms());
	}

	public Command AccepTermsCommand { get; set; }

	private void AcceptTerms()
	{
		AppConfiguration.UserAcceptTerms = true;
		Application.Current.MainPage = new NavigationPage(_sp.GetRequiredService<LoginPage>());
	}
}
