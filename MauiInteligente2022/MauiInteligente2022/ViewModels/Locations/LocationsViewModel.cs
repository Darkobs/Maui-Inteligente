using BaseRestClientCore.Enums;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Web;

namespace MauiInteligente2022.ViewModels;

public class LocationsViewModel : BaseViewModel
{
    private readonly BranchRestServices _branchRestService;
    private int currentPage = 0;

    public LocationsViewModel(BranchRestServices branchRestService)
    {
        PageId = BRANCHES_PAGE_ID;
        Title = Resources.LocationsTitle;
        _branchRestService = branchRestService;
        LoadNextItemsCommand = new(async () => await LoadNextItemsAsync());
        RefreshCommand = new(async () => await RefreshAsync());
    }

    public Command LoadNextItemsCommand { get; set; }
    public Command RefreshCommand { get; set; }

    private bool isRefreshing;

    public bool IsRefreshing
    {
        get => isRefreshing;
        set => SetProperty(ref isRefreshing, value);
    }

    private ObservableCollection<Branch> branches = new();

    public ObservableCollection<Branch> Branches
    {
        get => branches;
        set => SetProperty(ref branches, value);
    }

    private Branch selectedBranch;

    public Branch SelectedBranch
    {
        get => selectedBranch;

        set
        {
            SetProperty(ref selectedBranch, value);
            if (selectedBranch is not null)
            {
                AppShell.Current.GoToAsync(BRANCH_DETAIL_PAGE_ID, new Dictionary<string, object>()
                {
                    ["branch"] = selectedBranch,
                });

                SelectedBranch = null;
            }
        }
    }

    public override async Task OnAppearing()
    {
        await Task.Delay(100);
        await LoadNextItemsAsync();
    }

    private async Task RefreshAsync()
    {
        IsRefreshing = true;
        currentPage = 0;
        Branches = new();
        await LoadNextItemsAsync();
        IsRefreshing = false;
    }

    private async Task LoadNextItemsAsync()
    {
        try
        {
            if (!IsBusy)
            {
                IsBusy = true;
                currentPage++;

                NameValueCollection queryString = HttpUtility.ParseQueryString(string.Empty);
                queryString.Add("currentPage", currentPage.ToString());
                queryString.Add("pageSize", "20");

                var branchesResponse = await _branchRestService.GetAllAsync($"?{queryString}");

                if (branchesResponse.Status == RestResponseStatus.Success)
                {
                    foreach (var branch in branchesResponse.ResponseContent)
                    {
                        Branches.Add(branch);
                    }
                }

                IsBusy = false;
            }
        } 
        catch (Exception ex) 
        {
            string error = ex.Message;
        }
    }
    
}
