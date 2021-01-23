using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio.API.Controllers
{
    [Route("api/[controller]")]
    public class AcademyController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Token válido");
        }
    }
}
