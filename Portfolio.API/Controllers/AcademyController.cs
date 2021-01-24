using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.API.Business.Interface;
using Portfolio.API.Model.Response;

namespace Portfolio.API.Controllers
{
    [Route("api/[controller]")]
    public class AcademyController : Controller
    {
        IBAcademy _academy;
        public AcademyController(IBAcademy academy)
        {
            _academy = academy;
        }
        [HttpGet]
        public IActionResult Get()
        {
            ResponseHttp response = _academy.getAcademy();

            Response.StatusCode = (int)response.StatusCode;
            return Content(response.Body);
        }
    }
}
