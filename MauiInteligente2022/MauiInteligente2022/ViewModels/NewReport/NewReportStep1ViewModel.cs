using System.Text.Json;

namespace MauiInteligente2022.ViewModels;

public class NewReportStep1ViewModel : BaseViewModel
{
    private readonly MediaHelper _mediaHelper;

    private readonly LocalFilesHelper _localFilesHelper;

    private string reportId;

    private string photoPath1, photoPath2, photoPath3, photoPath4;

    public NewReportStep1ViewModel(MediaHelper mediaHelper, LocalFilesHelper localFilesHelper)
    {
        reportId = Guid.NewGuid().ToString();
        _mediaHelper = mediaHelper;
        _localFilesHelper = localFilesHelper;
        PageId = NEW_REPORT_STEP_1;
        Title = Resources.NewReportTitle;
        SubTitle = Resources.NewReportStep1Subtitle;
        TakePhotoCommand = new(async (photoIndex) => await TakePhotoAsync(photoIndex.ToString()));
        NextCommand = new(async () => await NextAsync());
        LoadDataAsync();
    }

    public Command TakePhotoCommand { get; set; }

    public Command NextCommand { get; set; }

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

    public async override Task SaveAsync()
    {
        NewRecordStep1 newRecordStep1 = new(reportId, photoPath1, photoPath2, photoPath3, photoPath4);
        var jsonReport1 = JsonSerializer.Serialize(newRecordStep1);
        await _localFilesHelper.SaveFileAsync(NEW_REPORT_STEP_1, jsonReport1);
    }

    private async Task LoadDataAsync()
    {
        if (!IsBusy)
        {
            IsBusy = true;

            var jsonReport1 = await _localFilesHelper.ReadTextFileAsync(NEW_REPORT_STEP_1);

            if (jsonReport1 is not null)
            {
                var savedData = JsonSerializer.Deserialize<NewRecordStep1>(jsonReport1);

                reportId = savedData.ReportId;
                photoPath1 = savedData.PhotoPath1;
                photoPath2 = savedData.PhotoPath2;
                photoPath3 = savedData.PhotoPath3;
                photoPath4 = savedData.PhotoPath4;

                if (photoPath1 is not null)
                    Photo1 = await _localFilesHelper.ReadFileAsync(photoPath1);

                if (photoPath2 is not null)
                    Photo2 = await _localFilesHelper.ReadFileAsync(photoPath2);

                if (photoPath3 is not null)
                    Photo3 = await _localFilesHelper.ReadFileAsync(photoPath3);

                if (photoPath4 is not null)
                    Photo4 = await _localFilesHelper.ReadFileAsync(photoPath4);
            }

            IsBusy = false;
        }
    }

    private async Task TakePhotoAsync(string photoIndex)
    {
        if (!IsBusy)
        {
            var photoBytes = await _mediaHelper.TakePhotoAsync();

            string photoPath = $"{reportId}-{photoIndex}.jpg";

            if (photoBytes is not null)
            {
                await _localFilesHelper.SaveFileAsync(photoPath, photoBytes);
            }
            else
            {
                _localFilesHelper.DeleteFile(photoPath);
            }

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

            await SaveAsync();

            if (photoBytes is not null)
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

    private async Task NextAsync() => await Shell.Current.GoToAsync(NEW_REPORT_STEP_2,
        new Dictionary<string, object>() {[STEP1_VM_PARAMETER] = this});
}
