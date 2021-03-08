using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Portfolio.API.Business.Interface;
using Portfolio.API.Model;
using System;
using System.Collections.Generic;

namespace Portfolio.API.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    public class ProfessionalController : Controller
    {
        private readonly IBProfessional _professionalInfo;

        public ProfessionalController(IBProfessional professionalInfo)
        {
            _professionalInfo = professionalInfo;
        }

        [HttpGet]
        [Produces(typeof(MProfessional))]
        public IActionResult Get()
        {
            try
            {
                List<MProfessional> response = _professionalInfo.GetAllProfessionalInfo();

                if (response == null || response.Equals(string.Empty))
                {
                    return NotFound("Não foi encontrado nenhum registro!");
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
