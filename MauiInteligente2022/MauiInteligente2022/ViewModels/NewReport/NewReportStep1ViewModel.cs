namespace MauiInteligente2022.ViewModels;

public class NewReportStep1ViewModel : BaseViewModel
{
    private readonly MediaHelper _mediaHelper;

    private readonly string reportId;

    private string photoPath1, photoPath2, photoPath3, photoPath4;

    public NewReportStep1ViewModel(MediaHelper mediaHelper)
    {
        reportId = Guid.NewGuid().ToString();
        _mediaHelper = mediaHelper;
        PageId = NEW_REPORT_STEP_1;
        Title = Resources.NewReportTitle;
        SubTitle = Resources.NewReportStep1Subtitle;
        TakePhotoCommand = new(async (photoIndex) => await TakePhotoAsync(photoIndex.ToString()));
        _mediaHelper = mediaHelper;

    }

    public Command TakePhotoCommand { get; set; }


    #region Properties
    private byte[] photo1;

    public byte[] Photo1
    {
        get => photo1;

        set => SetProperty(ref photo1, value);
    }

    private byte[] photo2;

    public byte[] Photo2
    {
        get => photo2;

        set => SetProperty(ref photo2, value);
    }

    private byte[] photo3;

    public byte[] Photo3
    {
        get => photo3;

        set => SetProperty(ref photo3, value);
    }

    private byte[] photo4;

    public byte[] Photo4
    {
        get => photo4;

        set => SetProperty(ref photo4, value);
    }
    #endregion

    private async Task TakePhotoAsync(string photoIndex)
    {
        if (!IsBusy)
        {
            var photoBytes = await _mediaHelper.TakePhotoAsync();

            string photoPath = $"{reportId}-{photoIndex}.jpg";

            switch (photoIndex)
            {
                case "1":
                    Photo1 = photoBytes;
                    photoPath1 = photoPath;
                    break;

                case "2":
                    Photo2 = photoBytes;
                    photoPath2 = photoPath;
                    break;

                case "3":
                    Photo3 = photoBytes;
                    photoPath3 = photoPath;
                    break;

                case "4":
                    Photo4 = photoBytes;
                    photoPath4 = photoPath;
                    break;
            }

            if(photoBytes is not null)
            {
                Dictionary<string, object> navigationParams = new()
                {
                    [PHOTO_PARAMETER] = photoBytes,
                    [PHOTO_INDEX_PARAMETER] = photoIndex,
                    [REPORT_ID_PARAMETER] = reportId,
                    [STEP1_VM_PARAMETER] = this
                };

                await AppShell.Current.GoToAsync(PREVIEW_PHOTO, navigationParams);
            }

            IsBusy = false;
        }
    }
}
