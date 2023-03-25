namespace MauiInteligente2022.Views;

public partial class ReportSummaryPage : BindedPage
{
	public ReportSummaryPage(ReportSummaryViewModel reportSummaryViewModel)
	{
		InitializeComponent();
        BindingContext = reportSummaryViewModel;
    }
}