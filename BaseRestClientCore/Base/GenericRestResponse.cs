using BaseRestClientCore.Enums;
using BaseRestClientCore.Interfaces;

namespace BaseRestClientCore.Base;

public class GenericRestResponse<U> : IRestResponse<U>
    where U : class
{
    public U? ResponseContent { get; set; }
    public string? ResponseMessage { get; set; }
    public RestResponseStatus Status { get; set; }
}
