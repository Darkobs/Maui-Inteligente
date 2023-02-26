namespace MauiInteligente2022.Views;

public partial class NewReportStep1Page : BindedPage
{
	public NewReportStep1Page(NewReportStep1ViewModel newReportStep1ViewModel)
	{
		InitializeComponent();
		BindingContext = newReportStep1ViewModel;
	}
}