using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Portfolio.API.Business.Interface;
using Portfolio.API.Model.Response;

namespace Portfolio.API.Controllers
{
    [EnableCors]
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
        [Produces(typeof(ResponseHttp))]
        public IActionResult Get()
        {
            ResponseHttp response = _git.GetAllRepository();

            Response.StatusCode = (int)response.StatusCode;
            return Content(response.Body);
        }
    }
}
