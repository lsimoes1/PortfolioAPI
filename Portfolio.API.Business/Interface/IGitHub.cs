using Portfolio.API.Model.Response;
using System.Net.Http;

namespace Portfolio.API.Business.Interface
{
    public interface IGitHub
    {
        ResponseHttp GetAllRepository(HttpClient client);
    }
}
