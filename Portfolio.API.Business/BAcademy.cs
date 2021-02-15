using Newtonsoft.Json;
using Portfolio.API.Business.Interface;
using Portfolio.API.DAO;
using Portfolio.API.DAO.Interface;
using Portfolio.API.Model;
using Portfolio.API.Model.Response;
using System;
using System.Collections.Generic;
using System.Net;

namespace Portfolio.API.Business
{
    public class BAcademy : IBAcademy
    {
        private IAcademyDAO _academyDAO;

        public BAcademy(IAcademyDAO academyDao)
        {
            _academyDAO = academyDao;
        }

        public ResponseHttp getAcademy()
        {
            List<MAcademy> academy = null;
            try
            {
                academy = _academyDAO.FindAllAcademyProjects();
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
