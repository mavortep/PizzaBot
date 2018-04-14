using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Administrating.WebClients
{
    public class ApiConnector
    {
        public static HttpClient client = new HttpClient();

        static ApiConnector()
        {
            client.BaseAddress = new Uri("http://localhost:52195/api/");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}