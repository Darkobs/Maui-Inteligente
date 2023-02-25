using BaseRestClientCore.Base;

namespace MauiInteligente2022.RestServices;

public class BranchRestServices : GenericRestService<Branch,Branch>
{
    public BranchRestServices(HttpClient httpClient)
        : base(httpClient, AppConfiguration.UserToken)
    {
        
    }
}
