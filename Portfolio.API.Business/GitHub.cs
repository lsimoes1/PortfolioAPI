﻿using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Portfolio.API.Business.Interface;
using Portfolio.API.Model.Response;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace Portfolio.API.Business
{
    public class GitHub : IGitHub
    {
        private IConfiguration _configuration;
        public GitHub(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public List<ResponseGit> GetAllRepository(HttpClient client)
        {
            HttpResponseMessage responseApi = null;
            try
            {
                client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");
                responseApi = client.GetAsync(_configuration.GetSection("GitRepository").Value).Result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro na chamada da API do Git - {ex.Message}");
            }

            if (responseApi.StatusCode.Equals(HttpStatusCode.OK))
            {
                List<ResponseGit> listRepository = JsonConvert.DeserializeObject<List<ResponseGit>>(responseApi.Content.ReadAsStringAsync().Result);
                return listRepository;
            }
            else
            {
                return null;
            }
        }
    }
}
