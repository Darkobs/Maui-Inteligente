namespace MauiInteligente2022.ViewModels;

public class PreviewPhotoViewModel : BaseViewModel
{
    private readonly MediaHelper _mediaHelper;
    private string photoIndex;
    private string reportId;
    private NewReportStep1ViewModel reportStep1ViewModel;

    public PreviewPhotoViewModel(MediaHelper mediaHelper)
    {
        _mediaHelper = mediaHelper;
        PageId = PREVIEW_PHOTO;
        Title = Resources.NewReportTitle;
        SubTitle = Resources.PreviewPhotoSubtitle;
        AcceptCommand = new(async () => await AcceptAsync());
        TakePhotoCommand = new(async () => await TakePhotoAsync());
    }

    public override void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Photo = query[PHOTO_PARAMETER] as byte[];
        photoIndex = query[PHOTO_INDEX_PARAMETER] as string;
        reportId = query[REPORT_ID_PARAMETER] as string;
        reportStep1ViewModel = query[STEP1_VM_PARAMETER] as NewReportStep1ViewModel;
    }

    public Command TakePhotoCommand { get; set; }

    public Command AcceptCommand { get; set; }

    private byte[] photo;

    public byte[] Photo
    {
        get => photo;
        set => SetProperty(ref photo, value);
    }

    private async Task AcceptAsync() => await AppShell.Current.GoToAsync("..");

    private async Task TakePhotoAsync()
    {
        if(!IsBusy)
        {
            IsBusy = true;

            var photoBytes = await _mediaHelper.TakePhotoAsync();
            string photoPath = $"{reportId}-{photoIndex}";

            Photo = photoBytes;

            switch(photoIndex)
            {
                case "1":
                    reportStep1ViewModel.Photo1 = photo;
                    break;

                case "2":
                    reportStep1ViewModel.Photo2 = photo;
                    break;

                case "3":
                    reportStep1ViewModel.Photo3 = photo;
                    break;

                case "4":
                    reportStep1ViewModel.Photo4 = photo;
                    break;
            }

            IsBusy = false;
        }
    }

}
