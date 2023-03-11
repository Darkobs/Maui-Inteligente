namespace MauiInteligente2022.Views;

public partial class NewReportStep2Page : BindedPage
{
	public NewReportStep2Page(NewReportStep2ViewModel newReportStep2ViewModel)
	{
		InitializeComponent();
		BindingContext = newReportStep2ViewModel;
	}
}