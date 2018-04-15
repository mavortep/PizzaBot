using Administrating.Models;
using Administrating.WebClients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Administrating.Controllers
{
    [Authorize]
    public class HistoriesController : Controller
    {
        // GET: Histories
        public ActionResult Index()
        {
            IEnumerable<HistoryModel> products;
            HttpResponseMessage response = ApiConnector.client.GetAsync("Histories").Result;
            var content = response.Content.ReadAsStringAsync();
            products = response.Content.ReadAsAsync<IEnumerable<HistoryModel>>().Result;
            return View(products);
        }
    }
}