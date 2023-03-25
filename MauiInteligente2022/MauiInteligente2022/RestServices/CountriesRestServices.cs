using BaseRestClientCore.Base;

namespace MauiInteligente2022.RestServices;

public class CountriesRestServices : GenericRestService<Country, Country>
{
    public CountriesRestServices(HttpClient httpClient) 
        : base(httpClient, AppConfiguration.UserToken)
    {

    }
}
