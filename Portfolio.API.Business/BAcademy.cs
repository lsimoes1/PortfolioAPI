using Newtonsoft.Json;
using Portfolio.API.Business.Interface;
using Portfolio.API.DAO;
using Portfolio.API.Model;
using Portfolio.API.Model.Response;
using System;
using System.Collections.Generic;
using System.Net;

namespace Portfolio.API.Business
{
    public class BAcademy : IBAcademy
    {
        public ResponseHttp getAcademy()
        {
            List<MAcademy> academy = null;
            try
            {
                academy = new AcademyDAO().FindAllAcademyProjects();
            }
            catch (Exception ex)
            {
                return new ResponseHttp()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Body = ex.Message
                };
            }

            if (academy == null || academy.Count.Equals(0))
            {
                return new ResponseHttp()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Body = "Nenhum registro encontrado."
                };
            }
            else
            {
                return new ResponseHttp()
                {
                    StatusCode = HttpStatusCode.OK,
                    Body = JsonConvert.SerializeObject(academy)
                };
            }
        }

       
    }
}
