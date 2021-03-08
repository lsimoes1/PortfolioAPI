using Portfolio.API.Model.Response;
using System.Collections.Generic;
using System.Net.Http;

namespace Portfolio.API.Business.Interface
{
    public interface IGitHub
    {
        List<ResponseGit> GetAllRepository(HttpClient client);
    }
}
