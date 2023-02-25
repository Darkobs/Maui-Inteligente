using BaseRestClientCore.Interfaces;
using System.Net.Http.Json;

namespace BaseRestClientCore.Base;

public class BaseRestService
{
    private readonly HttpClient _httpClient;

    public BaseRestService(HttpClient httpClient) => _httpClient = httpClient;

    public BaseRestService(HttpClient httpClient, string token)
        : this(httpClient) => _httpClient.DefaultRequestHeaders.Authorization = new("bearer", token);

    protected async Task<HttpResponseMessage> SendHttpRequest<T>
        (HttpMethod httpMethod, T content = null!, string uri = "" )
        where T : class
    {
        HttpRequestMessage httpRequestMessage = new(httpMethod, uri);

        if(content is not null)
        {
            httpRequestMessage.Content = JsonContent.Create(content);
        }

        return await _httpClient.SendAsync(httpRequestMessage);
    }

    public async Task<IRestResponse<U>> SendReceiveAsync<T,U>(HttpMethod httpMethod, T content = null, string uri = "")
        where T : class
        where U : class
    {
        GenericRestResponse<U> restResponse = new()
        {
            Status = Enums.RestResponseStatus.Error,
            ResponseMessage = "Unexpected error"
        };

        try
        {
            HttpResponseMessage httpResponse = await SendHttpRequest<T>(httpMethod, content, uri);

            if (httpResponse.IsSuccessStatusCode)
            {
                restResponse.ResponseMessage = "Success";
                restResponse.Status = Enums.RestResponseStatus.Success;

                if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    restResponse.ResponseContent = await httpResponse.Content.ReadFromJsonAsync<U>();
                }
            }
            else
            {
                restResponse.ResponseMessage = $"{httpResponse.StatusCode}: {httpResponse.ReasonPhrase}";
                restResponse.Status= Enums.RestResponseStatus.Error;
            }
        }

        catch (TimeoutException) 
        { 
            restResponse.Status = Enums.RestResponseStatus.Timeout;
            restResponse.ResponseMessage = "Timeout has occured";
        }
        catch (Exception ex)
        {
            restResponse.Status = Enums.RestResponseStatus.Exception;
            restResponse.ResponseMessage = ex.Message;
        }

        return restResponse;
    }
}
