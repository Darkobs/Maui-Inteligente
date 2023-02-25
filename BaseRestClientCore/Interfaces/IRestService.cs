namespace BaseRestClientCore.Interfaces;

public interface IRestService<T,U>
    where T : class
    where U : class
{
    Task<IRestResponse<IEnumerable<U>>> GetAllAsync(string uri);

    Task<IRestResponse<U>> GetAsync(int id, string uri);

    Task<IRestResponse<U>> PostAync(T content, string uri);

    Task<IRestResponse<U>> PutAsync(int id, T content, string uri);

    Task<IRestResponse<U>> DeleteAsync(int id, string uri);
}
