namespace MauiInteligente2022.ViewModels;

public class ReportSummaryViewModel : BaseViewModel
{
    public ReportSummaryViewModel()
    {
        Title = Resources.NewReportTitle;
        PageId = NEW_REPORT_SUMMARY;
    }

    public override void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        NewReportStep1ViewModel = query[STEP1_VM_PARAMETER] as NewReportStep1ViewModel;
        NewReportStep2ViewModel = query[STEP2_VM_PARAMETER] as NewReportStep2ViewModel;
        NewReportStep3ViewModel = query[STEP3_VM_PARAMETER] as NewReportStep3ViewModel;
    }

    private NewReportStep1ViewModel newReportStep1ViewModel;

    public NewReportStep1ViewModel NewReportStep1ViewModel
    {
        get => newReportStep1ViewModel;
        set => SetProperty(ref newReportStep1ViewModel, value);
    }

    private NewReportStep2ViewModel newReportStep2ViewModel;

    public NewReportStep2ViewModel NewReportStep2ViewModel
    {
        get => newReportStep2ViewModel;
        set => SetProperty(ref newReportStep2ViewModel, value);
    }

    private NewReportStep3ViewModel newReportStep3ViewModel;

    public NewReportStep3ViewModel NewReportStep3ViewModel
    {
        get => newReportStep3ViewModel;
        set => SetProperty(ref newReportStep3ViewModel, value);
    }
}
