using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Portfolio.API.Business.Interface;
using Portfolio.API.Model.Response;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Portfolio.API.Controllers
{
    [EnableCors]
    
    //Desativado devido a baixa performance no site.
    //[Authorize("Bearer")]

    [Route("api/[controller]")]
    public class GitHubController : Controller
    {
        private readonly IGitHub _git;
        private readonly HttpClient httpClient;

        public GitHubController(IGitHub git)
        {
            httpClient = new HttpClient();
            _git = git;
        }
        
        [HttpGet]
        [Produces(typeof(ResponseGit))]
        public IActionResult Get()
        {
            try
            {
                List<ResponseGit> response = _git.GetAllRepository(httpClient);

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
