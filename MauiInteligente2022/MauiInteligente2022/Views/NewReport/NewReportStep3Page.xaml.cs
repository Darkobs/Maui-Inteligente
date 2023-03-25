namespace MauiInteligente2022.Views;

public partial class NewReportStep3Page : BindedPage
{
	public NewReportStep3Page(NewReportStep3ViewModel newReportStep3ViewModel)
	{
		InitializeComponent();
		BindingContext = newReportStep3ViewModel;
	}
}