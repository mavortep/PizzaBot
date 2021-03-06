﻿using Administrating.Models;
using Administrating.WebClients;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Administrating.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index()
        {
            IEnumerable<ProductModel> products;
            HttpResponseMessage response = ApiConnector.client.GetAsync("Products").Result;
            var content = response.Content.ReadAsStringAsync();
            products = response.Content.ReadAsAsync<IEnumerable<ProductModel>>().Result;
            return View(products);
        }

        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new ProductModel());
            }
            else
            {
                HttpResponseMessage response = ApiConnector.client.GetAsync("Products/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<ProductModel>().Result);
            }
            
        }

        [HttpPost]
        public ActionResult AddOrEdit(ProductModel product)
        {
            try
            {
                if (product.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(product.ImageUpload.FileName);
                    string extension = Path.GetExtension(product.ImageUpload.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    product.ImagePath = fileName;
                    product.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/App_Files/Images/"), fileName));
                }
                if (product.Id == 0)
                {
                    HttpResponseMessage response = ApiConnector.client.PostAsJsonAsync("Products", product).Result;
                }
                else
                {
                    HttpResponseMessage response = ApiConnector.client.PutAsJsonAsync("Products/" + product.Id, product).Result;
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Json(new { success = true, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = ApiConnector.client.DeleteAsync("Products/" + id.ToString()).Result;
            return RedirectToAction("Index");
        }
    }
}