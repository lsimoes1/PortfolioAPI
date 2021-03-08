using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Portfolio.API.Business.Interface;
using Portfolio.API.Model;
using System;
using System.Collections.Generic;

namespace Portfolio.API.Controllers
{
    [EnableCors]

    //Desativado devido a baixa perfomance no site.
    //[Authorize("Bearer")]

    [Route("api/[controller]")]
    public class AcademyController : Controller
    {
        IBAcademy _academy;

        public AcademyController(IBAcademy academy)
        {
            _academy = academy;
        }

        [HttpGet]
        [Produces(typeof(MAcademy))]
        public IActionResult Get()
        {
            try
            {
                List<MAcademy> response = _academy.getAcademy();

                if (response == null || response.Equals(string.Empty))
                {
                    return NotFound("Nenhum registro encontrado!");
                }
                
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
