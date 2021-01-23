﻿using Newtonsoft.Json;
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
        private HttpClient client;
        public GitHub()
        {
            client = new HttpClient();
        }
        public ResponseHttp GetAllRepository()
        {
            HttpResponseMessage responseApi = null;
            try
            {
                client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");
                responseApi = client.GetAsync("https://api.github.com/users/lsimoes1/repos").Result;
            }
            catch (Exception ex)
            {

                return new ResponseHttp()
                {
                    StatusCode = responseApi == null ? HttpStatusCode.InternalServerError : responseApi.StatusCode,
                    Body = ex.Message
                };
            }

            if (responseApi.StatusCode.Equals(HttpStatusCode.OK))
            {
                List<ResponseGit> listRepository = JsonConvert.DeserializeObject<List<ResponseGit>>(responseApi.Content.ReadAsStringAsync().Result);
                return new ResponseHttp()
                {
                    StatusCode = responseApi.StatusCode,
                    Body = JsonConvert.SerializeObject(listRepository)
                };
            }
            else
            {
                return new ResponseHttp()
                {
                    StatusCode = responseApi.StatusCode,
                    Body = responseApi.Content.ReadAsStringAsync().Result
                };
            }
        }
    }
}