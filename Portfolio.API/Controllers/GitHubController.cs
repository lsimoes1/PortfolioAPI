using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.API.Business.Interface;
using Portfolio.API.Model.Response;

namespace Portfolio.API.Controllers
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    public class GitHubController : Controller
    {
        private IGitHub _git;
        public GitHubController(IGitHub git)
        {
            _git = git;
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            ResponseHttp response = _git.GetAllRepository();

            Response.StatusCode = (int)response.StatusCode;
            return Content(response.Body);
        }
    }
}
