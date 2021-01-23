using Portfolio.API.Model.Response;

namespace Portfolio.API.Business.Interface
{
    public interface IGitHub
    {
        ResponseHttp GetAllRepository();
    }
}
