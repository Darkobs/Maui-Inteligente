using MauiInteligente2022.AppBase.Services.GoogleApis;
using MauiInteligente2022.ViewModels;
using System.Web;
namespace MauiInteligente2022.AppBase.Objects;

public abstract class BaseViewModel : ObservableObject, IQueryAttributable
{
    private string _title;

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    private string _subtitle;

    public string SubTitle
    {
        get => _subtitle;
        set => SetProperty(ref _subtitle, value);
    }

    private string _pageId;

    public string PageId
    {
        get => _pageId;
        set => SetProperty(ref _pageId, value);
    }

    private bool _isBusy;
    public bool IsBusy
    {
        get => _isBusy;
        set => SetProperty(ref _isBusy, value);
    }

    public virtual Task OnBackButtonPressed() => Task.CompletedTask;

    public virtual Task SaveAsync() => Task.CompletedTask;

    public virtual Task OnAppearing() => Task.CompletedTask;

    public virtual Task OnDisappearing() => Task.CompletedTask;

    public virtual void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        
    }
}
