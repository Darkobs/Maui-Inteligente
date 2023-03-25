namespace MauiInteligente2022;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(ABOUT_PAGE_ID, typeof(AboutPage));
        Routing.RegisterRoute(BRANCH_DETAIL_PAGE_ID, typeof(BranchDetailPage));
        Routing.RegisterRoute(BRANCHES_PAGE_ID, typeof(LocationsPage));
        Routing.RegisterRoute(NEW_REPORT_STEP_1, typeof(NewReportStep1Page));
        Routing.RegisterRoute(NEW_REPORT_STEP_2, typeof(NewReportStep2Page));
        Routing.RegisterRoute(NEW_REPORT_STEP_3, typeof(NewReportStep3Page));
        Routing.RegisterRoute(NEW_REPORT_SUMMARY, typeof(ReportSummaryPage));
        Routing.RegisterRoute(PREVIEW_PHOTO, typeof(PreviewPhotoPage));
    }
}
