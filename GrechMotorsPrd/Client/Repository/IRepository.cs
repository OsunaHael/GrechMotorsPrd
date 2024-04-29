
namespace GrechMotorsPrd.Client.Repository
{
    public interface IRepository
    {
        Task<HttpResponseWrapper<T>> Get<T>(string url);
        Task<HttpResponseWrapper<object>> Post<T>(string url, T sendObject);
        Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T sendObject);
    }
}