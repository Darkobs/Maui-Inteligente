using BaseRestClientCore.Enums;

namespace BaseRestClientCore.Interfaces;

public interface IRestResponse<U> 
    where U : class
{
    U? ResponseContent { get; }

    string? ResponseMessage { get; }

    RestResponseStatus Status { get; }
}
