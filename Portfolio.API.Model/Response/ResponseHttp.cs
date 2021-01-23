using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Portfolio.API.Model.Response
{
    public class ResponseHttp
    {
        public HttpStatusCode StatusCode { get; set; }

        public string Body { get; set; }
    }
}
